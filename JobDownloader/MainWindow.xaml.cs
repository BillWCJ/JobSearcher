﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Business.Common;
using Data.Web.JobMine;
using Model.Definition;
using Model.Entities;

namespace JobDownloader
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FileLocation = "c:\\";
            NumberOfJobPerFile = 100;
            IsInProgress = false;
        }

        private string FileLocation { get; set; }
        private string Term { get; set; }
        private string UserName { get; set; }
        private string Password { get; set; }
        private string JobStatus { get; set; }
        private uint NumberOfJobPerFile { get; set; }
        private bool IsInProgress { get; set; }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Button_Click_DownloadJobs(object sender, RoutedEventArgs e)
        {
            if (IsInProgress)
                return;
            IsInProgress = true;

            GetPostInfo();
            var jobMineManager = new JobMineManager();
            foreach (string msg in  jobMineManager.DownLoadJobs(UserName, Password, Term, JobStatus, FileLocation))
                OutputTextBox.AppendText(msg);

            IsInProgress = false;
        }

        private void GetPostInfo()
        {
            UserName = UserNameTextBox.Text;
            Password = PasswordTextBox.Text;
            Term = TermTextBox.Text;
            switch (JobStatusComboBox.SelectedIndex)
            {
                case 0:
                    JobStatus = Model.Definition.JobStatus.Posted;
                    break;
                case 1:
                    JobStatus = Model.Definition.JobStatus.AppsAvail;
                    break;
                case 2:
                    JobStatus = Model.Definition.JobStatus.Cancelled;
                    break;
                default:
                    JobStatus = Model.Definition.JobStatus.Approved;
                    break;
            }
        }

        private void Button_Click_Select_File_Location(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            FileLocation = folderBrowserDialog.SelectedPath;
            FileLocation = FileLocation.TrimEnd(' ','\\') + '\\';
            FileLocationTextBox.Text = FileLocation;
        }
    }
}