﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
        }

        private void fetchProfileAndCoverPhotos()
        {
            pictureBoxProfilePicture.LoadAsync(m_LoggedInUser.PictureNormalURL);
            pictureBoxCoverPhoto.LoadAsync(m_LoggedInUser.Cover.SourceURL);
        }

        private void updateFriendsList()
        {
            foreach (User friend in m_LoggedInUser.Friends)
            {
                listBoxFriends.Items.Add(friend.Name);
            }
        }
    }
}
