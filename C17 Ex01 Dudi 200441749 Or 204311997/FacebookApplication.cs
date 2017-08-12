using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System;
using System.Windows.Forms;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public static class FacebookApplication
    {
        public static User LoggedInUser { get; private set; }
        public static AppSettings AppSettings { get; private set; }
        public static bool ExitSelected { get; set; }
        private static bool isFirstLogoutCall = true;
        private static Form m_MainForm;
        public const byte k_MaxPhotosInAlbum = 100;

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

        // used as a method to call after successfuly invoking FacebookService.Logout
        public static void Logout()
        {
            FacebookService.Logout(logoutSuccessful);
        }

        private static void showMainForm()
        {
            m_MainForm = new FormMain();
            m_MainForm.Size = AppSettings.LastWindowsSize;
            m_MainForm.StartPosition = AppSettings.LastFormStartPosition;
            m_MainForm.Location = AppSettings.LastWindowLocation;
            m_MainForm.ShowDialog();
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
            bool loginSuccessful = false;
            bool isFirstLoginAttempt = true;

            while (!loginSuccessful)
            {
                try
                {
                    if (AppSettings.RememberUser &&
                        !String.IsNullOrEmpty(AppSettings.LastAccessToken) &&
                        isFirstLoginAttempt)
                    {
                        loginResult = FacebookService.Connect(AppSettings.LastAccessToken);
                    }
                    else
                    {
                        loginResult = loginWithForm();
                    }

                    if (loginResult == null)
                    {
                        throw new Exception("Login returned null");
                    }

                    loginSuccessful = true;
                }
                catch
                {
                    MessageBox.Show("Error logging in to Facebook, please try again");
                    isFirstLoginAttempt = false;
                }
            }

            AppSettings.LastAccessToken = loginResult.AccessToken;
            LoggedInUser = loginResult.LoggedInUser;
        }

        private static LoginResult loginWithForm()
        {
            FormLogin formLogin = new FormLogin();
            DialogResult loginSuccessful = DialogResult.No;

            while (loginSuccessful != DialogResult.Yes &&
                loginSuccessful != DialogResult.Cancel)
            {
                loginSuccessful = formLogin.ShowDialog();
                if (loginSuccessful == DialogResult.Cancel)
                {
                    ExitSelected = true;
                }
                else if (loginSuccessful != DialogResult.Yes)
                {
                    MessageBox.Show("Login failed, try again");
                }
            }

            return formLogin.LoginResult;
        }

        private static void logoutSuccessful()
        {
            // this is a patch to fix bug in facebookWrapper where this method is called twice when Logout is invoked            
            if (isFirstLogoutCall)
            {
                AppSettings.SetDefaultSettings();
                MessageBox.Show(String.Format("{0} logged out", LoggedInUser.Name));
                LoggedInUser = null;
                closeAllForms();
            }

            // toggle isFirstLogoutCall
            isFirstLogoutCall = isFirstLogoutCall ? false : true;
        }

        private static void closeAllForms()
        {
            int numOfOpenForms = Application.OpenForms.Count;
            for (int i = numOfOpenForms; i > 0; i--)
            {
                Application.OpenForms[i - 1].Close();
            }
        }
    }
}
