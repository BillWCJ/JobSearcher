using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;
using System.Net;
using System.Windows;
using System.Diagnostics;
using WebClientExtension;
using Jobs;
using GlobalVariable;

namespace ConsoleApplication {
    class program {
        static void Main(string[] args) {
            string data = string.Empty;
            WebClientCookieEnabled client = SetUpWebClientCookieEnabled();
            data = client.DownloadString(GVar.JobSearchUrl);
            File.WriteAllText(GVar.LocationFilePath + "ConfirmLogIn.html", data);
            Process.Start(GVar.LocationFilePath + "ConfirmLogIn.html");

            StreamReader sr = new StreamReader(GVar.LocationFilePath + "JobList.txt");
            StreamWriter op = new StreamWriter(GVar.LocationFilePath + "Jobs.txt");

            op.Write("Extract Time:" + DateTime.Now.ToString("h:mm:ss tt"));
            while (!sr.EndOfStream) {
                string jobnum = sr.ReadLine();
                string url = GVar.JobDetailBaseUrl + jobnum;
                data = client.DownloadString(url);
                op.Write(returninfo(ExtractJobInfo(data,url)));
            }

            sr.Close();
            op.Close();
            Process.Start(GVar.LocationFilePath + "Jobs.txt");

        }

        public static WebClientCookieEnabled SetUpWebClientCookieEnabled()
        {
            WebClientCookieEnabled client = new WebClientCookieEnabled();
            client.Headers.Add("user-agent", GVar.UserAgent);
            client.BaseAddress = GVar.LogInUrl;
            NameValueCollection loginData = SetLoginData();
            client.UploadValues(GVar.LogInUrl, "POST", loginData);
            return client;
        }

        public static NameValueCollection SetLoginData()
        {
            var loginData = new NameValueCollection();
            loginData.Add("userid", GVar.UserID);
            loginData.Add("pwd", GVar.UserPW);
            loginData.Add("submit", "Submit");
            loginData.Add("timezoneOffset", "240");
            return loginData;
        }

        public static Job ExtractJobInfo(string htmlSource, string url)
        {
            string[] fieldNames = { "Employer", "Job Title", "Location", "Discriplines", "Levels", "Comment", "Description" };
            string[] fieldSearchString = {"id='UW_CO_JOBDTL_DW_UW_CO_EMPUNITDIV'>",
                                   "id='UW_CO_JOBDTL_VW_UW_CO_JOB_TITLE'>",
                                   "id='UW_CO_JOBDTL_VW_UW_CO_WORK_LOCATN'>",
                                   "id='UW_CO_JOBDTL_DW_UW_CO_DESCR'>",
                                   "id='UW_CO_JOBDTL_DW_UW_CO_DESCR_100'>",
                                   "id='UW_CO_JOBDTL_VW_UW_CO_JOB_SUMMARY'>",
                                    "id='UW_CO_JOBDTL_VW_UW_CO_JOB_DESCR'>"};

            string[] fields = new string[7];
            for (int i = 0; i < 7; i++)
                fields[i] = WebUtility.HtmlDecode(ExtractField(htmlSource, fieldSearchString[i], "</span>").Replace("&nbsp;", " ")).Replace("<br />", "\n");

            fields[3] += WebUtility.HtmlDecode(ExtractField(htmlSource, "id='UW_CO_JOBDTL_DW_UW_CO_DESCR100'>", "</span>").Replace("&nbsp;", " ")).Replace("<br />", "\n");
            
            Job newJob = new Job();            
            newJob.EmployerName = fields[0];
            newJob.JobTitle = fields[1];
            newJob.Location = fields[2];
            newJob.Disciplines = ExtractDisciplines(fields[3]);
            newJob.Levels = new Levels(IsAtLevel("Junior", fields[4]), IsAtLevel("Intermediate", fields[4]), IsAtLevel("Senior", fields[4]));
            newJob.Comment = fields[5];
            newJob.JobDescription = fields[6];
            newJob.JobUrl = url;
            return newJob;
        }

        static bool IsAtLevel(string level, string htmlSource)
        {
            return htmlSource.IndexOf(level) > -1;
        }
        static string returninfo(Job newJob)
        {
            string[] fieldNames = { "Employer", "Job Title", "Location", "Discriplines", "Levels", "Comment", "Description" };
            for (int i = 0; i < fieldNames.Length; i++)
                fieldNames[i] += ":" + Environment.NewLine;

            string info = "\n\n-------------------------------------------------------------------------\n";
            info += fieldNames[0] + newJob.EmployerName + "\n\n";
            info += fieldNames[1] + newJob.JobTitle + "\n\n";
            info += fieldNames[2] + newJob.Location + "\n\n";
            info += fieldNames[3] + newJob.Disciplines + "\n\n";
            info += fieldNames[4] + newJob.Levels + "\n\n";
            info += fieldNames[5] + newJob.Comment + "\n\n";
            info += fieldNames[6] + newJob.JobDescription + "\n\n";
            info += newJob.JobUrl;
            return info;
        }

        static string ExtractField(string data, string front, string back) {
            int start = data.IndexOf(front) + front.Length;
            int end = data.IndexOf(back,start);
            return data.Substring(start,end-start);
        }

        bool processdis (bool [] dis, int size, string data){
            string [] disci = new string []{"ENG-Mechatronics", "ENG-Mechanical", "ENG-Civil", "ENG-Computer", "ENG-Electrical", 
                "ENG-Management", "ENG-Software", "ENG-Systems Design", "MATH-Computer Science"};
            
            size = disci.Length;

            //bool[] target = new bool[size]; 
 
            for (int i = 0; i < size; i ++){
                dis[i] = data.IndexOf(disci[i]) > 0 ? true : false;
            }
            return true;
        }

        public static Disciplines ExtractDisciplines (string data)
        {
            bool[] isThisDiscipline = new bool[GVar.DisciplinesNames.Length];
            for (int i = 0; i < isThisDiscipline.Length; i++)
                isThisDiscipline[i] = data.IndexOf(GVar.DisciplinesNames[i]) > -1;
            return new Disciplines(isThisDiscipline);
        }
    }
}
