﻿/*
 * C17_Ex01: FacebookPhotoUtils.cs
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/
using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C17_Ex01_Dudi_200441749_Or_204311997.DataTables
{
    public static class FacebookPhotoUtils
    {
        //public static int GetTotalPhotosUploadedByUser(User i_User)
        //{
        //    List<Album> albums = new List<Album>(i_User.Albums.Count);
        //    int totalPhotos = 0;

        //    if (i_User.Albums.Count > 0)
        //    {
        //        albums.AddRange(i_User.Albums);
        //        totalPhotos = GetTotalPhotosInAlbumArray(albums.ToArray());
        //    }

        //    return totalPhotos;
        //    //int photoCounter = 0;

        //    //foreach (Album album in i_User.Albums)
        //    //{
        //    //    //cast to int - very unlikely that a user has that many albums
        //    //    photoCounter += Math.Min((int)(album.Count ?? 0), FacebookApplication.k_MaxPhotosInAlbum);
        //    //}

        //    //return photoCounter;
        //}

        //public static int GetTotalPhotosInAlbumArray(Album[] i_Albums)
        //{
        //    int photoCounter = 0;

        //    foreach (Album album in i_Albums)
        //    {
        //        //cast to int - very unlikely that a user has that many albums
        //        photoCounter += Math.Min((int)(album.Count ?? 0), FacebookApplication.k_MaxPhotosInAlbum);
        //    }

        //    return photoCounter;
        //}

        //public static List<Photo> GetAllUserPhotos(User i_User, ref int i_ProgressValue)
        //{
        //    List<Photo> photos = new List<Photo>(GetTotalPhotosUploadedByUser(i_User));

        //    i_ProgressValue = 0;
        //    foreach (Album album in i_User.Albums)
        //    {
        //        photos.AddRange(album.Photos);
        //        i_ProgressValue += (int)(album.Count ?? 0);
        //    }

        //    return photos;
        //}

        public static Dictionary<Album, List<Photo>> GetPhotosByOwnerAndTags(User i_User, User i_Tagged, Album[] albums, ProgressBar i_ProgressBar)
        {
            Dictionary<Album, List<Photo>> photos = new Dictionary<Album, List<Photo>>();
            int photosToSearch = 0;
            AlbumsSelector albumSelector = new AlbumsSelector(i_User);
            i_ProgressBar.Value = 0;

            if (albums.Length > 0)
            {
                foreach (Album album in albums)
                {
                    photosToSearch += album.Count != null ? (int)album.Count : 0;
                }
                i_ProgressBar.Maximum = photosToSearch;
                //ProgressBarWindow progressBarWindow = new ProgressBarWindow(0, photosToSearch, "photos");
                //progressBarWindow.Show();
                foreach (Album album in albums)
                {
                    List<Photo> photosInAlbum = new List<Photo>();
                    foreach (Photo photo in album.Photos)
                    {
                        //progressBarWindow.ProgressValue++;
                        i_ProgressBar.Value++;
                        if (photo.Tags != null)
                        {
                            foreach (PhotoTag tag in photo.Tags)
                            {
                                if (tag.User.Id == i_Tagged.Id)
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

            return photos;
        }
    }
}
