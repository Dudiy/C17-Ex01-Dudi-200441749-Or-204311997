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

        public override void fetchDataTableValues()
        {
            if (DataFetched == false || DataTable.Rows.Count == 0)
            {
                TotalRows = m_LoggedInUser.Friends.Count;
                //init columns
                DataTable.Columns.Add("First Name", typeof(string));
                DataTable.Columns.Add("Last Name", typeof(string));

                //add rows
                foreach (User friend in m_LoggedInUser.Friends)
                {
                    DataTable.Rows.Add(
                        friend,
                        friend.FirstName,
                        friend.LastName
                        );
                }
            }
            DataFetched = true;
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
    }
}
