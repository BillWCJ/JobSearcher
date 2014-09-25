using System;
using System.IO;
using System.Web;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Jobs;
using GlobalVariable;
using WebClientExtension;


namespace JobMine
{
    public static class JobDetail
    {
        public static Job ExtractHtmlJobInfo(string htmlSource, string url) //todo: improve
        {
            string[] fields = new string[GVar.FieldSearchString.Length + 1];
            for (int i = 0; i < GVar.FieldSearchString.Length; i++)
                fields[i] = WebUtility.HtmlDecode(ExtractField(htmlSource, GVar.FieldSearchString[i], "</span>").Replace("&nbsp;", " ")).Replace("<br />", "\n");

            fields[3] += WebUtility.HtmlDecode(ExtractField(htmlSource, "id='UW_CO_JOBDTL_DW_UW_CO_DESCR100'>", "</span>").Replace("&nbsp;", " ")).Replace("<br />", "\n");
            fields[7] = url;
            return new Job(fields);
        }
        public static string ExtractField(string data, string front, string back)
        {
            int start = data.IndexOf(front) + front.Length;
            int end = data.IndexOf(back, start);
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
        public static void DownLoadJobsFromWebToLocal()
        {
            CookieEnabledWebClient client = Login.NewJobMineLoggedInWebClient();
            DownLoadJobsFromWebToLocal(client, JobMine.TextParser.GetJobIDFromLocal(), GVar.FilePath, 100);
        }
        public static void DownLoadJobsFromWebToLocal(Queue<string> jobIDs)
        {
            CookieEnabledWebClient client = Login.NewJobMineLoggedInWebClient();
            DownLoadJobsFromWebToLocal(client, jobIDs, GVar.FilePath, 100);
        }
        public static void DownLoadJobsFromWebToLocal(CookieEnabledWebClient client, Queue<string> jobIDs, string fileLocation, uint numJobsPerFile)
        {
            try
            {
                for (uint currentFilePart = 1; jobIDs.Count > 0; currentFilePart++)
                {
                    StreamWriter writer = JobMine.TextParser.OpenFileForStreamWriter(fileLocation, "JobDetailPart" + currentFilePart + ".txt");
                    Console.WriteLine("Writing JobDetailPart {0} ({1} Jobs Per File)", currentFilePart, numJobsPerFile);
                    writer.Write("Download Time:" + DateTime.Now.ToString("s"));
                    for (uint currentFileJobCount = 0; currentFileJobCount < numJobsPerFile && jobIDs.Count > 0; currentFileJobCount++)
                    {
                        string url = GVar.JobDetailBaseUrl + jobIDs.Dequeue();
                        writer.Write(ExtractHtmlJobInfo(client.DownloadString(url), url).ToString());
                    }
                    writer.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}-{1}:{2}-{3}:{4}", System.Reflection.MethodBase.GetCurrentMethod().Name, e.ToString(), e.Message, e.StackTrace, e.Data);
            }
        }

    }
}