using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AwkEverywhere.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void BtnCloseAbout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Lnk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel oLink = sender as LinkLabel;
            System.Diagnostics.Process.Start(oLink.Text); 
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            label3.Text = "AwkEverywhere v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

    }
}