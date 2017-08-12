/*
 * C17_Ex01: FriendDetails.cs
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/
using FacebookWrapper.ObjectModel;
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
            userBindingSource.DataSource = m_Friend;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            labelLikedPage.Text = string.Format(
@"Look about pages that
{0} liked",
m_Friend.FirstName);
            if (labelBirthday.Text == "")
            {
                labelBirthdayTitle.Visible = false;
                labelBirthday.Visible = false;
            }
        }
        private void listBoxLikedPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            likedPagesBindingSource.DataSource = ((ListBox)sender).SelectedItem as Page;
        }

        private void linkLabelLikedPageURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            linkLabelLikedPageURL.LinkVisited = true;
            // Navigate to a URL.
            System.Diagnostics.Process.Start(linkLabelLikedPageURL.Text);
        }
    }
}
