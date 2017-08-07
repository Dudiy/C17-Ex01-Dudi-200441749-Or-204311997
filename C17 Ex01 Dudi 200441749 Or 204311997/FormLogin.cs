using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public partial class FormLogin : Form
    {
        private User m_LoggedInUser;
        private FormMain m_MainForm;
        public AppSettings AppSettings { get; set; }

        public FormLogin()
        {
            Hide();
            InitializeComponent();

        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // load settings from file
            AppSettings = AppSettings.LoadFromFile();
            if (AppSettings != null && !string.IsNullOrEmpty(AppSettings.LastAccessToken))
            {
                LoginResult resultLogin = FacebookService.Connect(AppSettings.LastAccessToken);
                showLoginUser(resultLogin);
            }
            else
            {
                Show();
            }
        }

        private void showLoginUser(LoginResult i_LoginResult)
        {
            m_MainForm = new FormMain();
            if (AppSettings == null)
            {
                AppSettings = AppSettings.Instance;
                AppSettings.DefaultSettings(m_MainForm);
            }

            if (!string.IsNullOrEmpty(i_LoginResult.AccessToken))
            {
                // TODO bug if logout and login with same user, the access token are different 
                if(!string.IsNullOrEmpty(AppSettings.LastAccessToken ) &&
                    AppSettings.LastAccessToken != i_LoginResult.AccessToken)
                {
                    AppSettings.DefaultSettings(m_MainForm);
                }
                AppSettings.LastAccessToken = i_LoginResult.AccessToken;
            }

            Hide();
            DialogResult dialogResultMainForm = m_MainForm.ShowDialog();
            
            if(dialogResultMainForm == DialogResult.Ignore)
            {
                Show();
            }
            else
            {
                Close();
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            loginAndInit();
        }

        // TODO see which permission we need
        private void loginAndInit()
        {
            //FacebookWrapper.FacebookService.Logout(Show);
            LoginResult resultLogin = FacebookWrapper.FacebookService.Login("197501144117907",
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
            showLoginUser(resultLogin);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
