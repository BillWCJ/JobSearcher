using System;
using System.Collections.ObjectModel;
using Business.Manager;
using JobBrowserModule.ViewModels;
using Microsoft.Practices.Prism.PubSubEvents;
using Model.Entities.JobMine;
using Presentation.WPF;
using Presentation.WPF.Events;

namespace JobDetailModule
{
    public class JobDetailViewModel : ViewModelBase
    {
        private Job _currentJob;
        private EventAggregator _aggregator;

        public JobDetailViewModel() { }

        public JobDetailViewModel(EventAggregator aggregator) : this()
        {
            _aggregator = aggregator;
            _aggregator.GetEvent<SelectedJobChangedEvent>().Subscribe(JobChanged);
        }

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
                OnPropertyChanged("GoogleSearchUrl");
                OnPropertyChanged("GoogleMapUrl");
            }
        }

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
        }
        public void AddSelectedJobToShortList(string name)
        {
            if (LocalShortListManager.AddJobToShortList(CurrentJob, name))
            {
                if (!ShortListNames.Contains(name))
                    ShortListNames.Add(name);
            }

        }

        public ObservableCollection<string> ShortListNames { get; set; }

        public string GoogleSearchUrl
        {
            get
            { 
                return @"http://www.google.com/search?q=" + (CurrentJob == null ? "" : CurrentJob.Employer.Name);
            }
        }

        public string GoogleMapUrl
        {
            get
            {
                //ie mode
                return @"http://maps.google.com/?q=" + (CurrentJob == null ? "" : CurrentJob.Employer.Name + " " + CurrentJob.JobLocation.Region);
            }
        }
    }
}