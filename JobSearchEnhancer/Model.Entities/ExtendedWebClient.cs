using System;
using System.Collections.Specialized;
using System.Net;

namespace Model.Entities
{
    /// <summary>
    ///     WebClient with Cookie and UserAgent
    /// </summary>
    public class CookieEnabledWebClient : WebClient
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
            get { return @"Mozilla/5.0 (Windows NT 6.3; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0"; }
        }

        /// <summary>
        ///     Get the System.Net.CookieContainer CookieContainer that contains cookies for the current object
        /// </summary>
        public CookieContainer CookieContainer { get; private set; }

        /// <summary>
        ///     Get or Set the System.String UserAgent for the current object
        /// </summary>
        public String UserAgent { get; set; }

        public string DownloadString(String address, string method, NameValueCollection data)
        {
            return UploadValues(address, "POST", data).ToString();
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
    }
}