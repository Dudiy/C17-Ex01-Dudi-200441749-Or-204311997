using FacebookWrapper.ObjectModel;
using System;
using System.Data;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    internal abstract class FacebookDataTable
    {
        public int TotalRows { get; protected set; }
        public DataTable DataTable { get; protected set; }
        // TODO delete after implementing singleton
        protected User m_LoggedInUser;
        protected Type m_ObjectTypeRepresentedByRow;
        public bool DataFetched { get; protected set; }

        public FacebookDataTable(User i_LoggedInUser, string i_TableName, Type i_ObjectTypeRepresentedByRow)
        {
            m_LoggedInUser = i_LoggedInUser;
            m_ObjectTypeRepresentedByRow = i_ObjectTypeRepresentedByRow;
            DataTable = new DataTable(i_TableName);
            DataFetched = false;
            // all tables initialy have a colum that holds the current row object displayed
            DataTable.Columns.Add("ObjectDisplayed", typeof(object));
        }

        public string TableName
        {
            get { return DataTable.TableName; }
        }

        public abstract void fetchDataTableValues();

        public abstract void OnRowDoubleClicked(object i_SelectedObject);
    }
}
