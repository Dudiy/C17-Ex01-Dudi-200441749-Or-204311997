﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using C17_Ex01_Dudi_200441749_Or_204311997.DataTables;
using System.Drawing;
using System.ComponentModel;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public partial class FormMain : Form
    {
        private FacebookDataTableManager m_DataTableManager;
        private FacebookDataTable m_DataTableBindedToView;
        private FriendshipAnalyzer m_FriendshipAnalyzer;
        private string m_PostPictureURL;
        //TODO set value once we are done with the design
        private static readonly Size sr_MinimumWindowSize = new Size(800, 600);
        private bool m_LogoutClicked = false;

        public FormMain()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            initMainForm();
            fetchProfileAndCoverPhotos();
            // init tabs
            initAboutMeTab();
            initDataTablesTab();
            initFriendshipAnalyzerTab();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!m_LogoutClicked)
            {
                //exitSelected is set here in case where user hits the X button or alt+F4
                FacebookApplication.ExitSelected = true;
                FacebookApplication.AppSettings.LastFormStartPosition = FormStartPosition.Manual;
                FacebookApplication.AppSettings.LastWindowLocation = Location;
                FacebookApplication.AppSettings.LastWindowsSize = Size;
            }
        }

        private void initMainForm()
        {
            Text = FacebookApplication.LoggedInUser.Name;
            labelUserName.Text = FacebookApplication.LoggedInUser.Name;
            MinimumSize = sr_MinimumWindowSize;
        }

        private void fetchProfileAndCoverPhotos()
        {
            try
            {
                pictureBoxProfilePicture.LoadAsync(FacebookApplication.LoggedInUser.PictureNormalURL);
                pictureBoxCoverPhoto.LoadAsync(FacebookApplication.LoggedInUser.Cover.SourceURL);
            }
            catch
            {
                MessageBox.Show("Profile or cover photo missing, default photos were loaded");
            }
        }

        // ================================================ About Me Tab ==============================================
        private void initAboutMeTab()
        {
            updateFriendsList();
            updatePagesList();
            updateEventsList();
            updateBirthday();
        }

        // friends
        private void updateFriendsList()
        {
            listBoxFriends.DisplayMember = "Name";
            comboBoxTagFriend.DisplayMember = "Name";
            foreach (User friend in FacebookApplication.LoggedInUser.Friends)
            {
                listBoxFriends.Items.Add(friend);
                comboBoxTagFriend.Items.Add(friend);
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
            foreach (Page page in FacebookApplication.LoggedInUser.LikedPages)
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
                linkLabelSite.Text = selectedPage.URL;
                linkLabelSite.Name = selectedPage.URL;
                linkLabelSite.Visible = true;
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
            linkLabelSite.Visible = false;
            labelPhoneTitle.Visible = false;
            labelPhone.Visible = false;
            buttonClearPageDetails.Visible = false;
        }

        // Events
        private void updateEventsList()
        {
            listBoxEvents.DisplayMember = "Name";
            foreach (Event upcomingEvent in FacebookApplication.LoggedInUser.Events)
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
                try { labelEventWhere.Text = selectedEvent.Place.Location.City + selectedEvent.Place.Location.Street; }
                catch { labelEventWhere.Text = ""; }
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

        // Birthday
        private void updateBirthday()
        {
            if (FacebookApplication.LoggedInUser.Birthday != null)
            {
                DateTime myBirthday = convertStringToDate(FacebookApplication.LoggedInUser.Birthday);
                DateTime myNextBirthday = new DateTime(DateTime.Now.Year, myBirthday.Month, myBirthday.Day);

                labelMyBirthdayTitle.Text = String.Format(
@"Born on {0}
My birthday is in {1} days",
    myBirthday.ToString("dd/MM/yyyy"),
    (myNextBirthday - DateTime.Now).Days);
            }
            else
            {
                labelMyBirthdayTitle.Visible = false;
            }
        }


        // ================================================ Close form ==============================================
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            m_LogoutClicked = true;
            FacebookApplication.Logout();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            FacebookApplication.ExitSelected = true;
            Close();
        }

        // ================================================ DataTables Tab ==============================================
        private void initDataTablesTab()
        {
            m_DataTableManager = new FacebookDataTableManager();
            initComboBoxDataTableBindingSelection();
        }

        private void initComboBoxDataTableBindingSelection()
        {
            comboBoxDataTableBindingSelection.DisplayMember = "TableName";
            comboBoxDataTableBindingSelection.DataSource = m_DataTableManager.GetDataTables();
        }

        private void buttonFetchData_Click(object sender, EventArgs e)
        {
            fetchDataForDataTablesTab();
        }

        private void fetchDataForDataTablesTab()
        {
            if (comboBoxDataTableBindingSelection.SelectedItem != null)
            {
                dataGridView.DataSource = null;
                m_DataTableBindedToView = (FacebookDataTable)comboBoxDataTableBindingSelection.SelectedItem;
                if (m_DataTableBindedToView is FacebookPhotosDataTable)
                {
                    List<Album> albumsToLoad = getAlbumsToLoadFromUser();
                    ((FacebookPhotosDataTable)m_DataTableBindedToView).AlbumsToLoad = albumsToLoad;
                }
                m_DataTableBindedToView.FetchDataTableValues();

                dataGridView.DataSource = m_DataTableBindedToView.DataTable;
                if (dataGridView.Columns["ObjectDisplayed"] != null)
                {
                    dataGridView.Columns["ObjectDisplayed"].Visible = false;
                }

                if (dataGridView.Columns.Count == 0)
                {
                    MessageBox.Show("The requested table could not be loaded, please try again");
                }
            }
        }

        private List<Album> getAlbumsToLoadFromUser()
        {
            AlbumsSelector albumsSelector = new AlbumsSelector();
            List<Album> selectedAlbums = new List<Album>();

            DialogResult albumsSelected = albumsSelector.ShowDialog();
            if (albumsSelected == DialogResult.Yes)
            {
                selectedAlbums = albumsSelector.SelectedAlbums;
            }

            return selectedAlbums;
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow rowSelected = ((DataGridView)sender).SelectedCells[0].OwningRow;
            rowSelected.Selected = true;
            displayDetailsForRowObject(rowSelected);
        }

        private void displayDetailsForRowObject(DataGridViewRow i_RowSelected)
        {
            object selectedObject = i_RowSelected.Cells["ObjectDisplayed"].Value;
            m_DataTableBindedToView.DisplayObjectDetails(selectedObject);
        }

        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ((DataGridView)sender).SelectedCells[0].OwningRow.Selected = true;
        }
        // ================================================ Friendship analyzer Tab ==============================================
        private void initFriendshipAnalyzerTab()
        {
            m_FriendshipAnalyzer = new FriendshipAnalyzer();
            initComboBoxFriendshipAnalyzer();
            initFriendsPhotosBar();
        }

        private void initComboBoxFriendshipAnalyzer()
        {
            comboBoxFriends.DisplayMember = "Name";
            comboBoxFriends.DataSource = FacebookApplication.LoggedInUser.Friends;
        }

        private void fetchPhotosTaggedTogether()
        {
            List<Photo> taggedTogether = m_FriendshipAnalyzer.FetchPhotosTaggedTogether();
            Dictionary<string, List<Photo>> photos = new Dictionary<string, List<Photo>>();

            foreach (Photo photo in taggedTogether)
            {
                if (photos.ContainsKey(photo.From.Id))
                {
                    photos[photo.From.Id].Add(photo);
                }
                else
                {
                    List<Photo> photoList = new List<Photo>();
                    photoList.Add(photo);
                    photos.Add(photo.From.Id, photoList);
                }
            }

            foreach (KeyValuePair<string, List<Photo>> UserPhotos in photos)
            {
                TreeNode fromNode = new TreeNode(String.Format("Photos by {0}", UserPhotos.Value[0].From.Name));

                fromNode.Tag = UserPhotos.Value[0].From;
                foreach (Photo photo in UserPhotos.Value)
                {
                    TreeNode photoNode = new TreeNode(String.Format("{0} - {1}", photo.CreatedTime.ToString(), photo.Name));
                    photoNode.Tag = photo;
                    fromNode.Nodes.Add(photoNode);
                }

                treeViewTaggedTogether.Nodes.Add(fromNode);
            }
        }

        private void fetchPhotosOfFriendInMyPhotos()
        {
            treeViewPhotosOfFriendInMyPhotos.Nodes.Clear();
            m_FriendshipAnalyzer.Friend = (User)comboBoxFriends.SelectedItem;
            Dictionary<Album, List<Photo>> photos = m_FriendshipAnalyzer.GetPhotosOfMineFriendIsIn();
            foreach (Album album in photos.Keys)
            {
                TreeNode albumNode = new TreeNode(album.Name);
                albumNode.Tag = album;

                foreach (Photo photo in photos[album])
                {
                    string photoDescription = String.Format(@"
{0} - {1}",
photo.CreatedTime.ToString(),
photo.Name != String.Empty ? photo.Name : "No name");
                    TreeNode photoNode = new TreeNode(photoDescription);
                    photoNode.Tag = photo;
                    albumNode.Nodes.Add(photoNode);
                }

                treeViewPhotosOfFriendInMyPhotos.Nodes.Add(albumNode);
            }
        }

        private void treeViewPhotosOfFriendInMyPhotos_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode selectedNode = e.Node as TreeNode;
            Photo selectedPhoto = selectedNode.Tag as Photo;
            if (selectedPhoto != null)
            {
                pictureBox1.LoadAsync(selectedPhoto.PictureThumbURL);
            }

        }

        private void treeViewTaggedTogether_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeViewTaggedTogether.SelectedNode.Tag is User)
            {
                User selectedUser = (User)treeViewTaggedTogether.SelectedNode.Tag;
                PictureFrame profile = new PictureFrame(selectedUser.PictureLargeURL, selectedUser.Name);
                profile.Show();
            }
            else
            {
                Photo selectedPhoto = (Photo)treeViewTaggedTogether.SelectedNode.Tag;
                PhotoDetails photoDetails = new PhotoDetails(selectedPhoto);
                photoDetails.Show();
            }
        }

        private void buttonAnalyzeFriendship_Click(object sender, EventArgs e)
        {
            fetchPhotosOfFriendInMyPhotos();
            fetchPhotosTaggedTogether();
        }

        // ================================================ utils ==============================================
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

        private void linkLabelSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            linkLabelSite.LinkVisited = true;
            // Navigate to a URL.
            System.Diagnostics.Process.Start(linkLabelSite.Text);
        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            // TODO multi tags
            User friend = comboBoxTagFriend.SelectedItem as User;
            string friendID = friend != null ? friend.Id : null;
            // TODO work only with URL of web
            m_PostPictureURL = "https://ibb.co/g5SthF";
            Status postedStatus = FacebookApplication.LoggedInUser.PostStatus(richTextBoxStatusPost.Text,
                i_TaggedFriendIDs: friendID, i_PictureURL: m_PostPictureURL,
                i_Link: m_PostPictureURL);

            MessageBox.Show("Status Posted! ID: " + postedStatus.Id);
        }

        // TODO bug
        private void buttonAddPicture_Click(object sender, EventArgs e)
        {
            //Image file;
            OpenFileDialog file = new OpenFileDialog();

            //file.Filter = "JPG(*.JPG|*.jpg";
            //file.Filter = "PNG(*.PNG|*.png";
            if (file.ShowDialog() == DialogResult.OK)
            {
                m_PostPictureURL = new Uri(file.FileName).AbsoluteUri;
                //m_PostPictureURL = Path.GetFullPath(file.FileName);
                //m_PostPictureURL = Image.FromFile(file.FileName);
            }
        }

        private void initFriendsPhotosBar()
        {
            flowLayoutPanelFriendshipAnalyzer.Width = 110;
            flowLayoutPanelFriendshipAnalyzer.Padding = new Padding(0,0,20,0);
            
            foreach (User friend in FacebookApplication.LoggedInUser.Friends)
            {
                PictureBox profilePic = new PictureBox();
                profilePic.LoadAsync(friend.PictureSqaureURL);
                profilePic.Size = new Size(90, 90);
                profilePic.SizeMode = PictureBoxSizeMode.Zoom;
                profilePic.Tag = friend;
                profilePic.MouseEnter += ProfilePic_MouseEnter;
                profilePic.MouseLeave += ProfilePic_MouseLeave;
                flowLayoutPanelFriendshipAnalyzer.Controls.Add(profilePic);
            }
        }

        private void ProfilePic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox me = sender as PictureBox;
            increasePictureBoxSize(me, -20);
        }

        private void ProfilePic_MouseEnter(object sender, EventArgs e)
        {
            PictureBox me = sender as PictureBox;
            increasePictureBoxSize(me, 20);
        }

        private void increasePictureBoxSize(PictureBox i_PictureBox, int i_Size)
        {
            int newWidth = i_PictureBox.Size.Width + i_Size;
            int newHeight = i_PictureBox.Size.Height + i_Size;
            i_PictureBox.Size = new Size(newWidth, newHeight);
        }
    }
}
