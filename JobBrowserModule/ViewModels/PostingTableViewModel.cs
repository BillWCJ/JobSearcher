using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Business.Manager;
using JobBrowserModule.Services;

namespace JobBrowserModule.ViewModels
{
    public class PostingTableViewModel
    {
        private readonly ICollectionView _jobPostings;
        private IList<FilterViewModel> _filters = new List<FilterViewModel>();
        private bool _isAllSelected;
        protected JobReviewManager JobReviewManager;

        public PostingTableViewModel()
        {
            JobReviewManager = new JobReviewManager();
            var jobs = JobSearcher.FindJobs();
            var jobPostingViewModels = jobs.Select(job => new JobPostingViewModel(job));
            _jobPostings = CollectionViewSource.GetDefaultView(jobPostingViewModels);
            _jobPostings.Filter += JobPostingFilter;
        }

        public ICollectionView JobPostings
        {
            get
            {
                return _jobPostings;
            }
        }

        public bool IsAllSelected
        {
            get
            {
                return _isAllSelected;
            }
            set
            {
                if (value == _isAllSelected)
                    return;
                _isAllSelected = value;
                foreach (JobPostingViewModel posting in JobPostings)
                {
                    posting.IsSelected = _isAllSelected;
                }
            }
        }

        private bool JobPostingFilter(object item)
        {
            var jobPosting = item as JobPostingViewModel;
            if (jobPosting == null)
                return false;

            var isVisible = FilterHelper.IsPostingVisible(jobPosting, _filters);

            if (!isVisible)
                jobPosting.IsSelected = false;

            return isVisible;
        }

        public void FilterChanged(IEnumerable<FilterViewModel> filters)
        {
            _filters = filters.ToList();
            JobPostings.Refresh();
        }
    }
}