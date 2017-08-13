/*
 * C17_Ex01: FacebookFriendsDataTable.cs
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/
using FacebookWrapper.ObjectModel;
using System;
using System.Text;

namespace C17_Ex01_Dudi_200441749_Or_204311997.DataTables
{
    class FacebookFriendsDataTable : FacebookDataTable
    {
        public FacebookFriendsDataTable()
            : base("Friends", typeof(User))
        { }

        public override void FetchDataTableValues()
        {
            TotalRows = FacebookApplication.LoggedInUser.Friends.Count;
            //add rows
            foreach (User friend in FacebookApplication.LoggedInUser.Friends)
            {
                DataTable.Rows.Add(
                    friend,
                    friend.FirstName,
                    friend.LastName,
                    friend.Gender != null ? friend.Gender.ToString() : String.Empty,
                    getMostRecentPost(friend)
                    );
            }
        }

        public override void DisplayObjectDetails(object i_SelectedObject)
        {
            User friendSelected = i_SelectedObject as User;

            if (friendSelected != null)
            {
                PictureFrame pictureFrame = new PictureFrame(friendSelected.PictureLargeURL, friendSelected.Name);
                pictureFrame.Show();
            }
        }

        protected override void InitColumns()
        {
            DataTable.Columns.Add("First Name", typeof(string));
            DataTable.Columns.Add("Last Name", typeof(string));
            DataTable.Columns.Add("Gender", typeof(string));
            DataTable.Columns.Add("Most Recent Post", typeof(string));
        }

        private string getMostRecentPost(User i_User)
        {
            StringBuilder mostRecentPostStr = new StringBuilder();

            if (i_User != null && i_User.Posts.Count > 0)
            {
                Post mostRecentPost = i_User.Posts[0];
                mostRecentPostStr.Append(mostRecentPost.CreatedTime);
                if (!string.IsNullOrEmpty(mostRecentPost.Message))
                {
                    mostRecentPostStr.Append(String.Format(" - {0}", mostRecentPost.Message));
                }
            }

            return mostRecentPostStr.ToString();
        }

    }
}
