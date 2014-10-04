using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using GlobalVariable;
using Model.Entities;

namespace Data.Web.JobMine
{
    public static class JobDetail
    {
        public static Job GetJob(string htmlSource, string jobId) //todo: improve using html parsing
        {
            var fields = new string[GVar.FieldSearchString.Length];
            for (int i = 0; i < GVar.FieldSearchString.Length; i++)
                fields[i] =
                    WebUtility.HtmlDecode(
                        ExtractField(htmlSource, GVar.FieldSearchString[i], "</span>").Replace("&nbsp;", " "))
                        .Replace("<br />", "\n");

            fields[3] +=
                WebUtility.HtmlDecode(
                    ExtractField(htmlSource, "id='UW_CO_JOBDTL_DW_UW_CO_DESCR100'>", "</span>").Replace("&nbsp;", " "))
                    .Replace("<br />", "\n");
            return new Job
            {
                Employer = new Employer { Name = fields[0] },
                JobTitle = fields[1],
                Location = new Location { Region = fields[2] },
                Disciplines = new Disciplines(fields[3]),
                Levels = new Levels(fields[4]),
                Comment = fields[5],
                JobDescription = fields[6],
                JobMineId = Convert.ToInt32(jobId),
            };
        }

        public static Job GetJob(CookieEnabledWebClient client, JobOverView jobOverView)
        {
            string jobId = jobOverView.IdString;
            string url = GVar.JobDetailBaseUrl + jobId;
            string htmlSource = client.DownloadString(url);
            Job job = GetJob(htmlSource, jobId);
            job.Employer.UnitName = jobOverView.Employer.UnitName;

            bool theSame = true;
            if (job.Employer.Name.IndexOf(jobOverView.Employer.Name, StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                job.Employer.Name = jobOverView.Employer.Name;
            }
            else
            {
                theSame = false;
            }
            if (!job.Location.Region.Equals(jobOverView.Location.Region, StringComparison.InvariantCultureIgnoreCase))
                theSame = false;
            if (!job.JobTitle.Equals(jobOverView.JobTitle, StringComparison.InvariantCultureIgnoreCase))
                theSame = false;
            if (!theSame)
                Console.WriteLine("Not The Same: {0}", jobId);

            return job;
        }

        public static string ExtractField(string data, string front, string back)
        {
            int start = data.IndexOf(front, StringComparison.InvariantCulture) + front.Length;
            int end = data.IndexOf(back, start, StringComparison.InvariantCulture);
            string extractedString = String.Empty;
            try
            {
                extractedString = data.Substring(start, end - start);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("!Error-ArgumentOutOfRangeException_In_ExtractField: {0}\n", e);
            }
            return extractedString;
        }

        public static void DownLoadAndWriteJobsToLocal(Queue<string> jobIDs, CookieEnabledWebClient client, string fileLocation = GVar.FilePath, uint numJobsPerFile = 100)
        {
            try
            {
                for (uint currentFilePart = 1; jobIDs.Count > 0; currentFilePart++)
                {
                    StreamWriter writer = TextParser.OpenFileForStreamWriter(fileLocation,
                        "JobDetailPart" + currentFilePart + ".txt");
                    Console.WriteLine("Writing JobDetailPart {0} ({1} Jobs Per File)", currentFilePart, numJobsPerFile);
                    writer.Write("Download Time:" + DateTime.Now.ToString("s"));
                    for (uint currentFileJobCount = 0;
                        currentFileJobCount < numJobsPerFile && jobIDs.Count > 0;
                        currentFileJobCount++)
                    {
                        string currentJobId = jobIDs.Dequeue();
                        string url = GVar.JobDetailBaseUrl + currentJobId;
                        writer.Write(GetJob(client.DownloadString(url), currentJobId).ToString());
                    }
                    writer.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}-{1}:{2}-{3}:{4}", MethodBase.GetCurrentMethod().Name, e, e.Message, e.StackTrace,
                    e.Data);
            }
        }
    }
}