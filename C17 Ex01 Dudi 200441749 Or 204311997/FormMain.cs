using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using C17_Ex01_Dudi_200441749_Or_204311997.DataTables;
using System.Drawing;
using System.ComponentModel;
using System.Threading;

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
        private const byte k_PictureBoxIncreaseSizeOnMouseHover = 20;

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
            updateAboutMeFriends();
            //likedPagesBindingSource.DataSource = FacebookApplication.LoggedInUser.LikedPages;
            //postsBindingSource.DataSource = FacebookApplication.LoggedInUser.Posts;
            //friendsBindingSource.DataSource = FacebookApplication.LoggedInUser.Friends;
            listBoxPostTags.ClearSelected();
        }

        private void updateAboutMeFriends()
        {
            foreach (User friend in FacebookApplication.LoggedInUser.Friends)
            {
                PictureBox profilePic = new PictureBox();
                profilePic.LoadAsync(friend.PictureSqaureURL);
                profilePic.Size = new Size(90, 90);
                profilePic.SizeMode = PictureBoxSizeMode.Zoom;
                profilePic.Tag = friend;
                profilePic.MouseEnter += friendshipAnalyzerFriendsDockPhoto_MouseEnter;
                profilePic.MouseLeave += friendshipAnalyzerFriendsDockPhoto_MouseLeave;
                profilePic.MouseClick += friendshipAnalyzerFriendsDockPhoto_MouseClick;
                flowLayoutPanelAboutMeFriends.Controls.Add(profilePic);
            }
        }

        private void uRLLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            uRLLinkLabel.LinkVisited = true;
            // Navigate to a URL.
            System.Diagnostics.Process.Start(uRLLinkLabel.Text);
        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            if (richTextBoxStatusPost.Text != "")
            {
                string friendID = string.Empty;
                foreach (User friend in listBoxPostTags.SelectedItems)
                {
                    friendID += friend.Id + ",";
                }
                friendID = friendID != "" ?
                    friendID.Remove(friendID.Length - 1) :
                    null;

                Status postedStatus = FacebookApplication.LoggedInUser.PostStatus(
                    richTextBoxStatusPost.Text, i_TaggedFriendIDs: friendID);

                MessageBox.Show("Status Posted! ID: " + postedStatus.Id);
                richTextBoxStatusPost.Clear();
                listBoxPostTags.ClearSelected();
            }
            else
            {
                MessageBox.Show("You mush enter a status text OR add photo");
            }
        }

        private void buttonClearPostTags_Click(object sender, EventArgs e)
        {
            listBoxPostTags.ClearSelected();
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
            AlbumsSelector albumsSelector = new AlbumsSelector(FacebookApplication.LoggedInUser);
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
            initFriendsPhotosBar();
        }

        private void fetchPhotosTaggedTogether()
        {
            List<Photo> taggedTogether = m_FriendshipAnalyzer.FetchPhotosTaggedTogether(progressBarTaggedTogether);
            Dictionary<string, List<Photo>> photosGroupedByOwner = m_FriendshipAnalyzer.groupPhotoListByOwner(taggedTogether);                

            foreach (KeyValuePair<string, List<Photo>> UserPhotos in photosGroupedByOwner)
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
            AlbumsSelector albumSelector = new AlbumsSelector(FacebookApplication.LoggedInUser);
            Album[] selectedAlbums = albumSelector.GetAlbumsSelection();
            Dictionary<Album, List<Photo>> photos = FacebookPhotoUtils.GetPhotosByOwnerAndTags(
                FacebookApplication.LoggedInUser, m_FriendshipAnalyzer.Friend, selectedAlbums, progressBarPhotosOfFriendInMine);

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

        private void initFriendsPhotosBar()
        {
            flowLayoutPanelFriendshipAnalyzer.Width = 110;
            flowLayoutPanelFriendshipAnalyzer.Padding = new Padding(0, 0, 20, 0);

            foreach (User friend in FacebookApplication.LoggedInUser.Friends)
            {
                PictureBox friendshipAnalyzerFriendsDockPhoto = new PictureBox();
                friendshipAnalyzerFriendsDockPhoto.LoadAsync(friend.PictureSqaureURL);
                //TODO delete
                //friendshipAnalyzerFriendsDockPhoto.Image = friend.ImageLarge;
                friendshipAnalyzerFriendsDockPhoto.Size = new Size(90, 90);
                friendshipAnalyzerFriendsDockPhoto.SizeMode = PictureBoxSizeMode.Zoom;
                friendshipAnalyzerFriendsDockPhoto.Tag = friend;
                friendshipAnalyzerFriendsDockPhoto.MouseEnter += friendshipAnalyzerFriendsDockPhoto_MouseEnter;
                friendshipAnalyzerFriendsDockPhoto.MouseLeave += friendshipAnalyzerFriendsDockPhoto_MouseLeave;
                friendshipAnalyzerFriendsDockPhoto.MouseClick += friendshipAnalyzerFriendsDockPhoto_MouseClick;
                flowLayoutPanelFriendshipAnalyzer.Controls.Add(friendshipAnalyzerFriendsDockPhoto);
            }
        }

        private void friendshipAnalyzerFriendsDockPhoto_MouseLeave(object sender, EventArgs e)
        {
            increasePictureBoxSize(sender as PictureBox, -k_PictureBoxIncreaseSizeOnMouseHover);
        }

        private void friendshipAnalyzerFriendsDockPhoto_MouseEnter(object sender, EventArgs e)
        {
            increasePictureBoxSize(sender as PictureBox, k_PictureBoxIncreaseSizeOnMouseHover);
        }

        private void friendshipAnalyzerFriendsDockPhoto_MouseClick(object sender, MouseEventArgs e)
        {
            User friend = ((PictureBox)sender).Tag as User;
            m_FriendshipAnalyzer.Friend = friend;
            friendSelectionChanged();
        }

        private void friendSelectionChanged()
        {
            User selectedFriend = m_FriendshipAnalyzer.Friend;
            panelAnalyzingFriendship.Visible = true;
            panelGeneralInfo.Visible = false;            
            labelAnalyzingFriendship.Text = "Counting likes";
            int numPhotosFriendLiked = m_FriendshipAnalyzer.GetNumberOfPhotosFriendLiked(progressBarAnalyzingFriendship);
            labelAnalyzingFriendship.Text = "Counting comments";
            int numOfPhotosFriendCommented = m_FriendshipAnalyzer.GetNumberOfPhotosFriendCommented(progressBarAnalyzingFriendship);
            labelAnalyzingFriendship.Text = "Searching for most recent photo together";
            Photo mostRecentTaggedTogether = m_FriendshipAnalyzer.GetMostRecentPhotoTaggedTogether();
            labelName.Text = selectedFriend.Name;
            labelNumLikes.Text = String.Format("Number of times {0} liked my photos: {1}", selectedFriend.Name, numPhotosFriendLiked);
            labelNumComments.Text = String.Format("Number of times {0} commented on my photos: {1}", selectedFriend.Name, numOfPhotosFriendCommented);
            pictureBoxMostRecentTaggedTogether.LoadAsync(mostRecentTaggedTogether.PictureNormalURL);
            buttonFetchMyPhotosFriendIsIn.Text = String.Format("Fetch photos of mine {0} is in", selectedFriend.FirstName);
            buttonFetchPhotosOfFriendIAmTaggedIn.Text = String.Format("Fetch {0}'s Photos I am Tagged in", selectedFriend.FirstName);
            panelGeneralInfo.Visible = true;
            panelAnalyzingFriendship.Visible = false;
        }

        private void increasePictureBoxSize(PictureBox i_PictureBox, int i_Size)
        {
            int newWidth = i_PictureBox.Size.Width + i_Size;
            int newHeight = i_PictureBox.Size.Height + i_Size;

            i_PictureBox.Size = new Size(newWidth, newHeight);
        }

        private void fetchPhotosOfMeInFriendsPhotos()
        {
            AlbumsSelector albumSelector = new AlbumsSelector(m_FriendshipAnalyzer.Friend);            
            Album[] selectedAlbums = albumSelector.GetAlbumsSelection();
            Dictionary<Album, List<Photo>> photos = 
                FacebookPhotoUtils.GetPhotosByOwnerAndTags(m_FriendshipAnalyzer.Friend, FacebookApplication.LoggedInUser, selectedAlbums, progressBarPhotosOfMeInFriendsPhotos);
            
            treeViewPhotosOfFriendIAmTaggedIn.Nodes.Clear();
            progressBarPhotosOfMeInFriendsPhotos.Visible = true;
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

                treeViewPhotosOfFriendIAmTaggedIn.Nodes.Add(albumNode);
            }

            progressBarPhotosOfMeInFriendsPhotos.Visible = false;
        }

        // ====================================== Buttons ==================================
        private void buttonFetchPhotosOfFriendIAmTaggedIn_Click(object sender, EventArgs e)
        {
            fetchPhotosOfMeInFriendsPhotos();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            FacebookApplication.ExitSelected = true;
            Close();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            m_LogoutClicked = true;
            FacebookApplication.Logout();
        }

        // ====================================== Event handlers ======================================
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

        private void buttonFetchTaggedTogether_Click(object sender, EventArgs e)
        {
            fetchPhotosTaggedTogether();
        }

        private void buttonFetchMyPhotosFriendIsIn_Click(object sender, EventArgs e)
        {
            fetchPhotosOfFriendInMyPhotos();
        }
    }
}
