using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    class FriendshipAnalyzer
    {
        public User Friend { get; set; }
        private User m_LoggedInUser = FacebookApplication.LoggedInUser;

        public List<Photo> FetchPhotosTaggedTogether()
        {
            List<Photo> photos = new List<Photo>();
            ProgressBarWindow progressBarWindow = new ProgressBarWindow(0, m_LoggedInUser.PhotosTaggedIn.Count, "photos tagged in");
            progressBarWindow.Show();

            foreach (Photo photo in m_LoggedInUser.PhotosTaggedIn)
            {
                progressBarWindow.ProgressValue++;
                if (photo.Tags != null)
                {
                    foreach (PhotoTag tag in photo.Tags)
                    {
                        if (tag.User.Id == Friend.Id)
                        {
                            photos.Add(photo);
                            break;
                        }
                    }
                }
            }

            return photos;
        }

        public Dictionary<Album, List<Photo>> GetPhotosOfMineFriendIsIn()
        {
            Dictionary<Album, List<Photo>> photos = new Dictionary<Album, List<Photo>>();
            if (Friend != null)
            {
                int photosToSearch = 0;
                AlbumsSelector albumSelector = new AlbumsSelector();

                DialogResult dialogResult = albumSelector.ShowDialog();

                if (dialogResult == DialogResult.OK && albumSelector.SelectedAlbums.Count > 0)
                {
                    foreach (Album album in albumSelector.SelectedAlbums)
                    {
                        photosToSearch += album.Count != null ? (int)album.Count : 0;
                    }

                    ProgressBarWindow progressBarWindow = new ProgressBarWindow(0, photosToSearch, "photos");
                    progressBarWindow.Show();
                    foreach (Album album in albumSelector.SelectedAlbums)
                    {
                        List<Photo> photosInAlbum = new List<Photo>();
                        foreach (Photo photo in album.Photos)
                        {
                            progressBarWindow.ProgressValue++;
                            if (photo.Tags != null)
                            {
                                foreach (PhotoTag tag in photo.Tags)
                                {
                                    if (tag.User.Id == Friend.Id)
                                    {
                                        photosInAlbum.Add(photo);
                                        break;
                                    }
                                }
                            }
                        }
                        if (photosInAlbum.Count > 0)
                        {
                            photos.Add(album, photosInAlbum);
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("No friend selected");
            }

            return photos;
        }

        public int GetNumberOfPhotosFriendLiked()
        {
            int numLikes = 0;
            int totalPhotos = 0;
            foreach (Album album in m_LoggedInUser.Albums)
            {
                totalPhotos += Math.Min((int)(album.Count ?? 0), FacebookApplication.k_MaxPhotosInAlbum);
            }

            ProgressBarWindow progressWindow = new ProgressBarWindow(0, totalPhotos, "Likes");
            progressWindow.Show();
            foreach (Album album in m_LoggedInUser.Albums)
            {
                foreach (Photo photo in album.Photos)
                {
                    progressWindow.ProgressValue++;
                    foreach (User user in photo.LikedBy)
                    {
                        if (user.Id == Friend.Id)
                        {
                            numLikes++;
                            break;
                        }
                    }
                }
            }

            return numLikes;
        }

        public int GetNumberOfPhotosFriendCommented()
        {
            int numComments = 0;
            int totalPhotos = 0;

            foreach (Album album in m_LoggedInUser.Albums)
            {
                totalPhotos += Math.Min((int)(album.Count ?? 0), FacebookApplication.k_MaxPhotosInAlbum);
            }

            ProgressBarWindow progressWindow = new ProgressBarWindow(0, totalPhotos, "Likes");
            progressWindow.Show();
            foreach (Album album in m_LoggedInUser.Albums)
            {
                foreach (Photo photo in album.Photos)
                {
                    progressWindow.ProgressValue++;
                    foreach (Comment comment in photo.Comments)
                    {
                        if (comment.From.Id == Friend.Id)
                        {
                            numComments++;
                            break;
                        }
                    }
                }
            }

            return numComments;
        }
    }
}
