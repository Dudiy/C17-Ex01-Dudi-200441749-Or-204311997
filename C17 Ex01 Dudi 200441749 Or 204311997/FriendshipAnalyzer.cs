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
        private User m_LoggedInUser = FacebookApplication.LoggedInUser;
        public User Friend { get; set; }

        public List<Photo> FetchPhotosTaggedTogether(ProgressBar i_ProgressBar)
        {
            List<Photo> photos = new List<Photo>();
            if (i_ProgressBar == null)
            {
                i_ProgressBar = new ProgressBar();
            }

            i_ProgressBar.Maximum = m_LoggedInUser.PhotosTaggedIn.Count;
            i_ProgressBar.Minimum = 0;
            i_ProgressBar.Value = 0;

            foreach (Photo photo in m_LoggedInUser.PhotosTaggedIn)
            {
                i_ProgressBar.Value++;
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

            photos.OrderBy(photo => photo.CreatedTime);
            return photos;
        }

        public Dictionary<string, List<Photo>> groupPhotoListByOwner(List<Photo> i_Photos)
        {
            Dictionary<string, List<Photo>> groupedPhotos = new Dictionary<string, List<Photo>>();

            foreach (Photo photo in i_Photos)
            {
                if (groupedPhotos.ContainsKey(photo.From.Id))
                {
                    groupedPhotos[photo.From.Id].Add(photo);
                }
                else
                {
                    List<Photo> photoList = new List<Photo>();
                    photoList.Add(photo);
                    groupedPhotos.Add(photo.From.Id, photoList);
                }
            }

            return groupedPhotos;
        }

        public Dictionary<Album, List<Photo>> GetPhotosByOwnerAndTags()
        {
            Dictionary<Album, List<Photo>> photos = new Dictionary<Album, List<Photo>>();
            if (Friend != null)
            {
                int photosToSearch = 0;
                AlbumsSelector albumSelector = new AlbumsSelector(FacebookApplication.LoggedInUser);

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
        public Photo GetMostRecentPhotoTaggedTogether()
        {
            List<Photo> photosTaggedTogether = FetchPhotosTaggedTogether(null);
            return photosTaggedTogether.Count > 0 ? photosTaggedTogether[0] : null;
        }

        public int GetNumberOfPhotosFriendLiked(ProgressBar i_ProgressBar)
        {
            int numLikes = 0;
            int totalPhotos = 0;
            foreach (Album album in m_LoggedInUser.Albums)
            {
                totalPhotos += Math.Min((int)(album.Count ?? 0), FacebookApplication.k_MaxPhotosInAlbum);
            }

            i_ProgressBar.Maximum = totalPhotos;
            i_ProgressBar.Minimum = 0;
            i_ProgressBar.Value = 0;
            foreach (Album album in m_LoggedInUser.Albums)
            {
                foreach (Photo photo in album.Photos)
                {
                    i_ProgressBar.Value++;
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

        public int GetNumberOfPhotosFriendCommented(ProgressBar i_ProgressBar)
        {
            int numComments = 0;
            int totalPhotos = 0;

            foreach (Album album in m_LoggedInUser.Albums)
            {
                totalPhotos += Math.Min((int)(album.Count ?? 0), FacebookApplication.k_MaxPhotosInAlbum);
            }

            i_ProgressBar.Maximum = totalPhotos;
            i_ProgressBar.Minimum = 0;
            i_ProgressBar.Value = 0;
            foreach (Album album in m_LoggedInUser.Albums)
            {
                foreach (Photo photo in album.Photos)
                {
                    i_ProgressBar.Value++;
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
