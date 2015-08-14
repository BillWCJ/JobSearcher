using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Business.DataBaseSeeder;
using Business.Manager;
using Data.Contract.JobMine;
using Data.IO.Local;
using Data.Web.JobMine;
using JobBrowserModule.Annotations;
using Model.Definition;

namespace JobDownloaderModule
{
    public class JobDownloaderViewModel : INotifyPropertyChanged
    {
        public JobDownloaderViewModel()
        {
            NumberOfJobPerFile = 100;
            FileLocation = "c:\\";
            ProgressStringBuilder = new StringBuilder();
            MessageCallBack = message =>
            {
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Render, (Action<string>)(UpdateProgress), message);
            };

        }

        private void UpdateProgress(string newMessage)
        {
            lock (_isDisplayingMessageLock)
            {
                ProgressStringBuilder.AppendLine(newMessage);
                OnPropertyChanged("Progress");
            }
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Term { get; set; }
        public int JobStatusSelectionIndex { get; set; }

        public string FileLocation
        {
            get
            {
                return _fileLocation;
            }
            set
            {
                _fileLocation = value;
                OnPropertyChanged();
            }
        }

        public string DownloadFormat { get; set; }
        public uint NumberOfJobPerFile { get; set; }
        private readonly object _isInProgressLock = new object();
        private readonly object _isDisplayingMessageLock = new object();
        private string _fileLocation;
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
            ExecuteDownloadOrExport(() =>
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
            });
        }

        public void DownloadAndSeedJobIntoDb()
        {
            ExecuteDownloadOrExport(() =>
            {
                var account = new JseLocalRepo().GetAccount();
                foreach (var msg in MasterSeeder.SeedAll(Term, GetJobStatus(), account, false, false))
                {
                    MessageCallBack(msg);
                }
            });

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
            ExecuteDownloadOrExport(() =>
            {
                LocalDownloadManager.ExportJob(MessageCallBack, FileLocation, NumberOfJobPerFile);
            });
        }

        private void ExecuteDownloadOrExport(Action action)
        {
            bool acquired = false;
            try
            {
                acquired = Monitor.TryEnter(_isInProgressLock);
                if (acquired)
                {
                    Task task = new Task(action);
                    task.Start();
                    Monitor.Exit(_isInProgressLock);
                }
            }
            catch
            {
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}