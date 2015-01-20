using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.JobMine;

namespace Model.Entities
{
    public class JobSearchModel : Job
    {
        public JobSearchModel()
        {
            Job = new Job();
        }
        public JobSearchModel(Job job)
        {
            Job = job;
        }
        public Job Job { get; set; }
        public int Score;
    }
}
