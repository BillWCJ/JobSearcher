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
            var jobList = new List<Job>();
            var jobManager = new JobManager();
            using (var db = new JseDbContext())
            {
                foreach (Job j in db.Jobs.Include(j => j.Levels).Include(j => j.Disciplines).Include(j => j.JobLocation).Include(j => j.Employer))
                {
                    if (IsNotEightMonth(jobManager, j) && IsMyLevel(j.Levels) && IsRelatedDiscipline(j.Disciplines) )//&& IsNotQaJob(j))
                    {
                        j.Score = 0;
                        //location score
                        if (ContainWord(j.JobLocation.Region, "ottawa"))
                            j.Score += 5;
                        else if (ContainWord(j.JobLocation.Region, "usa"))
                            j.Score += 5;
                        else if (ContainWord(j.JobLocation.Region, "toronto"))
                            j.Score += 2;
                        else if (ContainWord(j.JobLocation.Region, "waterloo"))
                            j.Score += 2;
                        else if (ContainWord(j.JobLocation.Region, "kitchener"))
                            j.Score += 2;

                        //level score
                        if (j.Levels.IsJunior)
                            j.Score += 3;

                        //discipline score
                        if (j.Disciplines.ContainDiscipline(DisciplineEnum.ENGMechatronics))
                            j.Score += 2;

                        //--------skill & keywords
                        //mech
                        if (ContainWord(j.JobDescription, "solidwork"))
                            j.Score += 5;
                        else if (ContainWord(j.JobDescription, "cad"))
                            j.Score += 3;
                        if (ContainWord(j.JobDescription, "fea"))
                            j.Score += 2;

                        //software
                        if (ContainWord(j.JobDescription, "c#"))
                            j.Score += 5;
                        else if (ContainWord(j.JobDescription, "c++"))
                            j.Score += 5;

                        if (ContainWord(j.JobDescription, "arduino"))
                            j.Score += 10;


                        if (ContainWord(j.JobTitle, "hardware"))
                            j.Score += 10;

                        j.Score += 100 * j.NumberOfOpening/(1+j.NumberOfApplied);

                        if(!j.AlreadyApplied)
                        jobList.Add(j);
                    }
                }
                Console.WriteLine(jobList.Count);
                jobList.Sort(SortByScore);
                return jobList;
            }
        }

        private static int SortByScore(Job x, Job y)
        {
            if (x == null)
                return 1;
            if (y == null)
                return -1;
            if (x.Score >= y.Score)
                return -1;
            return 1;
        }

        private static bool IsNotEightMonth(JobManager jobManager, Job j)
        {
            return JobManager.GetTermDuration(j) != TermType.Eight;
        }

        private static bool IsMyLevel(Levels levels)
        {
            return levels.IsJunior || levels.IsIntermediate;
        }

        private static bool IsRelatedDiscipline(Disciplines d)
        {
            return d.ContainDiscipline(DisciplineEnum.ENGMechatronics)
                || d.ContainDiscipline(DisciplineEnum.ENGMechanical)
                || d.ContainDiscipline(DisciplineEnum.ENGElectrical)
                || d.ContainDiscipline(DisciplineEnum.ENGComputer)
                || d.ContainDiscipline(DisciplineEnum.ENGUnSpecified)
                || d.ContainDiscipline(DisciplineEnum.ENGManagement)
                || d.ContainDiscipline(DisciplineEnum.ENGSystemsDesign)
                || d.ContainDiscipline(DisciplineEnum.ENGSoftware) 
                || d.ContainDiscipline(DisciplineEnum.MATHComputerScience);
        }

        private static bool ContainWord(string src, string word)
        {
            return src.IndexOf(word, StringComparison.OrdinalIgnoreCase) > 0;
        }

        private static bool IsNotQaJob(Job job)
        {
            return !ContainWord(job.JobTitle, "Quality") && !ContainWord(job.JobTitle, "QA") && !ContainWord(job.JobTitle, "verification") && !ContainWord(job.JobTitle, "test");
        }
    }
}
