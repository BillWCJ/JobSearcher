using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.EF.ClusterDB;
using Model.Entities;

namespace Business.Manager
{
    public static class JobManager
    {
        public static TermType GetTermDuration(Job job=null)
        {
            var value = (TermType) 0;
            using (var db = new DatabaseContext())
            {
                
                foreach (var j in db.Jobs)
                {
                    if (string.IsNullOrWhiteSpace(j.Comment))
                    {
                        if (string.IsNullOrWhiteSpace(j.JobDescription))
                            throw new ArgumentException("Empty description field");

                        value = CheckString(j.JobDescription);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Job title: {0}\nJob description: {1}\nTerm Duration: {2}\n JobId: {3} ",
                            j.JobTitle,j.JobDescription,value, j.Id);
                        Console.ResetColor();
                        Console.WriteLine("-----------------------------------------------------");
                    }
                    else
                    {
                        value = CheckString(j.Comment);
                        if (value == TermType.Not_Specified)
                            value = CheckString(j.JobDescription);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Job title: {0}\nJob comment: {1}\nTerm Duration: {2}\n JobId: {3}",
                            j.JobTitle, j.Comment, value, j.Id);
                        Console.ResetColor();
                        Console.WriteLine("-----------------------------------------------------");
                    }

                }
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
                comment.ToLower().Contains("8 month"))
            {
                return TermType.Eight;
            }

            return TermType.Not_Specified;
        }
    }
}
