using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using Data.Contract.JobMine.Interface;
using Model.Definition;
using Model.Entities;
using Model.Entities.JobMine;
using Model.Entities.Web;

namespace Data.Web.JobMine.DataSource
{
    public class JobDetail : IJobDetail
    {
        static ICookieEnabledWebClient Client { get; set; }
        public JobDetail(ICookieEnabledWebClient client)
        {
            Client = client;
        }

        private static Job GetJob(string htmlSource, string jobId) //todo: improve using html parsing
        {
            var fields = new string[JobMineDef.FieldSearchString.Length];
            for (int i = 0; i < JobMineDef.FieldSearchString.Length; i++)
                fields[i] =
                    WebUtility.HtmlDecode(
                        ExtractField(htmlSource, JobMineDef.FieldSearchString[i], "</span>").Replace("&nbsp;", " "))
                        .Replace("<br />", "\n");

            fields[3] +=
                WebUtility.HtmlDecode(
                    ExtractField(htmlSource, "id='UW_CO_JOBDTL_DW_UW_CO_DESCR100'>", "</span>").Replace("&nbsp;", " "))
                    .Replace("<br />", "\n");

            return new Job
            {
                Employer = new Employer
                {
                    Name = fields[0]
                },
                JobTitle = fields[1],
                JobLocation = new JobLocation
                {
                    Region = fields[2]
                },
                Disciplines = new Disciplines(fields[3]),
                Levels = new Levels(fields[4]),
                Comment = fields[5],
                JobDescription = fields[6],
                Id = Convert.ToInt32(jobId)
            };
        }

        public Job GetJob(JobOverView jov)
        {
            string jobId = jov.IdString;
            string url = JobMineDef.JobDetailBaseUrl + jobId;
            string htmlSource = Client.DownloadString(url);
            Job job = GetJob(htmlSource, jobId);
            job.Employer.UnitName = jov.Employer.UnitName;
            job.NumberOfOpening = jov.NumberOfOpening;
            job.NumberOfApplied = jov.NumberOfApplied;
            job.AlreadyApplied = jov.AlreadyApplied;
            job.OnShortList = jov.OnShortList;
            job.LastDateToApply = jov.LastDateToApply;

            if (!job.Employer.Name.Contains(jov.Employer.Name))
            {
                job.Employer.Name = job.Employer.Name.Split(',')[0];
                Console.WriteLine("Not The Same: {0}", jov.Employer.Name);
            }
            else
                job.Employer.Name = jov.Employer.Name;

            return job;
        }

        private static string ExtractField(string data, string front, string back)
        {
            int start = data.IndexOf(front, StringComparison.InvariantCulture) + front.Length;
            int end = data.IndexOf(back, start, StringComparison.InvariantCulture);
            string extractedString;
            try
            {
                extractedString = data.Substring(start, end - start);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Trace.Write(e);
                throw;
            }
            return extractedString;
        }

        public IEnumerable<string> DownLoadAndWriteJobsToLocal(Queue<string> jobIDs, string fileLocation, uint numJobsPerFile = 100)
        {
            bool success = true;
            for (int currentFilePart = 1; jobIDs.Count > 0; currentFilePart++)
            {
                yield return string.Format("Writing JobDetailPart {0} ({1} Jobs Per File)\n", currentFilePart, numJobsPerFile);

                try
                {
                    var writer = new StreamWriter(fileLocation + ("JobDetailPart" + currentFilePart + ".txt"));
                    writer.Write("Download Time:" + DateTime.Now.ToString("s"));
                    for (uint currentFileJobCount = 0; currentFileJobCount < numJobsPerFile && jobIDs.Count > 0; currentFileJobCount++)
                    {
                        string currentJobId = jobIDs.Dequeue();
                        string url = JobMineDef.JobDetailBaseUrl + currentJobId;
                        Job job = GetJob(Client.DownloadString(url), currentJobId);
                        writer.Write(job.ToString());
                    }
                    writer.Close();
                }
                catch (Exception e)
                {
                    success = false;
                    Console.WriteLine(e);
                }
                if (success)
                    yield return string.Format("Finished Writing JobDetailPart" + currentFilePart + "\n");
                else
                    yield return string.Format("Writing JobDetailPart" + currentFilePart + " Failed, Existing \n");
            }
        }
    }
}