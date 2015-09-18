using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Business.DataBaseSeeder;
using Business.Manager;
using Data.Contract.JobMine;
using Data.IO.Local;
using Data.Web.JobMine;
using Microsoft.Practices.Prism.PubSubEvents;
using Model.Definition;
using Model.Entities;
using Presentation.WPF;
using Presentation.WPF.Events;

namespace JobDownloaderModule
{
    public class JobDownloaderViewModel : ViewModelBase
    {
        public JobDownloaderViewModel()
        {
            NumberOfJobPerFile = 100;
            FileLocation = "c:\\";
            MessageCallBack = message =>
            {
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Render, (Action<string>) (UpdateProgress), message);
            };
        }

        public JobDownloaderViewModel(EventAggregator aggregator) : this()
        {
            _aggregator = aggregator;
        }

        private EventAggregator _aggregator;
        private readonly object _isDisplayingMessageLock = new object();
        private readonly object _isInProgressLock = new object();

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Term { get; set; }
        public int JobStatusSelectionIndex { get; set; }
        public string FileLocation { get; set; }
        public string DownloadFormat { get; set; }
        public int NumberOfJobPerFile { get; set; }
        public bool SeedLocation { get; set; }
        public bool SeedJobRating { get; set; }
        public string GoogleMapApiKeyString { get; set; }
        private Action<string> MessageCallBack { get; set; }
        private string _progress = string.Empty;

        public string Progress
        {
            get
            {
                return _progress;
            }
        }

        private void UpdateProgress(string newMessage)
        {
            if (!newMessage.StartsWith(CommonDef.CurrentStatus))
            {
                lock (_isDisplayingMessageLock)
                {
                    _progress += newMessage + Environment.NewLine;
                    OnPropertyChanged("Progress");
                }
            }
            else
            {
                _aggregator.GetEvent<CurrentStatusMessageChangedEvent>().Publish(newMessage.Replace(CommonDef.CurrentStatus, ""));
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
            Task.Factory.StartNew(() =>
            {
                var acquired = false;
                try
                {
                    acquired = Monitor.TryEnter(_isInProgressLock);
                    if (acquired)
                    {
                        LocalDownloadManager.ExportJob(MessageCallBack, FileLocation, NumberOfJobPerFile);
                    }
                    else
                    {
                        MessageCallBack("An Operation is already in progress");
                    }
                }
                finally
                {
                    if (acquired)
                        Monitor.Exit(_isInProgressLock);
                }
            }, Task.Factory.CancellationToken);
        }

        public void ImportFromLocal()
        {
            Task.Factory.StartNew(() =>
            {
                var acquired = false;
                try
                {
                    acquired = Monitor.TryEnter(_isInProgressLock);
                    if (acquired)
                    {
                        LocalDownloadManager.ImportJob(MessageCallBack, FileLocation);
                    }
                    else
                    {
                        MessageCallBack("An Operation is already in progress");
                    }
                }
                finally
                {
                    if (acquired)
                        Monitor.Exit(_isInProgressLock);
                }
            }, Task.Factory.CancellationToken);
            
        }

        public void DownloadAndSeedJobIntoDb()
        {
            Task.Factory.StartNew(() =>
            {
                var acquired = false;
                try
                {
                    acquired = Monitor.TryEnter(_isInProgressLock);
                    if (acquired)
                    {
                        MasterSeeder.SeedAll(MessageCallBack, new UserAccount(){JobMineUsername = UserName, JobMinePassword = Password}, Term, GetJobStatus(), true);
                        _aggregator.GetEvent<JobDownloadCompletedEvent>().Publish(true);
                    }
                    else
                    {
                        MessageCallBack("An Operation is already in progress");
                    }
                }
                finally
                {
                    if (acquired)
                        Monitor.Exit(_isInProgressLock);
                }
            }, Task.Factory.CancellationToken);
        }

        public void DownloadJobToLocal()
        {
            Task.Factory.StartNew(() =>
            {
                var acquired = false;
                try
                {
                    acquired = Monitor.TryEnter(_isInProgressLock);
                    if (acquired)
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
                    else
                    {
                        MessageCallBack("An Operation is already in progress");
                    }
                }
                finally
                {
                    if (acquired)
                        Monitor.Exit(_isInProgressLock);
                }
            }, Task.Factory.CancellationToken);
        }

        public void DeleteJobsFromDatabase()
        {
            DatabaseCleaner.DeleteJobs(MessageCallBack);
        }
    }
}