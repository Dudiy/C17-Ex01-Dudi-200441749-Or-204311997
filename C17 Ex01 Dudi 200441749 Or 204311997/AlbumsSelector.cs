﻿/*
 * C17_Ex01: AlbumsSelector.cs
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public partial class AlbumsSelector : Form
    {
        private User m_AlbumsOwner;
        private const DialogResult k_AlbumSelectionSuccessful = DialogResult.Yes;
        public List<Album> SelectedAlbums { get; private set; }
        private bool m_IgnoreCheckChangeEvents = false;

        public AlbumsSelector(User i_User)
        {
            InitializeComponent();
            m_AlbumsOwner = i_User;
            initAlbumsList();
        }

        private void initAlbumsList()
        {
            listBoxAlbums.DisplayMember = "Name";
            foreach (Album album in m_AlbumsOwner.Albums)
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

        public Album[] GetAlbumsSelection()
        {
            List<Album> selectedAlbums = new List<Album>();
            DialogResult dialogResult = this.ShowDialog();
            
            return SelectedAlbums.ToArray();
        }
    }
}
