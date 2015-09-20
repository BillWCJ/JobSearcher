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
        private static readonly ObservableCollection<EmployerReview> NoResultCollection = new ObservableCollection<EmployerReview>(new List<EmployerReview>{new EmployerReview{Name = "No Result"}});

        public ObservableCollection<EmployerReview> EmployerReviews
        {
            get
            {
                return _employerReviews ?? (_employerReviews = NoResultCollection);
            }
            set
            {
                _employerReviews = value.Count > 0 ? value : NoResultCollection;
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
