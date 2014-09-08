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
        public static NameValueCollection LoginData()
        {
            NameValueCollection loginData = new NameValueCollection();
            loginData.Add("userid", GVar.UserID);
            loginData.Add("pwd", GVar.UserPW);
            loginData.Add("submit", "Submit");
            loginData.Add("timezoneOffset", "240");
            return loginData;
        }

        public static CookieEnabledWebClient SetUpClient(string url, NameValueCollection postData)
        {
            CookieEnabledWebClient client = new CookieEnabledWebClient();
            client.UploadValues(url, "POST", postData);
            return client;
        }

        public static string ConfirmLogin()
        {
            CookieEnabledWebClient client = ContentExtraction.SetUpClient(GVar.LogInUrl, ContentExtraction.LoginData());
            return ConfirmLogin(client);
        }
            
        public static string ConfirmLogin(CookieEnabledWebClient client)
        {
            string url = GVar.TestJobDetailUrl;
            string data = client.DownloadString(url);
            string processedData = ContentExtraction.ExtractHtmlJobInfo(data, url).ToString();
            StreamReader reader = new StreamReader(GVar.LocationFilePath + "JobDetailForConfirmLogIn.txt");
            string baseData = reader.ReadToEnd();
            if (baseData.Length == processedData.Length)
                return "LoggedIn";
            else return processedData.Substring(100, 20) + "\n\n! Not Logged In";
        }

        public static void GenerateConfirmLoginFile()
        {
            CookieEnabledWebClient client = ContentExtraction.SetUpClient(GVar.LogInUrl, ContentExtraction.LoginData());
            string url = GVar.TestJobDetailUrl;
            string data = ContentExtraction.ExtractHtmlJobInfo(client.DownloadString(url), url).ToString();
            StreamWriter writer = new StreamWriter(GVar.LocationFilePath + "JobDetailForConfirmLogIn.txt");
            writer.Write(data);
            writer.Close();
            Process.Start(GVar.LocationFilePath + "JobDetailForConfirmLogIn.txt");
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
            string extractedString = string.Empty;
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
            string data = string.Empty;
            StreamReader reader = StreamReader.Null;

            try
            {
                reader = new StreamReader(GVar.LocationFilePath + "Jobs.txt");
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
            string temp = string.Empty;
            StreamReader reader = StreamReader.Null;
            try
            {
                reader = new StreamReader(GVar.LocationFilePath + "JobList.txt");
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

        public static void GetJobsFromWeb()
        {
            CookieEnabledWebClient client = ContentExtraction.SetUpClient(GVar.LogInUrl, ContentExtraction.LoginData());
            GetJobsFromWeb(client);
        }

        public static void GetJobsFromWeb(CookieEnabledWebClient client)
        {
            string info = string.Empty;
            StreamReader reader = StreamReader.Null;
            StreamWriter writer = StreamWriter.Null;

            try
            {
                reader = new StreamReader(GVar.LocationFilePath + "JobList.txt");
                writer = new StreamWriter(GVar.LocationFilePath + "Jobs.txt");
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
                    writer.Write(ContentExtraction.ExtractHtmlJobInfo(info, url).ToString());
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
                Process.Start(GVar.LocationFilePath + "Jobs.txt");
            }
        }
    }
}
