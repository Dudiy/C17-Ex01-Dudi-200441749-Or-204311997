/*
 * C17_Ex01: FacebookDataTable.cs
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/
using System;
using System.Collections.Generic;
using System.Data;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public abstract class FacebookDataTable
    {
        public int TotalRows { get; protected set; }
        public DataTable DataTable { get; protected set; }
        protected Type m_ObjectTypeRepresentedByRow;

        public FacebookDataTable(string i_TableName, Type i_ObjectTypeRepresentedByRow)
        {
            m_ObjectTypeRepresentedByRow = i_ObjectTypeRepresentedByRow;
            DataTable = new DataTable(i_TableName);
            // all tables initialy have a colum that holds the current row object displayed
            DataTable.Columns.Add("ObjectDisplayed", typeof(object));
            InitColumns();
        }

        // using yield, who will use that method know the promoting progress (numbers of done item, numbers of all item)
        public abstract IEnumerable<KeyValuePair<int, int>> FetchDataTableValues();

        public abstract void DisplayObjectDetails(object i_SelectedObject);

        protected abstract void InitColumns();
    }
}
