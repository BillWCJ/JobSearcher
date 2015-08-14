using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Business.Manager;
using JobBrowserModule.Services;
using Model.Entities.JobMine;

namespace JobBrowserModule.ViewModels
{
    public interface IPostingTableViewModel
    {
        ICollectionView JobPostings { get; }
        bool IsAllSelected { get; set; }
        void FilterChanged(IList<FilterViewModel> filters);
        void SelectedJobChanged(Job job);
    }

    internal class PostingTableViewModelMock : IPostingTableViewModel
    {
        public ICollectionView JobPostings { get; private set; }
        public bool IsAllSelected { get; set; }

        public void FilterChanged(IList<FilterViewModel> filters)
        {
        }

        public void SelectedJobChanged(Job job)
        {
        }
    }

    public class PostingTableViewModel : ViewModelBase, IPostingTableViewModel
    {
        private readonly ICollectionView _jobPostings;
        private IList<FilterViewModel> _activeFilters = new List<FilterViewModel>();
        private IReporter _aggregator;
        private bool _isAllSelected;
        protected JobReviewManager JobReviewManager;

        public PostingTableViewModel()
        {
        }

        public PostingTableViewModel(IReporter aggregator) : this()
        {
            _aggregator = aggregator;
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

        public void FilterChanged(IList<FilterViewModel> filters)
        {
            _activeFilters = filters;
            JobPostings.Refresh();
        }

        public void SelectedJobChanged(Job job)
        {
            if (_aggregator.SelectedJobChanged == null)
                return;
            _aggregator.SelectedJobChanged(job);
        }

        private bool JobPostingFilter(object item)
        {
            var jobPosting = item as JobPostingViewModel;
            if (jobPosting == null)
                return false;

            var isVisible = FilterHelper.IsPostingVisible(jobPosting, _activeFilters);

            if (!isVisible)
                jobPosting.IsSelected = false;

            return isVisible;
        }
    }
}