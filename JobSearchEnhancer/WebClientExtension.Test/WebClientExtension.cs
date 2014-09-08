﻿using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Specialized;
using GlobalVariable;

namespace WebClientExtension
{
    public class CookieEnabledWebClient : WebClient
    {
        private System.Net.CookieContainer cookiecontainer;
        private System.String userAgent;
        public System.Net.CookieContainer CookieContainer
        {
            get { return cookiecontainer; }
            set { cookiecontainer = value; }
        }

        public System.String UserAgent
        {
            get { return userAgent; }
            set { userAgent = value; }
        }

        public CookieEnabledWebClient()
        {
            userAgent = GVar.UserAgent;
            cookiecontainer = new System.Net.CookieContainer();
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);

            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = CookieContainer;
                (request as HttpWebRequest).UserAgent = userAgent;
            }

            return request;
        }


    }
}
