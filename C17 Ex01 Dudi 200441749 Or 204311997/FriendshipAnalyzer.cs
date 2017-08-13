/*
 * C17_Ex01: FriendshipAnalyzer.cs
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/
using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public class FriendshipAnalyzer
    {
        private User m_LoggedInUser = FacebookApplication.LoggedInUser;
        public User Friend { get; set; }

        public IEnumerable<Tuple<int, int, object>> FetchPhotosTaggedTogether()
        {
            List<Photo> photos = new List<Photo>();
            int currTag = 0;
            int totalTag = m_LoggedInUser.PhotosTaggedIn.Count;

            foreach (Photo photo in m_LoggedInUser.PhotosTaggedIn)
            {
                yield return Tuple.Create(++currTag, totalTag, (object)photos);

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

        //public Dictionary<Album, List<Photo>> GetPhotosByOwnerAndTags()
        //{
        //    Dictionary<Album, List<Photo>> photos = new Dictionary<Album, List<Photo>>();
        //    if (Friend != null)
        //    {
        //        int photosToSearch = 0;
        //        AlbumsSelector albumSelector = new AlbumsSelector(FacebookApplication.LoggedInUser);

        //        DialogResult dialogResult = albumSelector.ShowDialog();

        //        if (dialogResult == DialogResult.OK && albumSelector.SelectedAlbums.Count > 0)
        //        {
        //            foreach (Album album in albumSelector.SelectedAlbums)
        //            {
        //                photosToSearch += album.Count != null ? (int)album.Count : 0;
        //            }

        //            ProgressBarWindow progressBarWindow = new ProgressBarWindow(0, photosToSearch, "photos");
        //            progressBarWindow.Show();
        //            foreach (Album album in albumSelector.SelectedAlbums)
        //            {
        //                List<Photo> photosInAlbum = new List<Photo>();
        //                foreach (Photo photo in album.Photos)
        //                {
        //                    progressBarWindow.ProgressValue++;
        //                    if (photo.Tags != null)
        //                    {
        //                        foreach (PhotoTag tag in photo.Tags)
        //                        {
        //                            if (tag.User.Id == Friend.Id)
        //                            {
        //                                photosInAlbum.Add(photo);
        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //                if (photosInAlbum.Count > 0)
        //                {
        //                    photos.Add(album, photosInAlbum);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        throw new ArgumentException("No friend selected");
        //    }

        //    return photos;
        //}
        public Photo GetMostRecentPhotoTaggedTogether(List<Photo> i_PhotosTaggedTogether)
        {
            //List<Photo> photosTaggedTogether = FetchPhotosTaggedTogether(null);
            return i_PhotosTaggedTogether.Count > 0 ? i_PhotosTaggedTogether[0] : null;
        }

        public IEnumerable<Tuple<int, int, object>> GetNumberOfPhotosFriendLiked()
        {
            int numLikes = 0;
            int totalPhotos = 0;
            foreach (Album album in m_LoggedInUser.Albums)
            {
                totalPhotos += Math.Min((int)(album.Count ?? 0), FacebookApplication.k_MaxPhotosInAlbum);
            }

            //i_ProgressBar.Maximum = totalPhotos;
            //i_ProgressBar.Minimum = 0;
            //i_ProgressBar.Value = 0;
            int currPhoto = 0;

            foreach (Album album in m_LoggedInUser.Albums)
            {
                foreach (Photo photo in album.Photos)
                {
                    yield return Tuple.Create(++currPhoto, totalPhotos, (object)numLikes);

                    //i_ProgressBar.Value++;
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

            //return numLikes;
        }

        public IEnumerable<Tuple<int, int, object>> GetNumberOfPhotosFriendCommented()
        {
            int numComments = 0;
            int totalPhotos = 0;

            foreach (Album album in m_LoggedInUser.Albums)
            {
                totalPhotos += Math.Min((int)(album.Count ?? 0), FacebookApplication.k_MaxPhotosInAlbum);
            }

            //i_ProgressBar.Maximum = totalPhotos;
            //i_ProgressBar.Minimum = 0;
            //i_ProgressBar.Value = 0;
            int currPhoto = 0;

            foreach (Album album in m_LoggedInUser.Albums)
            {
                foreach (Photo photo in album.Photos)
                {
                    //i_ProgressBar.Value++;
                    foreach (Comment comment in photo.Comments)
                    {
                        yield return Tuple.Create(++currPhoto, totalPhotos, (object)numComments);

                        if (comment.From.Id == Friend.Id)
                        {
                            numComments++;
                            break;
                        }
                    }
                }
            }

            //return numComments;
        }
    }
}
