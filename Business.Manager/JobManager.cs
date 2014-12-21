using System;
using Model.Definition;
using Model.Entities;

namespace Business.Manager
{
    public class JobManager
    {
        public static TermType GetTermDuration(Job job)
        {
            TermType value;
                
            if (string.IsNullOrWhiteSpace(job.Comment))
            {
                if (string.IsNullOrWhiteSpace(job.JobDescription))
                    throw new ArgumentException("Empty description field");

                value = CheckString(job.JobDescription);
            }
            else
            {
                value = CheckString(job.Comment);
                if (value == TermType.NotSpecified)
                    value = CheckString(job.JobDescription);
            }
            return value;
        }

        private static TermType CheckString(string comment)
        {
            if ((comment.ToLower().Contains("4 month") &&
                comment.ToLower().Contains("8 month")) ||
                (comment.ToLower().Contains("4 months") &&
                comment.ToLower().Contains("8 months")) ||
                (comment.ToLower().Contains("4 or 8 months")))
            {
                return TermType.Both;
            }

            if (comment.ToLower().Contains("4 months") ||
                comment.ToLower().Contains("4 month"))
            {
                return TermType.Four;
            }

            if (comment.ToLower().Contains("8 months") ||
                comment.ToLower().Contains("8-months") ||
                comment.ToLower().Contains("8 month") ||
                comment.ToLower().Contains("8-month"))
            {
                return TermType.Eight;
            }

            return TermType.NotSpecified;
        }
    }
}
