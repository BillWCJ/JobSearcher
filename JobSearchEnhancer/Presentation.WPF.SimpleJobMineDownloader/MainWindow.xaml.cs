using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Business.Account;
using Data.Web.JobMine;
using Model.Definition;
using Model.Entities;

namespace Presentation.WPF.SimpleJobMineDownloader
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

            ThreadPool.QueueUserWorkItem(o =>
            {
                Dispatcher.Invoke(GetPostInfo);

                var client = new CookieEnabledWebClient();

                bool isLoggedIntoJobMine = new Login(new AccountManager().Account).LoginToJobMine(client, UserName, Password);
                Dispatcher.Invoke((() => OutputTextBox.AppendText("Loggedin :" + isLoggedIntoJobMine + "\n")));
                if (!isLoggedIntoJobMine)
                    return;

                Queue<string> jobIDs = JobInquiry.GetJobIDs(client, Term, JobStatus);
                Dispatcher.Invoke((() => OutputTextBox.AppendText("Total Number of Jobs Found:" + jobIDs.Count + "\n")));

                try
                {
                    for (uint currentFilePart = 1; jobIDs.Count > 0; currentFilePart++)
                    {
                        StreamWriter writer = TextParser.OpenFileForStreamWriter(FileLocation,
                            "JobDetailPart" + currentFilePart + ".txt");
                        uint part = currentFilePart;
                        Dispatcher.Invoke(
                            (() =>
                                OutputTextBox.AppendText("Writing JobDetailPart" + part + " (" + NumberOfJobPerFile +
                                                         " Jobs Per File)" + "\n")));

                        writer.Write("Download Time:" + DateTime.Now.ToString("s"));
                        for (uint currentFileJobCount = 1;
                            currentFileJobCount <= NumberOfJobPerFile && jobIDs.Count > 0;
                            currentFileJobCount++)
                        {
                            string currentJobId = jobIDs.Dequeue();
                            Dispatcher.Invoke(
                                (() =>
                                    OutputTextBox.AppendText("Downloading and Writing Job number:" + currentFileJobCount +
                                                             "- JobMine JobId: " + currentJobId + "\n")));
                            string url = JobMineDef.JobDetailBaseUrl + currentJobId;
                            writer.Write(JobDetail.GetJob(client.DownloadString(url), currentJobId).ToString());
                        }
                        writer.Close();
                        Dispatcher.Invoke((() => OutputTextBox.AppendText("Finished Writing JobDetailPart" + part + "\n")));
                    }
                    Dispatcher.Invoke((() => OutputTextBox.AppendText("Finished\n")));
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(
                        (() =>
                            OutputTextBox.AppendText(MethodBase.GetCurrentMethod().Name + "-" + ex + ":" + ex.Message + "\n")));
                }
            });
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
            DialogResult result = folderBrowserDialog.ShowDialog();
            FileLocation = folderBrowserDialog.SelectedPath;
            FileLocationTextBox.Text = FileLocation;
        }
    }
}