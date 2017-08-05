using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C17_Ex01_Dudi_200441749_Or_204311997.DataTables
{
    class FacebookLikedPagesDataTable : FacebookDataTable
    {
        public FacebookLikedPagesDataTable(User i_LoggedInUser)
            : base(i_LoggedInUser, "Liked Pages", typeof(Page))
        {
        }

        public override void FetchDataTableValues()
        {
            if (DataFetched == false || DataTable.Rows.Count == 0)
                {
                TotalRows = m_LoggedInUser.LikedPages.Count;
                
                //add rows
                foreach (Page page in m_LoggedInUser.LikedPages)
                {
                    DataTable.Rows.Add(
                        page,
                        page.Name,
                        page.Phone,
                        //page.LikesCount != null ? (int)page.LikesCount : 0,
                        page.Category,
                        page.Description,
                        page.Website
                        );
                    //LikedPagesDataLoadStatus = (float)++counter / totalRows;
                    //LikedPagesDataLoadStatus.Value++;
                }
            }

            DataFetched = true;
        }

        public override void OnRowDoubleClicked(object i_SelectedObject)
        {
            Page pageSelected = i_SelectedObject as Page;

            if (pageSelected != null)
            {
                PictureFrame pictureFrame = new PictureFrame(pageSelected.PictureLargeURL, pageSelected.Name);
                pictureFrame.Show();
            }
        }

        protected override void initColumns()
        {
            DataTable.Columns.Add("Name", typeof(string));
            DataTable.Columns.Add("Phone Number", typeof(string));
            //dataTable.Columns.Add("Likes", typeof(int));
            DataTable.Columns.Add("Category", typeof(string));
            DataTable.Columns.Add("Description", typeof(string));
            DataTable.Columns.Add("Website", typeof(string));
        }
    }
}
