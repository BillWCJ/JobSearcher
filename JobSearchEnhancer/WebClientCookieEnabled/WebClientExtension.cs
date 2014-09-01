using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WebClientExtension 
{
    public class CookieEnabledWebClient : WebClient 
    {
        private CookieContainer cookie = new CookieContainer();

        public CookieContainer getcookie()
        {
            return cookie;
        }

        public bool setcookie (CookieContainer newcookie) 
        {
            cookie = newcookie;
            return true;
        }

        protected override WebRequest GetWebRequest(Uri address) 
        {
            WebRequest request = base.GetWebRequest(address);

            if (request is HttpWebRequest) 
            {
                (request as HttpWebRequest).CookieContainer = cookie;
            }

            return request;
        }
    }
}
