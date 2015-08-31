using Microsoft.Practices.Prism.PubSubEvents;

namespace JobDetailModule
{
    public class GoogleMapSearchViewModel : JobDetailViewModelBase
    {
        public GoogleMapSearchViewModel() : base(new EventAggregator())
        { }

        public GoogleMapSearchViewModel(EventAggregator aggregator)
            : base(aggregator)
        { }

        protected override void NotifyPropertyChanged()
        {
            OnPropertyChanged("GoogleMapUrl");
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