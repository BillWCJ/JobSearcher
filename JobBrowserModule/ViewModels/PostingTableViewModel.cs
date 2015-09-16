using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Business.Manager;
using JobBrowserModule.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using Model.Entities;
using Model.Entities.JobMine;
using Presentation.WPF;
using Presentation.WPF.Events;

namespace JobBrowserModule.ViewModels
{
    public interface IPostingTableViewModel
    {
        ICollectionView JobPostings { get; }
        bool IsAllSelected { get; set; }
        void FilterChanged(IEnumerable<Filter> filters);
        void SelectedJobChanged(Job job);
        void AddSelectedJobsToShortList(string name);
        ObservableCollection<string> ShortListNames { get; set; } 
    }

    internal class PostingTableViewModelMock : IPostingTableViewModel
    {
        public ICollectionView JobPostings { get; private set; }
        public bool IsAllSelected { get; set; }

        public void FilterChanged(IEnumerable<Filter> filters)
        {
        }

        public void SelectedJobChanged(Job job)
        {
        }

        public void AddSelectedJobsToShortList(string name)
        {
        }

        public ObservableCollection<string> ShortListNames { get; set; }

        public PostingTableViewModelMock()
        {
            var jobs = JobManager.FindJobs();
            var jobPostingViewModels = jobs.Select(job => new JobPostingViewModel(job));
            JobPostings = CollectionViewSource.GetDefaultView(jobPostingViewModels);
        }
    }

    public class PostingTableViewModel : ViewModelBase, IPostingTableViewModel
    {
        private ICollectionView _jobPostings;
        private IEnumerable<Filter> _activeFilters = new List<Filter>();
        private EventAggregator _aggregator;
        private bool _isAllSelected;
        protected JobReviewManager JobReviewManager;

        public PostingTableViewModel()
        {
        }

        public PostingTableViewModel(EventAggregator aggregator) : this()
        {
            _aggregator = aggregator;
            _aggregator.GetEvent<FilterSelectionChangedEvent>().Subscribe(FilterChanged);
            _aggregator.GetEvent<JobDownloadCompleted>().Subscribe((a) => PopulateTable());
            PopulateTable();
            //ShortListNames = new ObservableCollection<string>(LocalShortListManager.GetListOfShortListNames());
        }

        private void PopulateTable()
        {
            JobReviewManager = new JobReviewManager();
            var jobs = JobManager.FindJobs();
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

        public void FilterChanged(IEnumerable<Filter> filters)
        {
            _activeFilters = filters;
            JobPostings.Refresh();
        }

        public void SelectedJobChanged(Job job)
        {
            if (_aggregator != null)
                _aggregator.GetEvent<SelectedJobChangedEvent>().Publish(job);
        }

        public void AddSelectedJobsToShortList(string name)
        {
            foreach (var jobPostingViewModel in JobPostings.Cast<JobPostingViewModel>().Where(j => j.IsSelected))
            {
                if (LocalShortListManager.AddJobToShortList(jobPostingViewModel.Job, name))
                {
                    if(!ShortListNames.Contains(name))
                        ShortListNames.Add(name);
                }
            }
        }

        public ObservableCollection<string> ShortListNames { get; set; }

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