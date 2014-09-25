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
using JobMine;

namespace JobMine
{
    public static class Login
    {
        public static System.Collections.Specialized.NameValueCollection LoginData()
        {
            var loginData = new System.Collections.Specialized.NameValueCollection
            {
                {"userid", GVar.Account.User.UserName},
                {"pwd", GVar.Account.User.Password},
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

        public static void GenerateIsLoggedInFile()
        {
            CookieEnabledWebClient client = NewJobMineLoggedInWebClient();
            string url = GVar.TestJobDetailUrl;
            string data = JobDetail.ExtractHtmlJobInfo(client.DownloadString(url), url).ToString();
            StreamWriter writer = new StreamWriter(GVar.FilePath + "JobDetailForConfirmLogIn.txt");
            writer.Write(data);
            writer.Close();
            Process.Start(GVar.FilePath + "JobDetailForConfirmLogIn.txt");
        }
    }
}