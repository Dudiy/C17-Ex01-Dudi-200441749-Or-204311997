using System;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System.Drawing;
using System.ComponentModel;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public partial class FormLogin : Form
    {
        //public static User LoggedInUser { get; private set; }
        public LoginResult LoginResult { get; private set; }
        //private FormMain m_MainForm;
        //public static AppSettings AppSettings { get; set; }
        public FormLogin()
        {
            InitializeComponent();
            checkBoxRememberMe.Checked = FacebookApplication.AppSettings.RememberUser;
        }

        //protected override void OnShown(EventArgs e)
        //{
        //    base.OnShown(e);
        //    // load settings from file
        //    //AppSettings = AppSettings.LoadFromFile();
        //    //if (FacebookApplication.AppSettings.RememberUser && 
        //    //    !string.IsNullOrEmpty(FacebookApplication.AppSettings.LastAccessToken))
        //    //{
        //    //    //try
        //    //    //{
        //    //    //    //LoginResult resultLogin = FacebookService.Connect(FacebookApplication.AppSettings.LastAccessToken);
        //    //    //    //FacebookApplication.LoggedInUser = resultLogin.LoggedInUser;
        //    //    //    //showMainForm();
        //    //    //}
        //    //    //catch
        //    //    //{
        //    //    //    MessageBox.Show("Error logging in, please check internet connection and try again");
        //    //    //}
        //    //    ////showLoginUser(resultLogin);
        //    //}
        //    //else
        //    //{
        //    //    Show();
        //    //}
        //}

        //private void showMainForm()
        //{
        //    this.Hide();
        //    m_MainForm = new FormMain();
        //    m_MainForm.StartPosition = FormStartPosition.Manual;
        //    m_MainForm.Location = AppSettings.LastWindowsLocation;
        //    m_MainForm.Size = AppSettings.LastWindowsSize;
        //    DialogResult userLoggedOut = m_MainForm.ShowDialog();
        //    onMainFormClosed(userLoggedOut);
        //}

        //private void onMainFormClosed(DialogResult i_UserLoggedOut)
        //{
        //    if (i_UserLoggedOut == DialogResult.Yes)
        //    {
        //        // if the main form is closed because the user logged out
        //        this.Show();
        //        checkBoxRememberMe.Checked = false;
        //        AppSettings.SetDefaultSettings();
        //        LoggedInUser = null;
        //    }
        //    else
        //    {
        //        // when the main form is closed without the user logging out (the user exits)
        //        // the login form is closed which closes the application
        //        this.Close();
        //    }
        //}

        //private void showLoginUser(LoginResult i_LoginResult)
        //{
        //    m_MainForm = new FormMain();

        //    if (AppSettings == null)
        //    {
        //        AppSettings = AppSettings.Instance;
        //        //AppSettings.DefaultSettings(m_MainForm);
        //    }

        //    if (!string.IsNullOrEmpty(i_LoginResult.AccessToken))
        //    {
        //        // TODO bug if logout and login with same user, the access token are different 
        //        if (!string.IsNullOrEmpty(AppSettings.LastAccessToken) &&
        //            AppSettings.LastAccessToken != i_LoginResult.AccessToken)
        //        {
        //            AppSettings.DefaultSettings(m_MainForm);
        //        }
        //        AppSettings.LastAccessToken = i_LoginResult.AccessToken;
        //    }

        //    Hide();
        //    DialogResult dialogResultMainForm = m_MainForm.ShowDialog();

        //    if (dialogResultMainForm == DialogResult.Ignore)
        //    {
        //        Show();
        //    }
        //    else
        //    {
        //        Close();
        //    }
        //}

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                login();
                DialogResult = DialogResult.Yes;
            }
            catch
            {
                this.Show();
                MessageBox.Show("Error logging in, please check internet connection and try again");
            }
        }

        // TODO see which permission we need
        private void login()
        {
            //FacebookWrapper.FacebookService.Logout(Show);
            LoginResult = FacebookWrapper.FacebookService.Login("197501144117907",
                "public_profile",
                "email",
                //"user_education_history",
                "user_birthday",
                //"user_actions.video",
                "user_actions.news",
                //"user_actions.music",
                //"user_actions.fitness",
                //"user_actions.books",
                "user_about_me",
                "user_friends",
                "publish_actions",
                "user_events",
                //"user_games_activity",
                //"user_groups" (This permission is only available for apps using Graph API version v2.3 or older.)
                "user_hometown",
                "user_likes",
                "user_location",
                "user_managed_groups",
                "user_photos",
                "user_posts",
                "user_status",
                "user_relationships",
                "user_relationship_details",
                //"user_religion_politics",

                //"user_status" (This permission is only available for apps using Graph API version v2.3 or older.)
                "user_tagged_places",
                "user_videos",
                "user_website",
                "user_work_history",
                "read_custom_friendlists",

                // "read_mailbox", (This permission is only available for apps using Graph API version v2.3 or older.)
                "read_page_mailboxes",
                // "read_stream", (This permission is only available for apps using Graph API version v2.3 or older.)
                // "manage_notifications", (This permission is only available for apps using Graph API version v2.3 or older.)
                "manage_pages",
                "publish_pages",
                "publish_actions",

                "rsvp_event"
                );
            //showLoginUser(resultLogin);            
            //FacebookApplication.AppSettings.LastAccessToken = loginResult.AccessToken;
            //LoggedInUser = loginResult.LoggedInUser;
            //showMainForm();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            FacebookApplication.ExitSelected = true;
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (FacebookApplication.AppSettings.RememberUser)
            {
                FacebookApplication.AppSettings.LastWindowsLocation = Location;
                FacebookApplication.AppSettings.LastWindowsSize = Size;
            }
            //// TODO if logout and exit is the same in that case
            //AppSettings.RememberUser = checkBoxRememberMe.Checked;
            //// if rememberMe is false set settings to default before saving
            //if (!checkBoxRememberMe.Checked)
            //{
            //    AppSettings.SetDefaultSettings();
            //}

            //AppSettings.SaveToFile();
        }
    }
}
