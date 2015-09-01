using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Manager;
using Microsoft.Practices.Prism.PubSubEvents;
using Model.Entities.RateMyCoopJob;

namespace JobDetailModule
{
    public class JobRatingViewModel  : JobDetailViewModelBase
    {
        private ObservableCollection<EmployerReview> _employerReviews;

        public ObservableCollection<EmployerReview> EmployerReviews
        {
            get
            {
                return _employerReviews ?? (_employerReviews = new ObservableCollection<EmployerReview>());
            }
            set
            {
                _employerReviews = value;
                OnPropertyChanged();
            }
        }

        public JobRatingViewModel() : base(new EventAggregator())
        { }

        public JobRatingViewModel(EventAggregator aggregator) : base(aggregator)
        {
        }

        protected override void NotifyPropertyChanged()
        {
            EmployerReviews = new ObservableCollection<EmployerReview>(JobReviewManager.GetEmployerReview(CurrentJob.Employer.Name));
        }
    }
}
