using System;
using System.Diagnostics;
using System.IO;
using GlobalVariable;
using Model.Entities;

namespace Data.Web.JobMine
{
    public static class Login
    {
        public static System.Collections.Specialized.NameValueCollection LoginData()
        {
            var loginData = new System.Collections.Specialized.NameValueCollection
            {
                {"userid", GVar.Account.Username},
                {"pwd", GVar.Account.Password},
                {"submit", "Submit"},
                {"timezoneOffset", "240"}
            };
            return loginData;
        }

        public static bool LoginToJobmine(CookieEnabledWebClient client)
        {
            string result = client.UploadValues(GVar.LogInUrl, "POST", LoginData()).ToString();
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

        public static void GenerateIsLoggedInFile(string userName, string password)
        {
            CookieEnabledWebClient client = NewJobMineLoggedInWebClient();
            string data = JobDetail.GetJob(client.DownloadString(GVar.TestJobDetailUrl), GVar.TestJobId).ToString();
            StreamWriter writer = new StreamWriter(GVar.FilePath + "JobDetailForConfirmLogIn.txt");
            writer.Write(data);
            writer.Close();
            Process.Start(GVar.FilePath + "JobDetailForConfirmLogIn.txt");
        }
    }
}