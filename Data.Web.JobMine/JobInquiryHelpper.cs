using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using HtmlAgilityPack;
using Model.Definition;
using Model.Entities;
using Model.Entities.Web;

namespace Data.Web.JobMine
{
    internal static class JobInquiryHelpper
    {
        public static IEnumerable<T> GetJobInquiryPageObject<T>(CookieEnabledWebClient client, string term, string jobStatus, Func<HtmlNode, int, T> objectExtractor)
        {
            int numPages = 0; // numpage is 0 if we haven't search for the number of job that matches the given criteria 
            string iCAction = IcAction.Search, iCsid = "";
            int iCStateNum = 0;
            var objectT = new Queue<T>();

            for (int currentPageNum = 1; currentPageNum <= (numPages != 0 ? numPages : Int32.MaxValue); currentPageNum++)
            {
                var doc = new HtmlDocument();
                SetInquiryData(client, numPages, ref iCAction, ref iCsid, ref iCStateNum);

                string jobinfo = GetJobinfo(client, iCAction, term, iCsid, iCStateNum, jobStatus);
                doc.LoadHtml(jobinfo);

                if (iCAction == IcAction.Search)
                    numPages = GetNumberOfPages(doc);

                foreach (T o in GetCurrentPageObjects(doc, objectExtractor))
                    objectT.Enqueue(o);
            }
            return objectT;
        }

        private static IEnumerable<T> GetCurrentPageObjects<T>(HtmlDocument doc, Func<HtmlNode, int, T> objectExtractor)
        {
            HtmlNode thisTableNode = GetTableNode(doc);
            for (int childIndex = JobMineDef.JobInquiryFirstRowIndex, count = 0; childIndex < thisTableNode.ChildNodes.Count; childIndex++)
            {
                HtmlNode row = thisTableNode.ChildNodes[childIndex];
                if (row.Name == "tr")
                {
                    T objectT = objectExtractor(row, count++);
                    count++;
                    if (objectT != null)
                        yield return objectT;
                }
            }
        }

        private static void SetInquiryData(CookieEnabledWebClient client, int numPages, ref string iCAction,
            ref string iCsid, ref int iCStateNum)
        {
            var doc = new HtmlDocument();
            if (iCAction != IcAction.Down && numPages != JobMineDef.JobInquiryFirstSearch)
                iCAction = IcAction.Down;

            if (iCAction == IcAction.Search)
            {
                string downloadString = client.DownloadString(JobMineDef.InquiryResultTableIframeUrl);
                doc.LoadHtml(downloadString);
                iCsid = GetIcsid(doc);
                iCStateNum = GetIcStateNum(doc);
            }
            iCStateNum++;
        }

        private static string GetJobinfo(CookieEnabledWebClient client, string iCAction, string term, string iCsid,
            int iCStateNum, string jobStatus)
        {
            const string url = JobMineDef.JobInquiryUrlShortpsc, method = "POST";
            NameValueCollection postData = PostData.GetJobInquiryData(iCStateNum.ToString(CultureInfo.InvariantCulture), iCAction, iCsid, term, jobStatus);
            return Encoding.UTF8.GetString(client.UploadValues(url, method, postData));
        }

        private static int GetNumberOfPages(HtmlDocument doc)
        {
            string currentJobsDisplayString =
                doc.DocumentNode.SelectNodes("//span[@class='PSGRIDCOUNTER']")[1].InnerHtml;
            //var currentJobsDisplayString = doc.DocumentNode.SelectSingleNode("/page[1]/field[1]/tr[29]/td[2]/div[1]/table[1]/tr[2]/td[1]/table[1]/tr[2]/td[1]/div[1]/span[2]").InnerHtml;
            const string seperator = "of ";
            int numberOfJobs =
                Convert.ToInt32(
                    currentJobsDisplayString.Substring(
                        currentJobsDisplayString.IndexOf(seperator, StringComparison.Ordinal) + seperator.Length));


            return numberOfJobs/25 + ((numberOfJobs%25 == 0) ? 0 : 1);
        }


        private static HtmlNode GetTableNode(HtmlDocument doc)
        {
            return
                doc.DocumentNode.SelectSingleNode(
                    "/page[1]/field[1]/tr[29]/td[2]/div[1]/table[1]/tr[2]/td[1]/table[1]/tr[1]/td[1]/table[1]");
        }

        /// <summary>
        ///     Get the ICStateNum of the page in Html
        /// </summary>
        /// <remarks>Safe Extraction: DocumentNode.SelectSingleNode("//input[@id='ICStateNum']").Attributes["value"].Value;</remarks>
        private static int GetIcStateNum(HtmlDocument doc)
        {
            string icStateNumString = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/input[3]").Attributes["value"].Value;
            return Convert.ToInt32(icStateNumString);
        }

        /// <summary>
        ///     Get the ICSID of the page in Html
        /// </summary>
        /// <remarks>Safe Extraction: DocumentNode.SelectSingleNode("//input[@id='ICSID']").Attributes["value"].Value;</remarks>
        private static string GetIcsid(HtmlDocument doc) { return doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/input[13]").Attributes["value"].Value; }
    }
}