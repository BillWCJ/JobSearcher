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
            string temp = String.Empty;
            StreamReader reader = StreamReader.Null;
            try
            {
                reader = new StreamReader(GVar.FilePath + "JobList.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_GetJobIDFromLocal: {1}\n", e.ToString(), e);
            }
            try
            {
                while (!reader.EndOfStream)
	            {
	                temp = reader.ReadLine();
                    if (IsCorrectJobID(temp))
                        jobID.Enqueue(temp);
	            }
            }
            finally
            {
                reader.Close();
            }
            return jobID;
        }

        public static bool IsCorrectJobID(string temp)
        {
            Regex regex = new Regex("[0-9]{8,8}");
            bool right = regex.IsMatch(temp);
            return regex.IsMatch(temp);
        }

        public static void DownLoadJobsFromWebToLocal()
        {
            CookieEnabledWebClient client = NewJobMineLoggedInWebClient();
            DownLoadJobsFromWebToLocal(client);
        }

        public static void DownLoadJobsFromWebToLocal(CookieEnabledWebClient client)
        {
            string info = String.Empty;
            StreamReader reader = StreamReader.Null;
            StreamWriter writer = StreamWriter.Null;

            try
            {
                reader = new StreamReader(GVar.FilePath + "JobList.txt");
                writer = new StreamWriter(GVar.FilePath + "Jobs.txt");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("!Error-FileNotFoundException_In_GetJobsFromWeb: {0}\n", e);
            }
            catch (IOException e)
            {
                Console.WriteLine("!Error-IOException_In_GetJobsFromWeb: {0}\n", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_GetJobsFromWeb: {1}\n", e.ToString(), e);
            }

            try
            {
                writer.Write("Extract Time:" + DateTime.Now.ToString("h:mm:ss tt"));
                while (!reader.EndOfStream)
                {
                    string jobnum = reader.ReadLine();
                    string url = GVar.JobDetailBaseUrl + jobnum;
                    info = client.DownloadString(url);
                    writer.Write(ExtractHtmlJobInfo(info, url).ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_GetJobsFromWeb: {1}\n", e.ToString(), e);
            }


            if (reader != StreamReader.Null)
            {
                reader.Close();
            }
            if (writer != StreamWriter.Null)
            {
                writer.Close();
                Process.Start(GVar.FilePath + "Jobs.txt");
            }
        }
        public static void DownLoadJobsFromWebToLocal(CookieEnabledWebClient client, Queue<string> jobIDs)
        {
            string info = String.Empty;
            StreamWriter writer = StreamWriter.Null;

            try
            {
                writer = new StreamWriter(GVar.FilePath + "Jobs.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_GetJobsFromWeb: {1}\n", e.ToString(), e);
            }

            try
            {
                writer.Write("Extract Time:" + DateTime.Now.ToString("h:mm:ss tt"));
                while (jobIDs.Count > 0)
                {
                    string jobnum = jobIDs.Dequeue();
                    string url = GVar.JobDetailBaseUrl + jobnum;
                    info = client.DownloadString(url);
                    writer.Write(ExtractHtmlJobInfo(info, url).ToString());
                    
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_GetJobsFromWeb: {1}\n", e.ToString(), e);
            }
            if (writer != StreamWriter.Null)
            {
                writer.Close();
                Process.Start(GVar.FilePath + "Jobs.txt");
            }
        }
    }
}
