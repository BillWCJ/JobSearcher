using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Business.DataBaseSeeder;
using Business.Manager;
using Data.EF.JseDb;
using Data.IO.Local;
using Model.Definition;
using Model.Entities.JobMine;

namespace UWActuallyWorks.PostingTable
{
    public class PostingTableViewModel
    {
        public ObservableCollection<JobPostingViewModel> JobPostings { get; private set; }

        protected JobReviewManager JobReviewManager;
        
        public PostingTableViewModel()
        {
            JobReviewManager = new JobReviewManager();
            var jobs = JobSearcher.FindJobs();
            var jobPostingViewModels = jobs.Select(job => new JobPostingViewModel(job));
            JobPostings = new ObservableCollection<JobPostingViewModel>(jobPostingViewModels);
        }
    }
}