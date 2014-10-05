using System;
using System.Diagnostics;
using System.IO;
using GlobalVariable;
using Model.Entities;

namespace Data.Web.JobMine
{
    public static class Login
    {
        /// <summary>
        /// Get the JobMine login data collection
        /// </summary>
        /// <param name="userName">Optional Username</param>
        /// <param name="password">Optional Password</param>
        /// <returns>login date NameValueCollection</returns>
        /// <exception cref="Exception">Thrown when Account not initalized</exception>
        public static System.Collections.Specialized.NameValueCollection LoginData(string userName = "", string password ="")
        {
            if(!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                return new System.Collections.Specialized.NameValueCollection
                {
                    {"userid", userName},
                    {"pwd", password},
                    {"submit", "Submit"},
                    {"timezoneOffset", "240"}
                };

            if (GVar.Account == null)
                throw new Exception("User Account not initalized");
            return new System.Collections.Specialized.NameValueCollection
            {
                {"userid", string.IsNullOrEmpty(userName)? GVar.Account.Username : userName},
                {"pwd", string.IsNullOrEmpty(password)? GVar.Account.Password : password},
                {"submit", "Submit"},
                {"timezoneOffset", "240"}
            };
        }

        /// <summary>
        /// Check wether the CookieEnabledWebClient is loggined into JobMine
        /// </summary>
        /// <param name="client"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool LoginToJobMine(CookieEnabledWebClient client, string userName = "", string password = "")
        {
            var logindata = LoginData(userName, password);
            string result = client.UploadValues(GVar.LogInUrl, "POST", logindata).ToString();
            return !String.IsNullOrEmpty(result) && client.CookieContainer.Count > 5;
        }

        /// <summary>
        /// Get a new CookieEnabledWebClient that has logged into JobMine
        /// </summary>
        /// <returns>CookieEnabledWebClient</returns>
        public static CookieEnabledWebClient NewJobMineLoggedInWebClient()
        {
            var client = new CookieEnabledWebClient();
            if (LoginToJobMine(client))
                return client;
            else
                throw new Exception("Cannot LogIn");
        }
    }
}