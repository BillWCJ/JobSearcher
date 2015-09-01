using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Utility;
using Data.EF.JseDb;
using Model.Definition;
using Model.Entities;
using Model.Entities.JobMine;

namespace Business.Manager
{
    public class JobManager
    {
        public static List<Job> FindJobs()
        {
            using (var db = new JseDbContext())
            {
                var jobList = db.Jobs.Include(j => j.Levels).Include(j => j.Disciplines).Include(j => j.JobLocation).Include(j => j.Employer).ToList();
                Console.WriteLine(jobList.Count);
                return jobList;
            }
        }


        public static TermType GetTermDuration(Job job)
        {
            TermType value;
                
            if (string.IsNullOrWhiteSpace(job.Comment))
            {
                if (string.IsNullOrWhiteSpace(job.JobDescription))
                    return TermType.Unknown;

                value = CheckString(job.JobDescription);
            }
            else
            {
                value = CheckString(job.Comment);
                if (value == TermType.Unknown)
                    value = CheckString(job.JobDescription);
            }

            //assume 4 month
            if (value == TermType.Unknown)
                value = TermType.Four;

            return value;
        }

        private static TermType CheckString(string data)
        {
            if (data.IsNullSpaceOrEmpty())
            {
                return TermType.Unknown;;
            }

            bool isFourMonth = false, isEightMonth = false;

            string lowerCase = data.ToLower();

            if(lowerCase.Contains("4 or 8 month") || lowerCase.Contains("four or eight month"))
                return TermType.Both;

            if (lowerCase.Contains("4 month") || lowerCase.Contains("4-month") || lowerCase.Contains("four month"))
            {
                isFourMonth = true;
            }

            if (lowerCase.Contains("8 month") || lowerCase.Contains("8-month") || lowerCase.Contains("eight month"))
            {
                isEightMonth = true;
            }

            if(isFourMonth && isEightMonth)
                return TermType.Both;
            
            if(isFourMonth)
                return TermType.Four;

            if(isEightMonth)
                return TermType.Eight;

            return TermType.Unknown;
        }
    }
}
