using Microsoft.Practices.Prism.PubSubEvents;
using Presentation.WPF.Events;

namespace JobDetailModule
{
    public class GoogleSearchViewModel : JobDetailViewModelBase
    {
        public GoogleSearchViewModel() : base(new EventAggregator())
        { }

        public GoogleSearchViewModel(EventAggregator aggregator)
            : base(aggregator)
        { }

        protected override void NotifyPropertyChanged()
        {
            OnPropertyChanged("GoogleSearchUrl");
        }

        public string GoogleSearchUrl
        {
            get
            { 
                return @"http://www.google.com/search?q=" + (CurrentJob == null ? "" : CurrentJob.Employer.Name);
            }
        }
    }
}