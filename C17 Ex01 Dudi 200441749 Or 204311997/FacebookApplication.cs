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
        private static bool isFirstLogoutCall = true;
        private static Form m_MainForm;
        public static void Run()
        {
            FacebookService.s_CollectionLimit = 500;
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
            //We get here only after ExitSelected is true
            exitApplication();
        }

        private static void showMainForm()
        {
            m_MainForm = new FormMain();
            m_MainForm.Size = AppSettings.LastWindowsSize;
            m_MainForm.StartPosition = AppSettings.LastFormStartPosition;
            m_MainForm.Location = AppSettings.LastWindowLocation;
            m_MainForm.ShowDialog();
        }

        // used as a method to call after succesfully invoking FacebookService.Logout
        public static void Logout()
        {
            // this is a patch to fix bug in facebookWrapper where this method is called twice when Logout is invoked            
            if (isFirstLogoutCall)
            {
                AppSettings.SetDefaultSettings();
                MessageBox.Show("{0} logged out", LoggedInUser.Name);
                LoggedInUser = null; 
                m_MainForm.Close();
            }

            // toggle isFirstLogoutCall
            isFirstLogoutCall = isFirstLogoutCall ? false : true;
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
