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
    public class JobDetailViewModel : JobDetailViewModelBase
    {
        public JobDetailViewModel() : base(new EventAggregator())
        { }

        public JobDetailViewModel(EventAggregator aggregator) : base (aggregator)
        { }

        public string CurrentJobString
        {
            get
            {
                return CurrentJob != null ? CurrentJob.ToString("") : "No Job Selected";
            }
        }

        protected override void NotifyPropertyChanged()
        {
            OnPropertyChanged("CurrentJobString");
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
    }
}