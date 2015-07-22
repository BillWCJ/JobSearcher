using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.JobMine;

namespace UWActuallyWorks.PostingTable
{
    public class JobPostingViewModel
    {
        public Job Job { get; private set; }

        public bool IsSelected { get; set; }

        public JobPostingViewModel(Job job)
        {
            this.Job = job;
        }
    }
}
