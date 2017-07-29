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
    public partial class FormMain : Form
    {
        private User m_LoggedInUser;

        public FormMain(User i_LoggedInUser)
        {
            InitializeComponent();
            m_LoggedInUser = i_LoggedInUser;
            initMainForm();
        }

        private void initMainForm()
        {
            labelUserName.Text = m_LoggedInUser.Name;
            fetchProfileAndCoverPhotos();
            updateFriendsList();
            updatePagesList();

            textBoxBirthdate.Text = m_LoggedInUser.Birthday;
        }

        private void fetchProfileAndCoverPhotos()
        {
            // TODO check if there are pic
            if (m_LoggedInUser.PictureNormalURL != null)
            {
                pictureBoxProfilePicture.LoadAsync(m_LoggedInUser.PictureNormalURL);
            }
            else
            {
                // TODO add empty user picture
            }

            if (m_LoggedInUser.Cover != null)
            {
                pictureBoxCoverPhoto.LoadAsync(m_LoggedInUser.Cover.SourceURL);
            }
        }

        // ================================================= tab1==================
        private void updateFriendsList()
        {
            listBoxFriends.DisplayMember = "Name";
            foreach (User friend in m_LoggedInUser.Friends)
            {
                listBoxFriends.Items.Add(friend);
            }
        }

        private void listBoxFriends_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFriends.SelectedItems.Count == 1)
            {
                User selectedFriend = listBoxFriends.SelectedItem as User;
                if (selectedFriend.PictureLargeURL != null)
                {
                    pictureBoxFriend.LoadAsync(selectedFriend.PictureNormalURL);
                    labelFriendName.Text = selectedFriend.Name;
                    labelFriendName.Visible = true;
                    labelMailTitle.Visible = true;
                    labelMail.Text = selectedFriend.Email != null ? selectedFriend.Email  : "Not defined";
                    labelMail.Visible = true;
                    labelBithdateTitle.Visible = true;
                    labelBirthdate.Text = selectedFriend.Birthday != null ? selectedFriend.Birthday : "Not defined";
                    labelBirthdate.Visible = true;

                }
                else
                {
                    // TODO add empty user picture
                }
            }
        }

        private void buttonSendMessage_Click(object sender, EventArgs e)
        {
        }

        private void updatePagesList()
        {
            listBoxLikedPages.DisplayMember = "Name";
            foreach (Page page in m_LoggedInUser.LikedPages)
            {
                listBoxLikedPages.Items.Add(page);
            }
        }

        private void listBoxLikedPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxLikedPages.SelectedItems.Count == 1)
            {
                Page selectedPage = listBoxLikedPages.SelectedItem as Page;
                if (selectedPage.PictureSqaureURL != null)
                {
                    pictureBoxPage.LoadAsync(selectedPage.PictureSqaureURL);
                }
                else
                {
                    // TODO add empty user picture
                }
            }
        }

        // ===============================================================================================
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            FacebookService.Logout(notifyLogout);
        }

        private void notifyLogout()
        {
            MessageBox.Show("Logged Out");
            Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
