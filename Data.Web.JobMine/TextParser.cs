using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Model.Definition;
using Model.Entities;

namespace Data.Web.JobMine
{
    // todo: Use Html parser instead of string operation
    public class TextParser
    {
        UserAccount Account { get; set; }
        public TextParser(UserAccount account)
        {
            Account = account;
        }
        public static Job GetJobFromTextFile(string sourceString, string jobId)
        {
            var fields = new string[JobMineDef.JobDetailPageFieldNameTitles.Length - 1];
            int indexStart = 0;
            for (int i = 0; i < JobMineDef.JobDetailPageFieldNameTitles.Length - 1; i++)
            {
                indexStart =
                    sourceString.IndexOf(JobMineDef.JobDetailPageFieldNameTitles[i], indexStart,
                        StringComparison.InvariantCulture) +
                    JobMineDef.JobDetailPageFieldNameTitles[i].Length;
                int indexEnd = sourceString.IndexOf(JobMineDef.JobDetailPageFieldNameTitles[i + 1], indexStart,
                    StringComparison.InvariantCulture);
                fields[i] = sourceString.Substring(indexStart, indexEnd - indexStart).TrimEnd('\n');
            }

            return new Job
            {
                Employer = new Employer {Name = fields[0]},
                JobTitle = fields[1],
                Location = new Location {Region = fields[2]},
                Disciplines = new Disciplines(fields[3]),
                Levels = new Levels(fields[4]),
                Comment = fields[5],
                JobDescription = fields[6],
                Id = Convert.ToInt32(jobId),
            };
        }

        public Job[] ReadInJobFromLocal()
        {
            int indexStart = 0;
            Queue<string> jobId = GetJobIdsFromLocal();
            int numberOfJobs = jobId.Count;
            var jobs = new Job[numberOfJobs];
            string data = String.Empty;
            StreamReader reader = StreamReader.Null;

            try
            {
                reader = new StreamReader(Account.FilePath + "Jobs.txt");
                data = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_ReadInJobFromLocal: {1}\n", e, e);
            }
            try
            {
                for (int i = 0; i < numberOfJobs; i++)
                {
                    indexStart = data.IndexOf(JobDef.SeperationBar, indexStart,StringComparison.InvariantCulture) + JobDef.SeperationBar.Length;
                    int indexEnd = 0;
                    indexEnd = i != numberOfJobs - 1 ? data.IndexOf(JobDef.SeperationBar, indexStart, StringComparison.InvariantCulture) : data.Length;
                    jobs[i] = GetJobFromTextFile(data.Substring(indexStart, indexEnd - indexStart), jobId.Dequeue());
                }
            }
            finally
            {
                reader.Close();
            }

            return jobs;
        }

        public Queue<string> GetJobIdsFromLocal()
        {
            var jobID = new Queue<string>();
            StreamReader reader = OpenFileForStreamReader(Account.FilePath, "JobList.txt");
            if (reader != null)
            {
                while (!reader.EndOfStream)
                {
                    string jobIdString = reader.ReadLine();
                    if (JobInquiry.IsCorrectJobId(jobIdString))
                        jobID.Enqueue(jobIdString);
                }
                reader.Close();
            }
            return jobID;
        }

        public static StreamWriter OpenFileForStreamWriter(string fileLocation, string fileName)
        {
            fileName = ConvertAndValidateFileName(fileName);
            try
            {
                return new StreamWriter(fileLocation + fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}-{1}:{2}-{3}:{4}", MethodBase.GetCurrentMethod().Name, e,
                    e.Message, e.StackTrace, e.Data);
                return null;
            }
        }

        public static StreamReader OpenFileForStreamReader(string fileLocation, string fileName)
        {
            fileName = ConvertAndValidateFileName(fileName);
            try
            {
                return new StreamReader(fileLocation + fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}-{1}:{2}-{3}:{4}", MethodBase.GetCurrentMethod().Name, e,
                    e.Message, e.StackTrace, e.Data);
                return null;
            }
        }

        private static string ConvertAndValidateFileName(string fileName)
        {
            if (!fileName.Contains(".txt"))
                fileName += ".txt";
            return fileName;
        }
    }
}