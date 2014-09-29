using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Model.Entities;
using WebClientExtension;
using System.Diagnostics;

namespace WebClientExtension.Test 
{
    [TestClass]
    public class WebClientCookieEnabledTest
    {
        CookieEnabledWebClient webclient = new CookieEnabledWebClient();
        CookieEnabledWebClient baseclient = new CookieEnabledWebClient();

        [TestMethod]
        public void WebClientCookieEnabled_ShouldRun_WhenExecute() 
        {
            webclient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            webclient.DownloadData("https://www.google.com");
            Trace.WriteLine(webclient.CookieContainer.Count);
            Trace.WriteLine(baseclient.CookieContainer.Count);
            Assert.IsTrue(webclient.CookieContainer.Count != baseclient.CookieContainer.Count, "Same Number of Cookies");
        }
    }
}
