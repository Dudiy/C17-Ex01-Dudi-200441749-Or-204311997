using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C17_Ex01_Dudi_200441749_Or_204311997.DataTables
{
    class FacebookFriendsDataTable : FacebookDataTable
    {
        public FacebookFriendsDataTable(User i_LoggedInUser)
            : base(i_LoggedInUser, "Friends", typeof(User))
        {
        }

        public override void FetchDataTableValues()
        {
            if (DataFetched == false || DataTable.Rows.Count == 0)
            {
                TotalRows = m_LoggedInUser.Friends.Count;

                //add rows
                foreach (User friend in m_LoggedInUser.Friends)
                {
                    DataTable.Rows.Add(
                        friend,
                        friend.FirstName,
                        friend.LastName,
                        friend.Gender != null ? friend.Gender.ToString() : String.Empty
                        //getMostRecentPost(friend)
                        );
                }
            }
            DataFetched = true;
        }

        private string getMostRecentPost(User i_User)
        {
            StringBuilder mostRecentPostStr = new StringBuilder();

            if (i_User != null && i_User.Posts[0] != null)
            {
                Post mostRecentPost = i_User.Posts[0];
                mostRecentPostStr.Append(mostRecentPost.CreatedTime);
                if (!string.IsNullOrEmpty(mostRecentPost.Message))
                {
                    mostRecentPostStr.Append(mostRecentPost.Message);
                }
            }

            return mostRecentPostStr.ToString();
        }

        public override void OnRowDoubleClicked(object i_SelectedObject)
        {
            User friendSelected = i_SelectedObject as User;

            if (friendSelected != null)
            {
                PictureFrame pictureFrame = new PictureFrame(friendSelected.PictureLargeURL, friendSelected.Name);
                pictureFrame.Show();
            }
        }

        protected override void initColumns()
        {
            DataTable.Columns.Add("First Name", typeof(string));
            DataTable.Columns.Add("Last Name", typeof(string));
            DataTable.Columns.Add("Gender", typeof(string));
            //DataTable.Columns.Add("Most Recent Post", typeof(string));
        }
    }
}
