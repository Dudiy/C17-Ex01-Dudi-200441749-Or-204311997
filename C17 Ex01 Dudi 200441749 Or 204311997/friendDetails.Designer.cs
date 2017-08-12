namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    partial class FriendDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelFriendDetails = new System.Windows.Forms.Panel();
            this.labelBirthdayTitle = new System.Windows.Forms.Label();
            this.largePictureBoxLikedPage = new System.Windows.Forms.PictureBox();
            this.likedPagesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.userBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.linkLabelLikedPageURL = new System.Windows.Forms.LinkLabel();
            this.labelLikedPageName = new System.Windows.Forms.Label();
            this.listBoxLikedPage = new System.Windows.Forms.ListBox();
            this.labelLikedPage = new System.Windows.Forms.Label();
            this.labelFriendName = new System.Windows.Forms.Label();
            this.labelBirthday = new System.Windows.Forms.Label();
            this.largePictureBoxFriend = new System.Windows.Forms.PictureBox();
            this.panelFriendDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.largePictureBoxLikedPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.likedPagesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.largePictureBoxFriend)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFriendDetails
            // 
            this.panelFriendDetails.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelFriendDetails.Controls.Add(this.labelBirthdayTitle);
            this.panelFriendDetails.Controls.Add(this.largePictureBoxLikedPage);
            this.panelFriendDetails.Controls.Add(this.linkLabelLikedPageURL);
            this.panelFriendDetails.Controls.Add(this.labelLikedPageName);
            this.panelFriendDetails.Controls.Add(this.listBoxLikedPage);
            this.panelFriendDetails.Controls.Add(this.labelLikedPage);
            this.panelFriendDetails.Controls.Add(this.labelFriendName);
            this.panelFriendDetails.Controls.Add(this.labelBirthday);
            this.panelFriendDetails.Controls.Add(this.largePictureBoxFriend);
            this.panelFriendDetails.Location = new System.Drawing.Point(12, 12);
            this.panelFriendDetails.Name = "panelFriendDetails";
            this.panelFriendDetails.Size = new System.Drawing.Size(356, 601);
            this.panelFriendDetails.TabIndex = 10;
            // 
            // labelBirthdayTitle
            // 
            this.labelBirthdayTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBirthdayTitle.AutoSize = true;
            this.labelBirthdayTitle.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBirthdayTitle.Location = new System.Drawing.Point(42, 271);
            this.labelBirthdayTitle.Name = "labelBirthdayTitle";
            this.labelBirthdayTitle.Size = new System.Drawing.Size(59, 15);
            this.labelBirthdayTitle.TabIndex = 26;
            this.labelBirthdayTitle.Text = "Birthday:";
            // 
            // largePictureBoxLikedPage
            // 
            this.largePictureBoxLikedPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.largePictureBoxLikedPage.DataBindings.Add(new System.Windows.Forms.Binding("Image", this.likedPagesBindingSource, "ImageLarge", true));
            this.largePictureBoxLikedPage.Location = new System.Drawing.Point(182, 411);
            this.largePictureBoxLikedPage.Name = "largePictureBoxLikedPage";
            this.largePictureBoxLikedPage.Size = new System.Drawing.Size(122, 124);
            this.largePictureBoxLikedPage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.largePictureBoxLikedPage.TabIndex = 25;
            this.largePictureBoxLikedPage.TabStop = false;
            // 
            // likedPagesBindingSource
            // 
            this.likedPagesBindingSource.DataMember = "LikedPages";
            this.likedPagesBindingSource.DataSource = this.userBindingSource;
            // 
            // userBindingSource
            // 
            this.userBindingSource.DataSource = typeof(FacebookWrapper.ObjectModel.User);
            // 
            // linkLabelLikedPageURL
            // 
            this.linkLabelLikedPageURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelLikedPageURL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.likedPagesBindingSource, "URL", true));
            this.linkLabelLikedPageURL.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelLikedPageURL.Location = new System.Drawing.Point(182, 538);
            this.linkLabelLikedPageURL.Name = "linkLabelLikedPageURL";
            this.linkLabelLikedPageURL.Size = new System.Drawing.Size(100, 59);
            this.linkLabelLikedPageURL.TabIndex = 24;
            this.linkLabelLikedPageURL.TabStop = true;
            this.linkLabelLikedPageURL.Text = "linkLabel1";
            this.linkLabelLikedPageURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLikedPageURL_LinkClicked);
            // 
            // labelLikedPageName
            // 
            this.labelLikedPageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLikedPageName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.likedPagesBindingSource, "Name", true));
            this.labelLikedPageName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLikedPageName.Location = new System.Drawing.Point(182, 357);
            this.labelLikedPageName.Name = "labelLikedPageName";
            this.labelLikedPageName.Size = new System.Drawing.Size(122, 51);
            this.labelLikedPageName.TabIndex = 23;
            this.labelLikedPageName.Text = "Name Of Page";
            // 
            // listBoxLikedPage
            // 
            this.listBoxLikedPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLikedPage.DataSource = this.likedPagesBindingSource;
            this.listBoxLikedPage.DisplayMember = "Name";
            this.listBoxLikedPage.FormattingEnabled = true;
            this.listBoxLikedPage.Location = new System.Drawing.Point(23, 357);
            this.listBoxLikedPage.Name = "listBoxLikedPage";
            this.listBoxLikedPage.Size = new System.Drawing.Size(153, 212);
            this.listBoxLikedPage.TabIndex = 22;
            this.listBoxLikedPage.ValueMember = "AccessToken";
            // 
            // labelLikedPage
            // 
            this.labelLikedPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLikedPage.Font = new System.Drawing.Font("Castellar", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLikedPage.Location = new System.Drawing.Point(0, 294);
            this.labelLikedPage.Name = "labelLikedPage";
            this.labelLikedPage.Size = new System.Drawing.Size(325, 60);
            this.labelLikedPage.TabIndex = 21;
            this.labelLikedPage.Text = "Look about pages that name liked";
            this.labelLikedPage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelFriendName
            // 
            this.labelFriendName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFriendName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.userBindingSource, "Name", true));
            this.labelFriendName.Font = new System.Drawing.Font("Castellar", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFriendName.Location = new System.Drawing.Point(41, 22);
            this.labelFriendName.Name = "labelFriendName";
            this.labelFriendName.Size = new System.Drawing.Size(272, 23);
            this.labelFriendName.TabIndex = 14;
            this.labelFriendName.Text = "Friend Name";
            // 
            // labelBirthday
            // 
            this.labelBirthday.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBirthday.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.userBindingSource, "Birthday", true));
            this.labelBirthday.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBirthday.Location = new System.Drawing.Point(109, 271);
            this.labelBirthday.Name = "labelBirthday";
            this.labelBirthday.Size = new System.Drawing.Size(100, 23);
            this.labelBirthday.TabIndex = 1;
            this.labelBirthday.Text = "1/1/1970";
            // 
            // largePictureBoxFriend
            // 
            this.largePictureBoxFriend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.largePictureBoxFriend.DataBindings.Add(new System.Windows.Forms.Binding("Image", this.userBindingSource, "ImageLarge", true));
            this.largePictureBoxFriend.Location = new System.Drawing.Point(45, 48);
            this.largePictureBoxFriend.Name = "largePictureBoxFriend";
            this.largePictureBoxFriend.Size = new System.Drawing.Size(268, 220);
            this.largePictureBoxFriend.TabIndex = 5;
            this.largePictureBoxFriend.TabStop = false;
            // 
            // FriendDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 625);
            this.Controls.Add(this.panelFriendDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FriendDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "friendDetails";
            this.panelFriendDetails.ResumeLayout(false);
            this.panelFriendDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.largePictureBoxLikedPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.likedPagesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.largePictureBoxFriend)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelFriendDetails;
        private System.Windows.Forms.BindingSource userBindingSource;
        private System.Windows.Forms.PictureBox largePictureBoxFriend;
        private System.Windows.Forms.Label labelFriendName;
        private System.Windows.Forms.Label labelLikedPage;
        private System.Windows.Forms.ListBox listBoxLikedPage;
        private System.Windows.Forms.BindingSource likedPagesBindingSource;
        private System.Windows.Forms.PictureBox largePictureBoxLikedPage;
        private System.Windows.Forms.LinkLabel linkLabelLikedPageURL;
        private System.Windows.Forms.Label labelLikedPageName;
        private System.Windows.Forms.Label labelBirthday;
        private System.Windows.Forms.Label labelBirthdayTitle;
    }
}