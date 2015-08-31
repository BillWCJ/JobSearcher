using Microsoft.Practices.Prism.PubSubEvents;
using Model.Entities.JobMine;
using Presentation.WPF;
using Presentation.WPF.Events;

namespace JobDetailModule
{
    public abstract class JobDetailViewModelBase : ViewModelBase
    {
        protected Job _currentJob;
        protected EventAggregator _aggregator;

        protected JobDetailViewModelBase(EventAggregator aggregator)
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
                NotifyPropertyChanged();
            }
        }

        protected abstract void NotifyPropertyChanged();

        public void JobChanged(Job newJob)
        {
            CurrentJob = newJob;
        }
    }
}