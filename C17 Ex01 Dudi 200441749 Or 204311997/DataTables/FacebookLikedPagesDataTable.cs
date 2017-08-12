/*
 * C17_Ex01: FacebookLikedPagesDataTable.cs
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

namespace C17_Ex01_Dudi_200441749_Or_204311997.DataTables
{
    class FacebookLikedPagesDataTable : FacebookDataTable
    {
        public FacebookLikedPagesDataTable()
            : base("Liked Pages", typeof(Page))
        {
        }

        public override void FetchDataTableValues()
        {
            TotalRows = FacebookApplication.LoggedInUser.LikedPages.Count;

            //add rows
            foreach (Page page in FacebookApplication.LoggedInUser.LikedPages)
            {
                DataTable.Rows.Add(
                    page,
                    page.Name,
                    page.Phone,
                    page.Category,
                    page.Description,
                    page.Website
                    );
            }
        }

        public override void DisplayObjectDetails(object i_SelectedObject)
        {
            Page pageSelected = i_SelectedObject as Page;

            if (pageSelected != null)
            {
                PictureFrame pictureFrame = new PictureFrame(pageSelected.PictureLargeURL, pageSelected.Name);
                pictureFrame.Show();
            }
        }

        protected override void InitColumns()
        {
            DataTable.Columns.Add("Name", typeof(string));
            DataTable.Columns.Add("Phone Number", typeof(string));
            DataTable.Columns.Add("Category", typeof(string));
            DataTable.Columns.Add("Description", typeof(string));
            DataTable.Columns.Add("Website", typeof(string));
        }
    }
}
