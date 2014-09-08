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
        private System.Net.CookieContainer CookieContainer { get; set; }
        private System.String UserAgent { get; set; }

        public CookieEnabledWebClient()
        {
            CookieContainer = new System.Net.CookieContainer();
            UserAgent = GVar.UserAgent;
            if ( string.IsNullOrEmpty(UserAgent) || string.IsNullOrWhiteSpace(UserAgent) )
            {
                UserAgent = @"Mozilla/5.0 (Windows NT 6.3; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0"; //Default User Agent
            }
        }
        protected override System.Net.WebRequest GetWebRequest(Uri address)
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
