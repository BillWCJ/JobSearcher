namespace WebClientExtension
{
    public class CookieEnabledWebClient : System.Net.WebClient
    {

        public string DefaultUserAgent { get { return @"Mozilla/5.0 (Windows NT 6.3; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0"; } } 
        /// <summary>
        /// Get the System.Net.CookieContainer CookieContainer that contains cookies for the current object
        /// </summary>
        public System.Net.CookieContainer CookieContainer { get; private set; }

        /// <summary>
        /// Get or Set the System.String UserAgent for the current object
        /// </summary>
        public System.String UserAgent { get; set; }

        /// <summary>
        /// Initalizes a new instatnce of WebClientExtension.CookieEnabledWebClient
        /// </summary>
        public CookieEnabledWebClient()
        {
            CookieContainer = new System.Net.CookieContainer();
            UserAgent = DefaultUserAgent;
        }

        public string DownloadString(System.String address, string method, System.Collections.Specialized.NameValueCollection data)
        {
            return this.UploadValues(address, "POST", data).ToString();
        }

        /// <summary>
        /// Returns a System.Net.WebRequest object for the specified resource.
        /// </summary>
        /// <param name="address">A System.Uri that identifies the resource to request.</param>
        /// <returns>A new System.Net.WebRequest object for the specified resource.</returns>
        protected override System.Net.WebRequest GetWebRequest(System.Uri address)
        {
            System.Net.WebRequest request = base.GetWebRequest(address);
            if (request is System.Net.HttpWebRequest)
            {
                (request as System.Net.HttpWebRequest).CookieContainer = this.CookieContainer;
                (request as System.Net.HttpWebRequest).UserAgent = this.UserAgent;
            }
            return request;
        }
    }
}
