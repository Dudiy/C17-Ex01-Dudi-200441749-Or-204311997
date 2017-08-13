/*
 * C17_Ex01: FacebookLikedPagesDataTable.cs
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;

namespace C17_Ex01_Dudi_200441749_Or_204311997.DataTables
{
    class FacebookLikedPagesDataTable : FacebookDataTable
    {
        public FacebookLikedPagesDataTable()
            : base("Liked Pages", typeof(Page))
        {
        }

        public override IEnumerable<KeyValuePair<int, int>> FetchDataTableValues()
        {
            int currRow = 0;
            TotalRows = FacebookApplication.LoggedInUser.LikedPages.Count;

            //add rows
            foreach (Page page in FacebookApplication.LoggedInUser.LikedPages)
            {
                yield return new KeyValuePair<int, int>(++currRow, TotalRows);
                DataTable.Rows.Add(
                    page,
                    page.Name,
                    page.Phone,
                    page.Category,
                    page.Description,
                    page.Website
                    );
            }
            // we don't need to see the progress here, so we do it in one time
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
