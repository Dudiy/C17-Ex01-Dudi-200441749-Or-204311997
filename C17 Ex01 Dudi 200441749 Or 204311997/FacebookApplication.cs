using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public static class FacebookApplication
    {
        public static User LoggedInUser { get; private set; }
        public static AppSettings AppSettings { get; private set; }
        public static bool ExitSelected { get; set; }
        public static void Run()
        {
            ExitSelected = false;
            AppSettings = AppSettings.LoadFromFile();
            while (!ExitSelected)
            {
                loginToFacebook();
                if (!ExitSelected && LoggedInUser != null)
                {
                    showMainForm();
                }
            }
            //We get here only after ExitSelected == true
            exitApplication();
        }

        private static void showMainForm()
        {
            FormMain mainForm = new FormMain();

            mainForm.StartPosition = FormStartPosition.Manual;
            mainForm.Location = AppSettings.LastWindowsLocation;
            mainForm.Size = AppSettings.LastWindowsSize;
            mainForm.ShowDialog();
        }

        private static void exitApplication()
        {
            if (!AppSettings.RememberUser)
            {
                AppSettings.SetDefaultSettings();
            }

            AppSettings.SaveToFile();
        }

        private static void loginToFacebook()
        {
            LoginResult loginResult = null;

            try
            {
                if (AppSettings.RememberUser &&
                    !String.IsNullOrEmpty(AppSettings.LastAccessToken))
                {
                    loginResult = FacebookService.Connect(AppSettings.LastAccessToken);
                }
                else
                {
                    loginResult = loginWithForm();
                }
            }
            catch
            {
                MessageBox.Show("Error logging in to Facebook, please try again");
                //TODO what if an error is thrown from this call to loginWithForm?
                loginWithForm();
            }

            if (loginResult != null)
            {
                AppSettings.LastAccessToken = loginResult.AccessToken;
                LoggedInUser = loginResult.LoggedInUser;
            }
        }

        private static LoginResult loginWithForm()
        {
            FormLogin formLogin = new FormLogin();
            DialogResult loginSuccesfull = DialogResult.No;

            while (loginSuccesfull != DialogResult.Yes &&
                loginSuccesfull != DialogResult.Cancel)
            {
                loginSuccesfull = formLogin.ShowDialog();
                if (loginSuccesfull == DialogResult.Cancel)
                {
                    ExitSelected = true;
                }
                else if (loginSuccesfull != DialogResult.Yes)
                {
                    MessageBox.Show("Login failed, try again");
                }
            }


            // TODO do we need to save to "res" in order to keep the value after fomLogin is disposed?
            LoginResult res = formLogin.LoginResult;
            return res;
        }
    }
}
