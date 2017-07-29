namespace C17_Ex01_Dudi_200441749_Or_204311997
{
    partial class FormMain
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageAboutMe = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pictureBoxProfilePicture = new System.Windows.Forms.PictureBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.pictureBoxCoverPhoto = new System.Windows.Forms.PictureBox();
            this.listBoxFriends = new System.Windows.Forms.ListBox();
            this.tabControl.SuspendLayout();
            this.tabPageAboutMe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfilePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCoverPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageAboutMe);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(12, 169);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1033, 743);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageAboutMe
            // 
            this.tabPageAboutMe.Controls.Add(this.listBoxFriends);
            this.tabPageAboutMe.Location = new System.Drawing.Point(4, 22);
            this.tabPageAboutMe.Name = "tabPageAboutMe";
            this.tabPageAboutMe.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAboutMe.Size = new System.Drawing.Size(1025, 717);
            this.tabPageAboutMe.TabIndex = 0;
            this.tabPageAboutMe.Text = "About Me";
            this.tabPageAboutMe.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1025, 717);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pictureBoxProfilePicture
            // 
            this.pictureBoxProfilePicture.Location = new System.Drawing.Point(916, 60);
            this.pictureBoxProfilePicture.Name = "pictureBoxProfilePicture";
            this.pictureBoxProfilePicture.Size = new System.Drawing.Size(125, 125);
            this.pictureBoxProfilePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxProfilePicture.TabIndex = 2;
            this.pictureBoxProfilePicture.TabStop = false;
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.BackColor = System.Drawing.SystemColors.Control;
            this.labelUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelUserName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelUserName.Location = new System.Drawing.Point(12, 12);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(160, 31);
            this.labelUserName.TabIndex = 3;
            this.labelUserName.Text = "User Name";
            // 
            // pictureBoxCoverPhoto
            // 
            this.pictureBoxCoverPhoto.Location = new System.Drawing.Point(293, 12);
            this.pictureBoxCoverPhoto.Name = "pictureBoxCoverPhoto";
            this.pictureBoxCoverPhoto.Size = new System.Drawing.Size(748, 151);
            this.pictureBoxCoverPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxCoverPhoto.TabIndex = 4;
            this.pictureBoxCoverPhoto.TabStop = false;
            // 
            // listBoxFriends
            // 
            this.listBoxFriends.FormattingEnabled = true;
            this.listBoxFriends.Location = new System.Drawing.Point(6, 36);
            this.listBoxFriends.Name = "listBoxFriends";
            this.listBoxFriends.Size = new System.Drawing.Size(207, 264);
            this.listBoxFriends.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 974);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.pictureBoxProfilePicture);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pictureBoxCoverPhoto);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.tabControl.ResumeLayout(false);
            this.tabPageAboutMe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfilePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCoverPhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageAboutMe;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBoxProfilePicture;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.PictureBox pictureBoxCoverPhoto;
        private System.Windows.Forms.ListBox listBoxFriends;
    }
}