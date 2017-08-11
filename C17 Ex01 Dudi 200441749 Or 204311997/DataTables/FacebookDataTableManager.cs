using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C17_Ex01_Dudi_200441749_Or_204311997.DataTables;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    class FacebookDataTableManager
    {
        private List<FacebookDataTable> m_DataTables = new List<FacebookDataTable>();
        // TODO delete after implementing singleton
        private User m_LoggedInUser = FacebookApplication.LoggedInUser;
        public FacebookDataTableManager()
        {
            m_DataTables.Add(new FacebookFriendsDataTable());
            m_DataTables.Add(new FacebookLikedPagesDataTable());
            m_DataTables.Add(new FacebookPhotosDataTable());
        }

        public FacebookDataTable[] GetDataTables()
        {
            return m_DataTables.ToArray();
        }
    }
}
