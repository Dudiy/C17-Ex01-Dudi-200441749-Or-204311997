﻿/*
 * C17_Ex01: ProgressBarWindow.cs
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/
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
    public partial class ProgressBarWindow : Form
    {
        //public bool Finished { get; set; }
        public ProgressBarWindow(int i_MinValue, int i_MaxValue, string i_Description)
        {
            InitializeComponent();
            progressBar.Minimum = i_MinValue;
            progressBar.Maximum = i_MaxValue;
            labelLoading.Text = String.Format("Loading {0}...", i_Description);
        }

        public ProgressBarWindow(string i_Description)
            : this(0, 0, i_Description)
        { }

        public int MaxValue
        {
            get { return progressBar.Maximum; }
            set { progressBar.Maximum = value; }
        }
        public int ProgressValue
        {
            get { return progressBar.Value; }
            set
            {
                if (value <= progressBar.Maximum)
                {
                    progressBar.Value = value;
                    labelLoadedPercent.Text = String.Format("{0:P0}", (float)value / progressBar.Maximum);
                    //TODO is the refresh needed?
                    Refresh();
                }
                else
                {
                    throw new ArgumentOutOfRangeException(string.Format(@"
Valid values for this progress bar are {0}-{1}",
progressBar.Minimum,
progressBar.Maximum));
                }
            }
        }
    }
}
