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

namespace ContentProcess
{
    public static class ContentExtraction
    {
        public static bool LoginToJobmine(CookieEnabledWebClient client)
        {
            string result = client.UploadValues(GVar.LogInUrl, "POST", GVar.LoginData()).ToString();
            return !String.IsNullOrEmpty(result) || IsLoggedInToJobmine(client);
        }

        public static bool IsLoggedInToJobmine(CookieEnabledWebClient client)
        {
            if (client.CookieContainer != null)
            {
                return true;
            }
            return false;
        }
        public static CookieEnabledWebClient NewJobMineLoggedInWebClient()
        {
            CookieEnabledWebClient client = new CookieEnabledWebClient();
            bool isLoggedIn = LoginToJobmine(client);
            if (isLoggedIn)
            {
                return client;
            }
            else
            {
                throw new Exception("Cannot LogIn");
            }
        } 

        public static void GenerateIsLoggedInFile()
        {
            CookieEnabledWebClient client = NewJobMineLoggedInWebClient();
            string url = GVar.TestJobDetailUrl;
            string data = ExtractHtmlJobInfo(client.DownloadString(url), url).ToString();
            StreamWriter writer = new StreamWriter(GVar.FilePath + "JobDetailForConfirmLogIn.txt");
            writer.Write(data);
            writer.Close();
            Process.Start(GVar.FilePath + "JobDetailForConfirmLogIn.txt");
        }

        public static Job ExtractHtmlJobInfo(string htmlSource, string url) //todo: improve
        {
            string[] fields = new string[GVar.FieldSearchString.Length+1];
            for (int i = 0; i < GVar.FieldSearchString.Length; i++)
                fields[i] = WebUtility.HtmlDecode(ExtractField(htmlSource, GVar.FieldSearchString[i], "</span>").Replace("&nbsp;", " ")).Replace("<br />", "\n");

            fields[3] += WebUtility.HtmlDecode(ExtractField(htmlSource, "id='UW_CO_JOBDTL_DW_UW_CO_DESCR100'>", "</span>").Replace("&nbsp;", " ")).Replace("<br />", "\n");
            fields[7] = url;
            return new Job(fields);
        }

        public static Job ExtractTextFileJobInfo(string sourceString, string url) //todo: improve
        {
            string[] fields = new string[GVar.JobDetailPageFieldNameTitles.Length];
            int indexStart = 0;
            int indexEnd = 0;
            for (int i = 0; i < GVar.JobDetailPageFieldNameTitles.Length - 1; i++)
            {
                indexStart = sourceString.IndexOf(GVar.JobDetailPageFieldNameTitles[i], indexStart) + GVar.JobDetailPageFieldNameTitles[i].Length;
                indexEnd = sourceString.IndexOf(GVar.JobDetailPageFieldNameTitles[i+1], indexStart);
                fields[i] = sourceString.Substring(indexStart, indexEnd-indexStart).TrimEnd('\n');
            }
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

        public static Job[] ReadInJobFromLocal()
        {
            int indexStart = 0;
            int indexEnd = 0;
            Queue<string> jobID = GetJobIDFromLocal();
            int numberOfJob = jobID.Count;
            Job[] jobs = new Job[numberOfJob];
            string data = String.Empty;
            StreamReader reader = StreamReader.Null;

            try
            {
                reader = new StreamReader(GVar.FilePath + "Jobs.txt");
                data = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_ReadInJobFromLocal: {1}\n", e.ToString(), e);
            }
            try
            {
                for (int i = 0; i < numberOfJob; i++)
                {
                    indexStart = data.IndexOf(GVar.SeperationBar, indexStart) + GVar.SeperationBar.Length;
                    if (i != numberOfJob - 1)
                    {
                        indexEnd = data.IndexOf(GVar.SeperationBar, indexStart);
                    }
                    else
                    {
                        indexEnd = data.Length;
                    }
                    jobs[i] = ExtractTextFileJobInfo(data.Substring(indexStart, indexEnd - indexStart),GVar.JobDetailBaseUrl+jobID.Dequeue());
                }
            }
            finally
            {
                reader.Close();
            }

            return jobs;
        }

        public static Queue<string> GetJobIDFromLocal()
        {
            Queue<string> jobID = new Queue<string>();
            StreamReader reader = OpenFileForStreamReader(GVar.FilePath, "JobList.txt");
            if(reader != null)
            {
                while (!reader.EndOfStream)
	            {
                    string jobIdString = reader.ReadLine();
                    if (IsCorrectJobID(jobIdString))
                        jobID.Enqueue(jobIdString);
	            }
                reader.Close();
            }
            return jobID;
        }

        public static bool IsCorrectJobID(string jobId)
        {
            Regex regex = new Regex("[0-9]{8,8}");
            bool right = regex.IsMatch(jobId);
            return regex.IsMatch(jobId);
        }

        public static void DownLoadJobsFromWebToLocal()
        {
            CookieEnabledWebClient client = NewJobMineLoggedInWebClient();
            DownLoadJobsFromWebToLocal(client, GetJobIDFromLocal(),GVar.FilePath,100);
        }
        public static void DownLoadJobsFromWebToLocal(Queue<string> jobIDs)
        {
            CookieEnabledWebClient client = NewJobMineLoggedInWebClient();
            DownLoadJobsFromWebToLocal(client, jobIDs, GVar.FilePath, 100);
        }
        public static void DownLoadJobsFromWebToLocal(CookieEnabledWebClient client, Queue<string> jobIDs, string fileLocation, uint numJobsPerFile)
        {
            try
            {
                for (uint currentFilePart = 1; jobIDs.Count > 0; currentFilePart++)
                {
                    StreamWriter writer = OpenFileForStreamWriter(fileLocation, "JobDetailPart"+currentFilePart+".txt");
                    Console.WriteLine("Writing JobDetailPart {0} ({1} Jobs Per File)", currentFilePart,numJobsPerFile);
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

        private static StreamWriter OpenFileForStreamWriter(string fileLocation, string fileName)
        {
            StreamWriter writer = StreamWriter.Null;
            if (fileName.IndexOf(".txt") == -1)
            {
                fileName += ".txt";
            }
            try
            {
                writer = new StreamWriter(fileLocation + fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}-{1}:{2}-{3}:{4}", System.Reflection.MethodBase.GetCurrentMethod().Name, e.ToString(),
                    e.Message, e.StackTrace, e.Data);
            }
            return writer;
        }
        private static StreamReader OpenFileForStreamReader(string fileLocation, string fileName)
        {
            StreamReader reader = StreamReader.Null;
            if (fileName.IndexOf(".txt") == -1)
            {
                fileName += ".txt";
            }
            try
            {
                reader = new StreamReader(fileLocation + fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}-{1}:{2}-{3}:{4}", System.Reflection.MethodBase.GetCurrentMethod().Name, e.ToString(),
                    e.Message, e.StackTrace, e.Data);
            }
            return reader;
        }
    }
}
