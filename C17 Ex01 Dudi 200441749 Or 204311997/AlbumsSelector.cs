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
        User m_LoggedInUser;
        public List<Album> SelectedAlbums { get; private set; }
        public AlbumsSelector(User i_LoggedInUser)
        {
            InitializeComponent();            
            m_LoggedInUser = i_LoggedInUser;
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

            DialogResult = DialogResult.OK;
        }
    }
}
