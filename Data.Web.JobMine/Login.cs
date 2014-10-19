using System;
using System.Diagnostics;
using System.IO;
using Model.Definition;
using Model.Entities;

namespace Data.Web.JobMine
{
    public class Login
    {
        UserAccount Account { get; set; }

        public Login(UserAccount account)
        {
            Account = account;
        }

        /// <summary>
        /// Get the JobMine login data collection
        /// </summary>
        /// <param name="userName">Optional Username</param>
        /// <param name="password">Optional Password</param>
        /// <returns>login date NameValueCollection</returns>
        /// <exception cref="Exception">Thrown when UserAccount not initalized</exception>
        public System.Collections.Specialized.NameValueCollection LoginData(string userName = "", string password ="")
        {
            if(!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                return new System.Collections.Specialized.NameValueCollection
                {
                    {"userid", userName},
                    {"pwd", password},
                    {"submit", "Submit"},
                    {"timezoneOffset", "240"}
                };

            if (Account == null)
                throw new Exception("User UserAccount not initalized");
            return new System.Collections.Specialized.NameValueCollection
            {
                {"userid", string.IsNullOrEmpty(userName)? Account.Username : userName},
                {"pwd", string.IsNullOrEmpty(password)? Account.Password : password},
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
        public bool LoginToJobMine(CookieEnabledWebClient client, string userName = "", string password = "")
        {
            var logindata = LoginData(userName, password);
            string result = client.UploadValues(JobMineDef.LogInUrl, "POST", logindata).ToString();
            return !String.IsNullOrEmpty(result) && client.CookieContainer.Count > 5;
        }

        /// <summary>
        /// Get a new CookieEnabledWebClient that has logged into JobMine
        /// </summary>
        /// <returns>CookieEnabledWebClient</returns>
        public CookieEnabledWebClient NewJobMineLoggedInWebClient()
        {
            var client = new CookieEnabledWebClient();
            if (LoginToJobMine(client))
                return client;
            else
                throw new Exception("Cannot LogIn");
        }
    }
}