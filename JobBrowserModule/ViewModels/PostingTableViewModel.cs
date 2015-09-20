using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Business.Manager;
using Common.Utility;
using JobBrowserModule.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using Model.Definition;
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
        string SearchOrCancelIcon { get; }
        string SearchOrCancelIconToolTip { get; }
        string SearchKeyWord { get; set; }
        string TableInfo { get; }
        ObservableCollection<string> ShortListNames { get; set; }
        void KeyWordSearchToggled();
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
        public void KeyWordSearchToggled()
        {
        }

        public string SearchOrCancelIcon { get; set; }

        public string SearchOrCancelIconToolTip { get; set; }

        public string SearchKeyWord { get; set; }

        public string TableInfo { get; private set; }

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
        private bool _isAllSelected;
        protected JobReviewManager JobReviewManager;

        private Filter _keyWordSearch = new Filter()
        {
            Category = FilterCategory.StringSearch,
            StringSearchTargets = new List<StringSearchTarget> {StringSearchTarget.Job}
        };

        private const string SearchIcon = @"../Icons/Search.png";
        private const string SearchIconToolTip = @"Search key word in all jobs";
        private const string CancelIcon = @"../Icons/Cross.png";
        private const string CancelIconToolTip = @"Remove search filter";


        private void SetUp()
        {
            SearchOrCancelIcon = SearchIcon;
            SearchOrCancelIconToolTip = SearchIconToolTip;
            SearchKeyWord = string.Empty;
        }

        public PostingTableViewModel(EventAggregator aggregator) : base(aggregator)
        {
            SetUp();
            Aggregator.GetEvent<FilterSelectionChangedEvent>().Subscribe(FilterChanged);
            Aggregator.GetEvent<JobDownloadCompletedEvent>().Subscribe((a) => PopulateTable());
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
            OnPropertyChanged("TableInfo");
        }

        public void SelectedJobChanged(Job job)
        {
            if (Aggregator != null)
                Aggregator.GetEvent<SelectedJobChangedEvent>().Publish(job);
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

        public string SearchOrCancelIcon { get; private set; }
        public string SearchOrCancelIconToolTip { get; private set; }
        public string SearchKeyWord { get; set; }

        public string TableInfo
        {
            get
            {
                int count = JobPostings.Cast<object>().Count();
                return "Number of jobs visible: {0}".FormatString(count);
            }
        }

        public ObservableCollection<string> ShortListNames { get; set; }
        public void KeyWordSearchToggled()
        {
            var filters = _activeFilters.ToList();
            if (SearchOrCancelIcon == CancelIcon)
            {
                filters.Remove(_keyWordSearch);
                SearchOrCancelIcon = SearchIcon;
                SearchOrCancelIconToolTip = SearchIconToolTip;
                SearchKeyWord = "";
            }
            else
            {
                _keyWordSearch.StringSearchValues = new List<string>{SearchKeyWord};
                filters.Add(_keyWordSearch);
                SearchOrCancelIcon = CancelIcon;
                SearchOrCancelIconToolTip = CancelIconToolTip;
            }
            FilterChanged(filters);
            OnPropertyChanged("SearchOrCancelIcon");
            OnPropertyChanged("SearchOrCancelIconToolTip");
            OnPropertyChanged("SearchKeyWord");
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