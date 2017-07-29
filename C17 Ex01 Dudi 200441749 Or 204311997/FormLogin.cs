using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookWrapper;

namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            loginAndInit();
        }

        private void loginAndInit()
        {
            LoginResult result = FacebookWrapper.FacebookService.Login("197501144117907", "public_profile");
        }
    }
}
