using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    static class FacebookObjectDisplayer
    {
        public static void Display(IDisplayable i_ObjectToDisplay)
        {
            object objectToDisplay = i_ObjectToDisplay.ObjectToDisplay;
            if (objectToDisplay is Photo)
            {

            }
            else if (objectToDisplay is User)
            {

            }
            else if (objectToDisplay is Page)
            {

            }
            else
            {
                MessageBox.Show(String.Format("Showing toString(): ",objectToDisplay.ToString()));
            }
        }
    }
}
