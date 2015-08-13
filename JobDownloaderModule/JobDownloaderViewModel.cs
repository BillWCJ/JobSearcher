using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Business.DataBaseSeeder;
using Business.Manager;
using Data.Contract.JobMine;
using Data.IO.Local;
using Data.Web.JobMine;
using Model.Definition;

namespace JobDownloaderModule
{
    public class JobDownloaderViewModel
    {
        public JobDownloaderViewModel()
        {
            NumberOfJobPerFile = 100;
            FileLocation = "c:\\";
            ProgressStringBuilder = new StringBuilder();
            IsInProgressLock = new object();
            MessageCallBack = newMessage =>
            {
                ProgressStringBuilder.AppendLine(newMessage);
            };
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Term { get; set; }
        public int JobStatusSelectionIndex { get; set; }
        public string FileLocation { get; set; }
        public string DownloadFormat { get; set; }
        public uint NumberOfJobPerFile { get; set; }
        private object IsInProgressLock { get; set; }
        private Action<string> MessageCallBack { get; set; }
        private StringBuilder ProgressStringBuilder { get; set; }
        public bool SeedLocation { get; set; }
        public bool SeedJobRating { get; set; }

        public string Progress
        {
            get
            {
                return ProgressStringBuilder.ToString();
            }
        }

        public void DownloadJobToLocal()
        {
            lock (IsInProgressLock)
            {
                IJobMineRepo jobMineRepo = null;
                try
                {
                    jobMineRepo = new JobMineRepo(UserName, Password);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                    MessageCallBack("Error While Logging In");
                    MessageCallBack(ex.ToString());
                    return;
                }
                MessageCallBack("Succesfully Loggedin");

                var jobMineManager = new LocalDownloadManager(jobMineRepo);
                try
                {
                    //DownloadFormat
                    ThreadPool.QueueUserWorkItem(o =>
                    {
                        foreach (var msg in jobMineManager.DownLoadJobs(UserName, Password, Term, GetJobStatus(), FileLocation))
                        {
                            //Dispatcher.Invoke(() => MessageCallBack(msg));
                        }
                    });
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                    MessageCallBack("A Major error occurred, Operation Aborted");
                }
            }
        }

        public void DownloadAndSeedJobIntoDb()
        {
            var account = new JseLocalRepo().GetAccount();
            foreach (var msg in MasterSeeder.SeedAll(Term, GetJobStatus(), account, false, false))
            {
                MessageCallBack(msg);
            }
        }

        private string GetJobStatus()
        {
            string jobStatus;
            switch (JobStatusSelectionIndex)
            {
                case 0:
                    jobStatus = JobStatus.Posted;
                    break;
                case 1:
                    jobStatus = JobStatus.AppsAvail;
                    break;
                case 2:
                    jobStatus = JobStatus.Cancelled;
                    break;
                default:
                    jobStatus = JobStatus.Approved;
                    break;
            }
            return jobStatus;
        }

        public void ExportFromDbToLocal()
        {
            LocalDownloadManager.ExportJob(MessageCallBack, FileLocation, NumberOfJobPerFile);
        }
    }
}