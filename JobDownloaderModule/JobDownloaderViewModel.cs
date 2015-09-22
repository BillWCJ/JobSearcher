using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Business.DataBaseSeeder;
using Business.Manager;
using Common.Utility;
using Data.Contract.JobMine;
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
        private readonly object _isDisplayingMessageLock = new object();
        private readonly object _isInProgressLock = new object();
        private readonly UserAccountManager _userAccountManager;
        private string _progress = string.Empty;

        public JobDownloaderViewModel() : base(new EventAggregator())
        {
            _userAccountManager = new UserAccountManager();
            SetUp();
        }

        public JobDownloaderViewModel(EventAggregator aggregator, UserAccountManager userAccountManager) :
            base(aggregator)
        {
            _userAccountManager = userAccountManager;
            SetUp();
        }

        public string UserName
        {
            get
            {
                return _userAccountManager.UserAccount.JobMineUsername;
            }
            set
            {
                _userAccountManager.UserAccount.JobMineUsername = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get
            {
                return _userAccountManager.UserAccount.JobMinePassword;
            }
            set
            {
                _userAccountManager.UserAccount.JobMinePassword = value;
                OnPropertyChanged();
            }
        }

        public string Term
        {
            get
            {
                return _userAccountManager.UserAccount.Term;
            }
            set
            {
                _userAccountManager.UserAccount.Term = value;
                OnPropertyChanged();
            }
        }

        public string FileLocation
        {
            get
            {
                return _userAccountManager.UserAccount.FilePath;
            }
            set
            {
                _userAccountManager.UserAccount.FilePath = value;
                OnPropertyChanged();
            }
        }

        public int JobStatusSelectionIndex
        {
            get
            {
                var index = 0;
                switch (_userAccountManager.UserAccount.JobStatus)
                {
                    case JobStatus.Posted:
                        index = 0;
                        break;
                    case JobStatus.AppsAvail:
                        index = 1;
                        break;
                    case JobStatus.Cancelled:
                        index = 2;
                        break;
                    case JobStatus.Approved:
                        index = 3;
                        break;
                    default:
                        _userAccountManager.UserAccount.JobStatus = JobStatus.Approved;
                        index = 3;
                        break;
                }
                return index;
            }
            set
            {
                string jobStatus;
                switch (value)
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
                _userAccountManager.UserAccount.JobStatus = jobStatus;
                OnPropertyChanged();
            }
        }

        public string DownloadFormat { get; set; }
        public int NumberOfJobPerFile { get; set; }
        public bool SeedLocation { get; set; }
        public bool SeedJobRating { get; set; }
        public string GoogleMapApiKeyString { get; set; }
        private Action<string> MessageCallBack { get; set; }

        public string Progress
        {
            get
            {
                return _progress;
            }
        }

        public string BeginText
        {
            get
            {
                if (_userAccountManager.UserAccount.JobMineUsername.IsNullSpaceOrEmpty())
                {
                    return "Welcome \n please enter the required information and follow the instruction to setup below.";
                }
                return "Welcome \n please enter your password inorder for the program to add job posting to the JobMine shortlist.";
            }
        }

        public string CurrentStatus { get; set; }

        private void SetUp()
        {
            NumberOfJobPerFile = 100;
            FileLocation = "c:\\";
            MessageCallBack = message =>
            {
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Render, (Action<string>) (UpdateProgress), message);
            };
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
                var payload = newMessage.Replace(CommonDef.CurrentStatus, "");
                Aggregator.GetEvent<CurrentStatusMessageChangedEvent>().Publish(payload);
                CurrentStatus = payload;
                OnPropertyChanged("CurrentStatus");
            }
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
            _userAccountManager.SaveAccount();
            Task.Factory.StartNew(() =>
            {
                var acquired = false;
                try
                {
                    acquired = Monitor.TryEnter(_isInProgressLock);
                    if (acquired)
                    {
                        CurrentStatus = "In Progress";
                        OnPropertyChanged("CurrentStatus");
                        MasterSeeder.SeedAll(MessageCallBack, _userAccountManager.UserAccount, Term, _userAccountManager.UserAccount.JobStatus, true);
                        Aggregator.GetEvent<JobDownloadCompletedEvent>().Publish(true);
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
                    CurrentStatus = "";
                    OnPropertyChanged("CurrentStatus");
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
                                foreach (var msg in jobMineManager.DownLoadJobs(UserName, Password, Term, _userAccountManager.UserAccount.JobStatus, FileLocation))
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