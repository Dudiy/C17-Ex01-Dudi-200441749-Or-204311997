﻿using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public partial class FriendDetails : Form
    {
        private User m_Friend;

        public FriendDetails(User i_Friend)
        {
            InitializeComponent();
            m_Friend = i_Friend;
            init();
        }

        private void init()
        {
            userBindingSource.DataSource = m_Friend;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            likedPagesBindingSource.DataSource = ((ListBox)sender).SelectedItem as Page;
        }

        private void uRLLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            uRLLinkLabel.LinkVisited = true;
            // Navigate to a URL.
            System.Diagnostics.Process.Start(uRLLinkLabel.Text);
        }
    }
}