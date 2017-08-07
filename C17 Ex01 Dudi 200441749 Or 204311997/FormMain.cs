using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System.Threading;
using C17_Ex01_Dudi_200441749_Or_204311997.DataTables;
using System.Drawing;
using System.IO;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public partial class FormMain : Form
    {
        private FacebookDataTableManager m_DataTableManager;
        private FacebookDataTable m_DataTableBindedToView;
        public AppSettings AppSettings { get; private set; } = AppSettings.Instance;
        public User LoggedInUser { get; private set; }
        public bool RememberMe { get; set; }
        private string m_PostPictureURL;

        public FormMain()
        {
            InitializeComponent();
            // TODO delete, in order to save center position in default settings
            CenterToScreen();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            initLastUseSettings();
            FacebookService.s_CollectionLimit = 500;
            LoggedInUser = FacebookService.Connect(AppSettings.LastAccessToken).LoggedInUser;
            initMainForm();
            fetchProfileAndCoverPhotos();
            //Thread.Sleep(100000);
            // init tabs
            initAboutMeTab();
            initDataTablesTab();
        }

        private void initMainForm()
        {
            // init global form
            Text = LoggedInUser.Name;
            labelUserName.Text = LoggedInUser.Name;

            MinimumSize = new System.Drawing.Size(Size.Width, Size.Height);
        }

        private void fetchProfileAndCoverPhotos()
        {
            // TODO check if there are pic
            if (LoggedInUser.PictureNormalURL != null)
            {
                pictureBoxProfilePicture.LoadAsync(LoggedInUser.PictureNormalURL);
                pictureBoxProfilePicture.Visible = true;
            }
            else
            {
                // TODO add empty user picture
            }

            if (LoggedInUser.Cover != null)
            {
                pictureBoxCoverPhoto.LoadAsync(LoggedInUser.Cover.SourceURL);
                pictureBoxCoverPhoto.Visible = true;
            }
        }

        private void initLastUseSettings()
        {
            AppSettings = AppSettings.Instance;StartPosition = FormStartPosition.Manual;
            Location = AppSettings.LastWindowsLocation;
            Size = AppSettings.LastWindowsSize;
            if (AppSettings.RememberUser == true)
            {
                checkBoxRememberMe.Checked = true;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // TODO if logout and exit is the same in that case
            // TODO 
            if(checkBoxRememberMe.Checked == true)
            {
                // access token update all the time to the current user
                AppSettings.LastWindowsLocation = Location;
                AppSettings.LastWindowsSize = Size;
                AppSettings.RememberUser = checkBoxRememberMe.Checked;
                AppSettings.SaveToFile();
            }
            else
            {
                AppSettings.Clear();
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
            foreach (User friend in LoggedInUser.Friends)
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
            foreach (Page page in LoggedInUser.LikedPages)
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
            foreach (Event upcomingEvent in LoggedInUser.Events)
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
            if(LoggedInUser.Birthday != null)
            {
                DateTime myBirthday = convertStringToDate(LoggedInUser.Birthday);
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
            DialogResult = DialogResult.Ignore;
            FacebookService.Logout(null);
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
            Close();
        }

        // ================================================ DataTables Tab ==============================================
        private void initDataTablesTab()
        {
            m_DataTableManager = new FacebookDataTableManager(LoggedInUser);
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
            AlbumsSelector albumsSelector = new AlbumsSelector(LoggedInUser);

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

        private void checkBoxRememberMe_CheckedChanged(object sender, EventArgs e)
        {
            RememberMe = ((CheckBox)sender).Checked;
        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            // TODO multi tags
            User friend = comboBoxTagFriend.SelectedItem as User;
            string friendID = friend != null ? friend.Id : null;
            Status postedStatus = LoggedInUser.PostStatus(richTextBoxStatusPost.Text, 
                i_TaggedFriendIDs: friendID, i_PictureURL: m_PostPictureURL);

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
    }
}
