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
    public partial class PhotoDetails : Form
    {
        Photo m_Photo;
        public PhotoDetails(Photo i_Photo)
        {
            InitializeComponent();
            m_Photo = i_Photo;
            initDetailsPane();
            pictureBox.LoadAsync(m_Photo.PictureNormalURL);
        }

        private void initDetailsPane()
        {
            labelName.Text = String.Format("Name: {0}", m_Photo.Name);
            labelAlbum.Text = String.Format("Album: {0}", m_Photo.Album != null ? m_Photo.Album.Name : "No Album Name");
            labelLikes.Text = String.Format("Likes ({0}):", m_Photo.LikedBy.Count);
            initLikes();
            initComments();
        }

        private void initLikes()
        {
            listBoxLikes.DisplayMember = "Name";
            foreach (User liker in m_Photo.LikedBy)
            {
                listBoxLikes.Items.Add(liker);
            }
        }

        private void initComments()
        {
            int numOfComments = m_Photo.Comments.Count;
            ProgressBarWindow commentsProgressBar = null;

            // TODO modify min num of comments to show progress bar
            if (numOfComments > 5)
            {
                commentsProgressBar = new ProgressBarWindow(0, numOfComments, "comments");
                commentsProgressBar.Show();
            }

            foreach (Comment comment in m_Photo.Comments)
            {
                TreeNode node = new TreeNode(comment.From.Name + ": " + comment.Message + " (" + comment.LikedBy.Count.ToString() + " Likes)");
                node.Tag = comment;
                foreach (Comment innerComment in comment.Comments)
                {
                    TreeNode child = new TreeNode(innerComment.From.Name + ": " + innerComment.Message + " (" + innerComment.LikedBy.Count.ToString() + " Likes)");
                    child.Tag = innerComment;
                    node.Nodes.Add(child);
                }
                treeViewComments.Nodes.Add(node);
                if (commentsProgressBar != null)
                {
                    commentsProgressBar.ProgressValue++;
                }
            }

            treeViewComments.Show();
            //TODO is refresh needed?
            treeViewComments.Refresh();
        }

        private void listBoxLikes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            User selectedUser = listBoxLikes.SelectedItem as User;
            if (selectedUser != null)
            {
                PictureFrame profilePic = new PictureFrame(selectedUser.PictureLargeURL);
                profilePic.Show();
            }
        }
    }
}
