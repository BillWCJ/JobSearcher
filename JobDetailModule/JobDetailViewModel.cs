using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JobBrowserModule.Annotations;
using Model.Entities.JobMine;

namespace JobDetailModule
{
    public class JobDetailViewModel : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
