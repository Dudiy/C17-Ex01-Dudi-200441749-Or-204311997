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
            updateEventsList();

            DateTime myBirthday = convertStringToDate(m_LoggedInUser.Birthday);
            DateTime myNextBirthday = new DateTime(DateTime.Now.Year, myBirthday.Month, myBirthday.Day);

            labelMyBirthdayTitle.Text = String.Format(
@"Born in {0}
My birthday in {1} days",
myBirthday.ToString("dd/MM/yyyy"),
(myNextBirthday - DateTime.Now).Days);
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
        
        // friends
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
                    pictureBoxFriend.LoadAsync(selectedFriend.PictureLargeURL);
                }
                else
                {
                    // TODO add empty user picture
                }
                pictureBoxFriend.Visible = true;
                labelFriendName.Text = selectedFriend.Name;
                labelFriendName.Visible = true;
                labelMailTitle.Visible = true;
                labelMail.Text = selectedFriend.Email != null ? selectedFriend.Email : "Not defined";
                labelMail.Visible = true;
                labelBithdayTitle.Visible = true;
                ;
                labelBirthday.Text = selectedFriend.Birthday != null ? 
                    convertStringToDate(selectedFriend.Birthday).ToString("dd/MM/yyyy") : 
                    "Not defined";
                labelBirthday.Visible = true;
                buttonClearFriendDetails.Visible = true;
            }
        }


        private void buttonClearFriendDetails_Click(object sender, EventArgs e)
        {
            pictureBoxFriend.Visible = false;
            labelFriendName.Visible = false;
            labelMailTitle.Visible = false;
            labelMail.Visible = false;
            labelBithdayTitle.Visible = false;
            labelBirthday.Visible = false;
            buttonClearFriendDetails.Visible = false;
        }

        // liked pages
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
                if (selectedPage.PictureLargeURL != null)
                {
                    pictureBoxPage.LoadAsync(selectedPage.PictureLargeURL);
                }
                else
                {
                    // TODO add empty page
                }
                pictureBoxPage.Visible = true;
                labelPageName.Text = selectedPage.Name;
                labelPageName.Visible = true;
                labelSiteTitle.Visible = true;
                labelSite.Text = selectedPage.URL;
                labelSite.Visible = true;
                labelPhoneTitle.Visible = true;
                labelPhone.Text = selectedPage.Phone != null ? selectedPage.Phone : "Not defined";
                labelPhone.Visible = true;
                buttonClearPageDetails.Visible = true;
            }
        }

        private void buttonClearPageDetails_Click(object sender, EventArgs e)
        {
            pictureBoxPage.Visible = false;
            labelPageName.Visible = false;
            labelSiteTitle.Visible = false;
            labelSite.Visible = false;
            labelPhoneTitle.Visible = false;
            labelPhone.Visible = false;
            buttonClearPageDetails.Visible = false;
        }

        // Events
        private void updateEventsList()
        {
            listBoxEvents.DisplayMember = "Name";
            foreach (Event upcomingEvent in m_LoggedInUser.Events)
            {
                listBoxEvents.Items.Add(upcomingEvent);
            }
        }

        private void listBoxEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxEvents.SelectedItems.Count == 1)
            {
                Event selectedEvent = listBoxEvents.SelectedItem as Event;
                if (selectedEvent.PictureLargeURL != null)
                {
                    pictureBoxEvent.LoadAsync(selectedEvent.PictureLargeURL);
                }
                else
                {
                    // TODO add empty event
                }
                pictureBoxEvent.Visible = true;
                labelEventName.Text = selectedEvent.Name;
                labelEventName.Visible = true;
                labelEventWhenTitle.Visible = true;
                labelEventWhen.Text = selectedEvent.TimeString;
                labelEventWhen.Visible = true;
                labelEventWhereTitle.Visible = true;
                labelEventWhere.Text = selectedEvent.Place.Location.City + selectedEvent.Place.Location.Street;
                labelEventWhere.Visible = true;
                buttonClearEventDetails.Visible = true;
            }
        }

        private void buttonClearEventDetails_Click(object sender, EventArgs e)
        {
            pictureBoxEvent.Visible = false;
            labelEventName.Visible = false;
            labelEventWhenTitle.Visible = false;
            labelEventWhen.Visible = false;
            labelEventWhereTitle.Visible = false;
            labelEventWhere.Visible = false;
            buttonClearEventDetails.Visible = false;
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



        ////////////////// utils

        private DateTime convertStringToDate(string i_Birthdate)
        {
            string[] splitDate = i_Birthdate.Split('/');
            DateTime date = new DateTime(
                int.Parse(splitDate[2]),
                int.Parse(splitDate[0]),
                int.Parse(splitDate[1])
                );

            return date;
        }

    }
}
