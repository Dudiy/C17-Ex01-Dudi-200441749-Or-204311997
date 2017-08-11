using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormLogin());
            FacebookApplication.Run();
            //FormLogin formLogin = new FormLogin();
            //formLogin.ShowDialog();
            //formLogin.Close();
        }
    }
}
