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
        User m_LoggedInUser;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            loginAndInit();
            FormMain mainForm = new FormMain(m_LoggedInUser);
            this.Hide();
            DialogResult closeMainFormResult = mainForm.ShowDialog();
            Show();
        }

        private void loginAndInit()
        {
            LoginResult result = FacebookWrapper.FacebookService.Login("197501144117907",
                "public_profile",
                "user_education_history",
                "user_birthday",
                "user_actions.video",
                "user_actions.news",
                "user_actions.music",
                "user_actions.fitness",
                "user_actions.books",
                "user_about_me",
                "user_friends",
                "publish_actions",
                "user_events",
                "user_games_activity",
                //"user_groups" (This permission is only available for apps using Graph API version v2.3 or older.)
                "user_hometown",
                "user_likes",
                "user_location",
                "user_managed_groups",
                "user_photos",
                "user_posts",
                "user_relationships",
                "user_relationship_details",
                "user_religion_politics",

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

            if (!string.IsNullOrEmpty(result.AccessToken))
            {
                m_LoggedInUser = result.LoggedInUser;
                //TODO save access token
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        
    }
}
