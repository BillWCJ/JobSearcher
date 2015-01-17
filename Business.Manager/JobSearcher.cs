using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.EF.JseDb;
using Model.Definition;
using Model.Entities.JobMine;

namespace Business.Manager
{
    public class JobSearcher
    {
        public static List<Job> FindJobs()
        {
            var jobIWant = new List<Job>();
            var jobICanGet = new List<Job>();
            var jobManager = new JobManager();
            using (var db = new JseDbContext())
            {
                foreach (Job j in db.Jobs.Include(j => j.Levels).Include(j => j.Disciplines).Include(j => j.JobLocation).Include(j => j.Employer))
                {
                    TermType termType = jobManager.GetTermDuration(j);
                    if (termType != TermType.Eight && NotQaJob(j)
                        && (j.Disciplines.ContainDiscipline(DisciplineEnum.ENGMechatronics) || j.Disciplines.ContainDiscipline(DisciplineEnum.ENGMechanical) || j.Disciplines.ContainDiscipline(DisciplineEnum.MATHComputerScience)))
                    {
                        jobIWant.Add(j);
                        if ((j.Levels.IsJunior || j.Levels.IsIntermediate) && (ContainWord(j.JobDescription, "c#") || ContainWord(j.JobDescription, "solidwork")) && j.Disciplines.ContainDiscipline(DisciplineEnum.ENGMechatronics))
                        {
                            jobICanGet.Add(j);
                        }
                    }
                }
                Console.WriteLine(jobIWant.Count);
                Console.WriteLine(jobICanGet.Count);
                return jobICanGet;
            }
        }

        private static bool ContainWord(string src, string word)
        {
            return src.IndexOf(word, StringComparison.OrdinalIgnoreCase) > 0;
        }

        private static bool NotQaJob(Job job)
        {
            return !ContainWord(job.JobTitle, "Quality") && !ContainWord(job.JobTitle, "QA") && !ContainWord(job.JobTitle, "verification");
        }
    }
}
