using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Model.Definition;
using Model.Entities;
using Model.Entities.JobMine;

namespace Data.Web.JobMine
{
    public class JobInquiry
    {
        static CookieEnabledWebClient Client { get; set; }
        public JobInquiry(CookieEnabledWebClient client)
        {
            Client = client;
        }
        /// <summary>
        ///     Get all the JobOverViews from inquiry page
        /// </summary>
        public IEnumerable<JobOverView> GetJobOverViews(string term, string jobStatus) { return JobInquiryHelpper.GetJobInquiryPageObject(Client, term, jobStatus, GetJobOverView); }

        public IEnumerable<string> GetJobIds(string term, string jobStatus) { return JobInquiryHelpper.GetJobInquiryPageObject(Client, term, jobStatus, GetJobId); }

        private static JobOverView GetJobOverView(HtmlNode row, int count)
        {
            string jobId = GetConvertedNodeInnerHtml(row, ColumnPath.JobId, count);
            var jobOverView = new JobOverView
            {
                JobTitle = " ", //GetConvertedNodeInnerHtml(row, ColumnPath.JobTitle, count),
                Employer = new Employer
                {
                    Name = GetConvertedNodeInnerHtml(row, ColumnPath.EmployerName, count),
                    UnitName = GetConvertedNodeInnerHtml(row, ColumnPath.UnitName, count)
                },
                Location = new Location
                {
                    Region = GetConvertedNodeInnerHtml(row, ColumnPath.Region, count)
                },
                Id = Convert.ToInt32(jobId),
            };
            if (Utility.IsCorrectJobId(jobId) && Utility.IsJobOverViewCompleted(jobOverView))
                return jobOverView;
            return null;
        }

        private static string GetJobId(HtmlNode row, int count) { return GetConvertedNodeInnerHtml(row, ColumnPath.JobId, count); }

        private static string GetConvertedNodeInnerHtml(HtmlNode row, string path, int count)
        {
            return
                row.SelectSingleNode(path + count + "']")
                    .InnerHtml.Replace("&nbsp;", " ")
                    .Replace("<br />", "\n")
                    .Replace("&amp;", "&");
        }
    }
}