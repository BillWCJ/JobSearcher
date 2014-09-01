using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Web;
using System.Net;
using GlobalVariable;
using WebClientExtension;
using Jobs;

namespace ContentProcess
{
    public static class ContentExtraction
    {
        public static CookieEnabledWebClient SetUpCookieEnabledWebClientForLogIn()
        {
            CookieEnabledWebClient client = new CookieEnabledWebClient();
            client.Headers.Add("user-agent", GVar.UserAgent);
            client.BaseAddress = GVar.LogInUrl;
            NameValueCollection loginData = SetLoginData();
            client.UploadValues(GVar.LogInUrl, "POST", loginData);
            return client;
        }

        public static NameValueCollection SetLoginData()
        {
            NameValueCollection loginData = new NameValueCollection();
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

        public static bool IsAtLevel(string level, string htmlSource)
        {
            return htmlSource.IndexOf(level) > -1;
        }

        public static string returninfo(Job newJob)
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
        public static Disciplines ExtractDisciplines(string data)
        {
            bool[] isThisDiscipline = new bool[GVar.DisciplinesNames.Length];
            for (int i = 0; i < isThisDiscipline.Length; i++)
                isThisDiscipline[i] = data.IndexOf(GVar.DisciplinesNames[i]) > -1;
            return new Disciplines(isThisDiscipline);
        }
    }
}
