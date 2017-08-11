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
    public partial class AlbumsSelector : Form
    {
        private User m_LoggedInUser = FacebookApplication.LoggedInUser;
        private const DialogResult k_AlbumSelectionSuccessful = DialogResult.Yes;
        public List<Album> SelectedAlbums { get; private set; }
        private bool m_IgnoreCheckChangeEvents = false;

        public AlbumsSelector()
        {
            InitializeComponent();
            initAlbumsList();
        }

        private void initAlbumsList()
        {
            listBoxAlbums.DisplayMember = "Name";
            foreach (Album album in m_LoggedInUser.Albums)
            {
                listBoxAlbums.Items.Add(album);
            }
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            SelectedAlbums = new List<Album>(listBoxAlbums.SelectedIndices.Count);
            foreach (Album selectedAlbum in listBoxAlbums.SelectedItems)
            {
                SelectedAlbums.Add(selectedAlbum);
            }

            DialogResult = k_AlbumSelectionSuccessful;
        }

        private void checkBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_IgnoreCheckChangeEvents)
            {
                setSelectedValueForAllItems(checkBoxSelectAll.Checked);
            }
        }

        private void setSelectedValueForAllItems(bool i_Selected)
        {
            for (int i = 0; i < listBoxAlbums.Items.Count; i++)
            {
                listBoxAlbums.SetSelected(i, i_Selected);
            }
        }

        private void listBoxAlbums_SelectedValueChanged(object sender, EventArgs e)
        {
            m_IgnoreCheckChangeEvents = true;
            if (listBoxAlbums.SelectedIndices.Count == listBoxAlbums.Items.Count)
            {
                checkBoxSelectAll.CheckState = CheckState.Checked;
            }
            else if (listBoxAlbums.SelectedIndices.Count == 0)
            {
                checkBoxSelectAll.CheckState = CheckState.Unchecked;
            }
            else
            {
                checkBoxSelectAll.CheckState = CheckState.Indeterminate;
            }

            m_IgnoreCheckChangeEvents = false;
        }
    }
}
