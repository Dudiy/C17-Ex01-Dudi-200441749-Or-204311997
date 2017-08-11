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
        User m_LoggedInUser;
        public FriendshipAnalyzer(User i_LoggedInUser)
        {
            m_LoggedInUser = FacebookApplication.LoggedInUser;
        }

        public List<Photo> FetchPhotosTaggedTogether()
        {
            List<Photo> photos = new List<Photo>();
            ProgressBarWindow progressBarWindow = new ProgressBarWindow(0, m_LoggedInUser.PhotosTaggedIn.Count);
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
                AlbumsSelector albumSelector = new AlbumsSelector(m_LoggedInUser);

                DialogResult dialogResult = albumSelector.ShowDialog();

                if (dialogResult == DialogResult.OK && albumSelector.SelectedAlbums.Count > 0)
                {
                    foreach (Album album in albumSelector.SelectedAlbums)
                    {
                        photosToSearch += album.Count != null ? (int)album.Count : 0;
                    }

                    ProgressBarWindow progressBarWindow = new ProgressBarWindow(0, photosToSearch);
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
    }
}
