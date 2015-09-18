using System;
using System.Collections;
using System.Collections.Specialized;
using System.Net;
using System.Reflection;

namespace Model.Entities.Web
{
    /// <summary>
    ///     WebClient with Cookie and UserAgent
    /// </summary>
    public class CookieEnabledWebClient : WebClient, ICookieEnabledWebClient
    {
        /// <summary>
        ///     Initalizes a new instatnce of WebClientExtension.CookieEnabledWebClient
        /// </summary>
        public CookieEnabledWebClient()
        {
            CookieContainer = new CookieContainer();
            UserAgent = DefaultUserAgent;
        }

        /// <summary>
        ///     Get the System.String DefaultUserAgent for the class object
        /// </summary>
        public string DefaultUserAgent
        {
            get
            {
                return @"Mozilla/5.0 (Windows NT 6.3; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0";
            }
        }

        /// <summary>
        ///     Get the System.Net.CookieContainer CookieContainer that contains cookies for the current object
        /// </summary>
        public CookieContainer CookieContainer { get; private set; }

        /// <summary>
        ///     Get or Set the System.String UserAgent for the current object
        /// </summary>
        public string UserAgent { get; set; }

        public string DownloadString(string address, NameValueCollection data, string method = "POST")
        {
            return UploadValues(address, method, data).ToString();
        }

        /// <summary>
        ///     Returns a System.Net.WebRequest object for the specified resource.
        /// </summary>
        /// <param name="address">A System.Uri that identifies the resource to request.</param>
        /// <returns>A new System.Net.WebRequest object for the specified resource.</returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = CookieContainer;
                (request as HttpWebRequest).UserAgent = UserAgent;
            }
            return request;
        }

        public CookieCollection GetAllCookies()
        {
            CookieCollection cookieCollection = new CookieCollection();

            Hashtable table = (Hashtable)this.CookieContainer.GetType().InvokeMember("m_domainTable",
                                                                            BindingFlags.NonPublic |
                                                                            BindingFlags.GetField |
                                                                            BindingFlags.Instance,
                                                                            null,
                                                                            this.CookieContainer,
                                                                            new object[] { });

            foreach (var tableKey in table.Keys)
            {
                String str_tableKey = (string)tableKey;

                if (str_tableKey[0] == '.')
                {
                    str_tableKey = str_tableKey.Substring(1);
                }

                SortedList list = (SortedList)table[tableKey].GetType().InvokeMember("m_list",
                                                                            BindingFlags.NonPublic |
                                                                            BindingFlags.GetField |
                                                                            BindingFlags.Instance,
                                                                            null,
                                                                            table[tableKey],
                                                                            new object[] { });

                foreach (var listKey in list.Keys)
                {
                    String url = "https://" + str_tableKey + (string)listKey;
                    cookieCollection.Add(this.CookieContainer.GetCookies(new Uri(url)));
                }
            }

            return cookieCollection;
        }
    }
}