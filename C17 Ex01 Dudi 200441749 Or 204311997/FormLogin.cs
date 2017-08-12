/*
 * C17_Ex01: FormLogin.cs
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/
using System;
using System.Windows.Forms;
using FacebookWrapper;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public partial class FormLogin : Form
    {
        public LoginResult LoginResult { get; private set; }
        public FormLogin()
        {
            InitializeComponent();
            checkBoxRememberMe.Checked = FacebookApplication.AppSettings.RememberUser;
        }
        
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
            LoginResult = FacebookWrapper.FacebookService.Login("197501144117907",
                "public_profile",
                "email",
                "user_birthday",
                "user_actions.news",
                "user_about_me",
                "user_friends",
                "publish_actions",
                "user_events",
                "user_hometown",
                "user_likes",
                "user_location",
                "user_managed_groups",
                "user_photos",
                "user_posts",
                "user_status",
                "user_relationships",
                "user_relationship_details",
                "user_tagged_places",
                "user_videos",
                "user_website",
                "user_work_history",
                "read_custom_friendlists",
                "read_page_mailboxes",
                "manage_pages",
                "publish_pages",
                "publish_actions",
                "rsvp_event"
                );
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            FacebookApplication.ExitSelected = true;
            Close();
        }

        private void checkBoxRememberMe_CheckedChanged(object sender, EventArgs e)
        {
            FacebookApplication.AppSettings.RememberUser = checkBoxRememberMe.Checked;
        }
    }
}
