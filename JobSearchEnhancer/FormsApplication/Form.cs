﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Account;
using Model.Entities;
using Data.Web.JobMine;
using Model.Definition;

namespace FormsApplication
{
    public partial class JobDetailBrowser : Form
    {
        Job[] jobs { get; set; }
        int current = 0;

        private void DisplayCurrentJobDetail()
        {

            SectionTabControl.SelectedIndex = 0;
            JobDetailRichTextBox.Text = jobs[current].ToString("f");
        }

        public JobDetailBrowser()
        {
            InitializeComponent();
            jobs = new TextParser(new AccountManager().Account).ReadInJobFromLocal();
            DisplayCurrentJobDetail();
            CommonWebBrowser.Url = new Uri(JobMineDef.LogInUrl);
        }

        private void PreviousJobButton_Click(object sender, EventArgs e)
        {
            if (--current < 0)
                current = 0;
            DisplayCurrentJobDetail();
        }

        private void NextJobButton_Click(object sender, EventArgs e)
        {
            if (++current >= jobs.Length)
                current = jobs.Length - 1;
            DisplayCurrentJobDetail();
        }

        private void JobDetailRichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            string url = e.LinkText;
            if (url.IndexOf(@"http://") != 0 && url.IndexOf(@"https://") !=0)
                url = @"http://" + url;
            CommonWebBrowser.Url = new Uri(url);
            SectionTabControl.SelectedIndex = 2;
        }
    }
}
