﻿namespace C17_Ex01_Dudi_200441749_Or_204311997
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
            System.Windows.Forms.Label birthdayLabel;
            this.panel1 = new System.Windows.Forms.Panel();
            this.imageLargePictureBox1 = new System.Windows.Forms.PictureBox();
            this.likedPagesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.userBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.uRLLinkLabel = new System.Windows.Forms.LinkLabel();
            this.nameLabel2 = new System.Windows.Forms.Label();
            this.languagesListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nameLabel1 = new System.Windows.Forms.Label();
            this.birthdayLabel1 = new System.Windows.Forms.Label();
            this.imageLargePictureBox = new System.Windows.Forms.PictureBox();
            birthdayLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageLargePictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.likedPagesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageLargePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // birthdayLabel
            // 
            birthdayLabel.AutoSize = true;
            birthdayLabel.Location = new System.Drawing.Point(144, 266);
            birthdayLabel.Name = "birthdayLabel";
            birthdayLabel.Size = new System.Drawing.Size(48, 13);
            birthdayLabel.TabIndex = 0;
            birthdayLabel.Text = "Birthday:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.imageLargePictureBox1);
            this.panel1.Controls.Add(this.uRLLinkLabel);
            this.panel1.Controls.Add(this.nameLabel2);
            this.panel1.Controls.Add(this.languagesListBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.nameLabel1);
            this.panel1.Controls.Add(birthdayLabel);
            this.panel1.Controls.Add(this.birthdayLabel1);
            this.panel1.Controls.Add(this.imageLargePictureBox);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(601, 728);
            this.panel1.TabIndex = 10;
            // 
            // imageLargePictureBox1
            // 
            this.imageLargePictureBox1.DataBindings.Add(new System.Windows.Forms.Binding("Image", this.likedPagesBindingSource, "ImageLarge", true));
            this.imageLargePictureBox1.Location = new System.Drawing.Point(306, 382);
            this.imageLargePictureBox1.Name = "imageLargePictureBox1";
            this.imageLargePictureBox1.Size = new System.Drawing.Size(109, 124);
            this.imageLargePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageLargePictureBox1.TabIndex = 25;
            this.imageLargePictureBox1.TabStop = false;
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
            // uRLLinkLabel
            // 
            this.uRLLinkLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.likedPagesBindingSource, "URL", true));
            this.uRLLinkLabel.Location = new System.Drawing.Point(306, 509);
            this.uRLLinkLabel.Name = "uRLLinkLabel";
            this.uRLLinkLabel.Size = new System.Drawing.Size(100, 59);
            this.uRLLinkLabel.TabIndex = 24;
            this.uRLLinkLabel.TabStop = true;
            this.uRLLinkLabel.Text = "linkLabel1";
            this.uRLLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.uRLLinkLabel_LinkClicked);
            // 
            // nameLabel2
            // 
            this.nameLabel2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.likedPagesBindingSource, "Name", true));
            this.nameLabel2.Location = new System.Drawing.Point(306, 356);
            this.nameLabel2.Name = "nameLabel2";
            this.nameLabel2.Size = new System.Drawing.Size(100, 23);
            this.nameLabel2.TabIndex = 23;
            this.nameLabel2.Text = "label2";
            this.nameLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // languagesListBox
            // 
            this.languagesListBox.DataSource = this.likedPagesBindingSource;
            this.languagesListBox.DisplayMember = "Name";
            this.languagesListBox.FormattingEnabled = true;
            this.languagesListBox.Location = new System.Drawing.Point(147, 356);
            this.languagesListBox.Name = "languagesListBox";
            this.languagesListBox.Size = new System.Drawing.Size(153, 212);
            this.languagesListBox.TabIndex = 22;
            this.languagesListBox.ValueMember = "AccessToken";
            // 
            // label1
            // 
            this.label1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.userBindingSource, "Name", true));
            this.label1.Font = new System.Drawing.Font("Castellar", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 307);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(567, 23);
            this.label1.TabIndex = 21;
            this.label1.Text = "Look about pages that name liked";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // nameLabel1
            // 
            this.nameLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.userBindingSource, "Name", true));
            this.nameLabel1.Font = new System.Drawing.Font("Castellar", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel1.Location = new System.Drawing.Point(143, 17);
            this.nameLabel1.Name = "nameLabel1";
            this.nameLabel1.Size = new System.Drawing.Size(272, 23);
            this.nameLabel1.TabIndex = 14;
            this.nameLabel1.Text = "Friend Name";
            this.nameLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // birthdayLabel1
            // 
            this.birthdayLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.userBindingSource, "Birthday", true));
            this.birthdayLabel1.Location = new System.Drawing.Point(248, 266);
            this.birthdayLabel1.Name = "birthdayLabel1";
            this.birthdayLabel1.Size = new System.Drawing.Size(100, 23);
            this.birthdayLabel1.TabIndex = 1;
            this.birthdayLabel1.Text = "label1";
            // 
            // imageLargePictureBox
            // 
            this.imageLargePictureBox.DataBindings.Add(new System.Windows.Forms.Binding("Image", this.userBindingSource, "ImageLarge", true));
            this.imageLargePictureBox.Location = new System.Drawing.Point(147, 43);
            this.imageLargePictureBox.Name = "imageLargePictureBox";
            this.imageLargePictureBox.Size = new System.Drawing.Size(268, 220);
            this.imageLargePictureBox.TabIndex = 5;
            this.imageLargePictureBox.TabStop = false;
            // 
            // FriendDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 740);
            this.Controls.Add(this.panel1);
            this.Name = "FriendDetails";
            this.Text = "friendDetails";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageLargePictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.likedPagesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageLargePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource userBindingSource;
        private System.Windows.Forms.Label birthdayLabel1;
        private System.Windows.Forms.PictureBox imageLargePictureBox;
        private System.Windows.Forms.Label nameLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox languagesListBox;
        private System.Windows.Forms.BindingSource likedPagesBindingSource;
        private System.Windows.Forms.PictureBox imageLargePictureBox1;
        private System.Windows.Forms.LinkLabel uRLLinkLabel;
        private System.Windows.Forms.Label nameLabel2;
    }
}