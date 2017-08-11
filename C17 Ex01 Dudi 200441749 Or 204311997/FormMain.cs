using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System.Threading;
using C17_Ex01_Dudi_200441749_Or_204311997.DataTables;
using System.Drawing;
using System.IO;
using System.ComponentModel;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public partial class FormMain : Form
    {
        private FacebookDataTableManager m_DataTableManager;
        private FacebookDataTable m_DataTableBindedToView;
        private FriendshipAnalyzer m_FriendshipAnalyzer;
        public AppSettings AppSettings { get; private set; } = AppSettings.Instance;
        //public User LoggedInUser { get; private set; }
        public bool RememberMe { get; set; }
        private string m_PostPictureURL;

        public FormMain()
        {
            InitializeComponent();
            // TODO delete, in order to save center position in default settings
            //LoggedInUser = FormLogin.LoggedInUser;
            CenterToScreen();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            //initLastUseSettings();
            FacebookService.s_CollectionLimit = 500;
            //LoggedInUser = FacebookService.Connect(AppSettings.LastAccessToken).LoggedInUser;
            initMainForm();
            fetchProfileAndCoverPhotos();
            //Thread.Sleep(100000);
            // init tabs
            initAboutMeTab();
            initDataTablesTab();
            initFriendshipAnalyzerTab();
        }

        private void initMainForm()
        {
            // init global form
            Text = FacebookApplication.LoggedInUser.Name;
            labelUserName.Text = FacebookApplication.LoggedInUser.Name;

            //MinimumSize = new System.Drawing.Size(Size.Width, Size.Height);
        }

        private void fetchProfileAndCoverPhotos()
        {
            // TODO check if there are pic
            if (FacebookApplication.LoggedInUser.PictureNormalURL != null)
            {
                pictureBoxProfilePicture.LoadAsync(FacebookApplication.LoggedInUser.PictureNormalURL);
                pictureBoxProfilePicture.Visible = true;
            }
            else
            {
                // TODO add empty user picture
            }

            if (FacebookApplication.LoggedInUser.Cover != null)
            {
                pictureBoxCoverPhoto.LoadAsync(FacebookApplication.LoggedInUser.Cover.SourceURL);
                pictureBoxCoverPhoto.Visible = true;
            }
        }

        private void initLastUseSettings()
        {
            //AppSettings = AppSettings.Instance;
            //StartPosition = FormStartPosition.Manual;
            //Location = AppSettings.LastWindowsLocation;
            //Size = AppSettings.LastWindowsSize;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            //TODO should we set exitSelected here? for the case where user hits the X button
            FacebookApplication.ExitSelected = true;
            FacebookApplication.AppSettings.LastWindowsLocation = Location;
            FacebookApplication.AppSettings.LastWindowsSize = Size;
        }
        //protected override void OnFormClosing(FormClosingEventArgs e)
        //{
        //    base.OnFormClosing(e);
        //    AppSettings.LastWindowsLocation = Location;
        //    AppSettings.LastWindowsSize = Size;
        //}

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
    @"Born in {0}
My birthday in {1} days",
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
            FacebookService.Logout(null);
            FacebookApplication.AppSettings.SetDefaultSettings();
            //DialogResult = DialogResult.Yes;
            this.Close();
            // TODO bug after logout and try to login 
            //FacebookService.Logout(notifyLogout);
        }

        // TODO call twice
        private void notifyLogout()
        {
            MessageBox.Show("Logged Out");
            Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            FacebookApplication.ExitSelected = true;
            DialogResult = DialogResult.No;
            Close();
        }

        // ================================================ DataTables Tab ==============================================
        private void initDataTablesTab()
        {
            m_DataTableManager = new FacebookDataTableManager(FacebookApplication.LoggedInUser);
            initComboBoxDataTableBindingSelection();
        }

        private void initComboBoxDataTableBindingSelection()
        {
            comboBoxDataTableBindingSelection.DisplayMember = "TableName";

            foreach (FacebookDataTable facebookDataTable in m_DataTableManager.GetDataTables())
            {
                comboBoxDataTableBindingSelection.Items.Add(facebookDataTable);
            }
        }

        private void buttonFetchData_Click(object sender, EventArgs e)
        {
            if (comboBoxDataTableBindingSelection.SelectedItem != null)
            {
                dataGridView.DataSource = null;
                m_DataTableBindedToView = (FacebookDataTable)comboBoxDataTableBindingSelection.SelectedItem;
                //if (!m_DataTableBindedToView.DataFetched)
                //{
                if (m_DataTableBindedToView is FacebookPhotosDataTable)
                {
                    List<Album> albumsToLoad = getAlbumsToLoadFromUser();
                    ((FacebookPhotosDataTable)m_DataTableBindedToView).AlbumsToLoad = albumsToLoad;
                }
                m_DataTableBindedToView.FetchDataTableValues();
                //}

                dataGridView.DataSource = m_DataTableBindedToView.DataTable;
                if (dataGridView.Columns.Count > 0)
                {
                    if (dataGridView.Columns["ObjectDisplayed"] != null)
                    {
                        dataGridView.Columns["ObjectDisplayed"].Visible = false;
                    }

                    //m_DataTableBindedToView = (FacebookDataTable)comboBoxDataTables.SelectedItem;
                    String toolStripMessage = String.Format(@"
Fetching {0} data from server ... {1:P0} Complete   ",
    m_DataTableBindedToView.TableName,
    (float)m_DataTableBindedToView.DataTable.Rows.Count / m_DataTableBindedToView.TotalRows);
                    updateToolStrip(toolStripMessage);
                }
                else
                {
                    String toolstripMessage = String.Format("The requested table could not be loaded, please try again");
                    updateToolStrip(toolstripMessage);
                }
            }
        }

        private List<Album> getAlbumsToLoadFromUser()
        {
            List<Album> selectedAlbums = new List<Album>();
            AlbumsSelector albumsSelector = new AlbumsSelector(FacebookApplication.LoggedInUser);

            DialogResult dialogResult = albumsSelector.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                selectedAlbums = albumsSelector.SelectedAlbums;
            }

            return selectedAlbums;
        }

        private void updateToolStrip(string i_ToolstripMessage)
        {

            //progress bar
            toolStripProgressBar.Minimum = 0;
            toolStripProgressBar.Maximum = m_DataTableBindedToView.TotalRows;
            toolStripProgressBar.Value = m_DataTableBindedToView.DataTable.Rows.Count;
            toolStripProgressBar.Visible =
                toolStripProgressBar.Value < toolStripProgressBar.Maximum ? true : false;
            // message
            toolStripLabelMessage.Text = i_ToolstripMessage;
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            object selectedObject = ((DataGridView)sender).SelectedCells[0].OwningRow.Cells["ObjectDisplayed"].Value;
            m_DataTableBindedToView.OnRowDoubleClicked(selectedObject);
        }

        // ================================================ Friendship analyzer Tab ==============================================
        private void initFriendshipAnalyzerTab()
        {
            m_FriendshipAnalyzer = new FriendshipAnalyzer(FacebookApplication.LoggedInUser);
            initComboBoxFriendshipAnalyzer();
        }

        private void initComboBoxFriendshipAnalyzer()
        {
            comboBoxFriends.DisplayMember = "Name";

            foreach (User friend in FacebookApplication.LoggedInUser.Friends)
            {
                comboBoxFriends.Items.Add(friend);
            }
        }

        private void fetchPhotosTaggedTogether()
        {
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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
