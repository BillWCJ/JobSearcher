using System;
using Model.Entities.JobMine;
using Presentation.WPF;

namespace JobDetailModule
{
    public class JobDetailViewModel : ViewModelBase
    {
        private Job _currentJob;

        public Job CurrentJob
        {
            get
            {
                return _currentJob;
            }
            set
            {
                _currentJob = value;
                OnPropertyChanged("CurrentJobString");
            }
        }

        public Action JobChangedCallBack { get; set; }

        public string CurrentJobString
        {
            get
            {
                return CurrentJob != null ? CurrentJob.ToString("") : "No Job Selected";
            }
        }

        public void JobChanged(Job newJob)
        {
            CurrentJob = newJob;
            if (JobChangedCallBack != null)
                JobChangedCallBack();
        }
    }
}