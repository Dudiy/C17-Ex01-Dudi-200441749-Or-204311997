using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C17_Ex01_Dudi_200441749_Or_204311997.DataTables
{
    class FacebookPhotosDataTable : FacebookDataTable
    {
        public List<Album> AlbumsToLoad { get; set; }

        public FacebookPhotosDataTable(User i_LoggedInUser)
            : base(i_LoggedInUser, "Photos", typeof(Photo))
        { }

        public override void FetchDataTableValues()
        {
            DataTable.Clear();
            TotalRows = 0;
            foreach (Album album in AlbumsToLoad)
            {
                TotalRows += album.Count != null ? (int)album.Count : 0;
            }

            //add rows
            ProgressBarWindow progressBarWindow = new ProgressBarWindow(0, TotalRows);
            progressBarWindow.Show();
            foreach (Album album in AlbumsToLoad)
            {
                foreach (Photo photo in album.Photos)
                {
                    StringBuilder photoTags = new StringBuilder();
                    if (photo.Tags != null)
                    {
                        foreach (PhotoTag tag in photo.Tags)
                        {
                            photoTags.Append(tag.User.Name);
                            photoTags.Append(", ");
                        }
                        photoTags.Remove(photoTags.Length - 2, 2);
                    }

                    DataTable.Rows.Add(
                        photo,
                        photo.Album.Name,
                        //TODO get most liked comment
                        photo.CreatedTime != null ? photo.CreatedTime : null,   //TODO fix format
                        photo.LikedBy != null ? photo.LikedBy.Count : 0,
                        photo.Comments != null ? photo.Comments.Count : 0,
                        photo.Tags != null ? photoTags.ToString() : string.Empty,
                        photo.URL
                        );
                    progressBarWindow.ProgressValue++;
                    //PhotosDataLoadStatus = (float)++counter / totalRows;
                    //PhotosDataLoadStatus.Value++;
                }
            }

            DataFetched = true;
        }

        public override void OnRowDoubleClicked(object i_SelectedObject)
        {
            Photo photoSelected = i_SelectedObject as Photo;

            if (photoSelected != null)
            {
                PhotoDetails photoDetails = new PhotoDetails(photoSelected);
                photoDetails.Show();
            }
        }

        protected override void initColumns()
        {
            DataTable.Columns.Add("Album Name", typeof(string));
            DataTable.Columns.Add("Created Time", typeof(DateTime));
            DataTable.Columns.Add("Likes", typeof(int));
            DataTable.Columns.Add("Comments", typeof(int));
            DataTable.Columns.Add("Tags", typeof(string));
            DataTable.Columns.Add("URL", typeof(string));
        }
    }
}
