/*
 * C17_Ex01: FacebookDataTable.cs
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/
using System;
using System.Data;
using FacebookWrapper.ObjectModel;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public abstract class FacebookDataTable
    {
        public int TotalRows { get; protected set; }
        public DataTable DataTable { get; protected set; }
        // TODO delete after implementing singleton
        //protected User m_LoggedInUser = FacebookApplication.LoggedInUser;
        protected Type m_ObjectTypeRepresentedByRow;
        public bool DataFetched { get; protected set; }

        public FacebookDataTable(string i_TableName, Type i_ObjectTypeRepresentedByRow)
        {
            m_ObjectTypeRepresentedByRow = i_ObjectTypeRepresentedByRow;
            DataTable = new DataTable(i_TableName);
            DataFetched = false;
            // all tables initialy have a colum that holds the current row object displayed
            DataTable.Columns.Add("ObjectDisplayed", typeof(object));
            initColumns();
        }

        public string TableName
        {
            get { return DataTable.TableName; }
        }

        public abstract void FetchDataTableValues();

        public abstract void DisplayObjectDetails(object i_SelectedObject);

        protected abstract void initColumns();
    }
}
