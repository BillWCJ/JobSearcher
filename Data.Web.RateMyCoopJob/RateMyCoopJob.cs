using System.Collections.Generic;
using System.Linq;
using FluentSharp.CoreLib;
using HtmlAgilityPack;
using Model.Entities;
using Model.Entities.RateMyCoopJob;

namespace Data.Web.RateMyCoopJob
{
    public static class RateMyCoopJob
    {
        private static readonly CookieEnabledWebClient Client = new CookieEnabledWebClient();

        public static IEnumerable<JobReview> GetListofJobs()
        {
            const string url = "http://www.ratemycoopjob.com/jobs";

            var htmlDocument = new HtmlDocument();

            string response = Client.DownloadString(url);
            htmlDocument.LoadHtml(response);

            HtmlNodeCollection body = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[3]/div[3]/table[1]/tbody[1]").ChildNodes;

            List<JobReview> listOfJobs = body.Where(x => x.Name.Equals("tr")).Select(node => new JobReview
            {
                Employer = new EmployerReview
                {
                    EmployerId = (node.ChildNodes[1].ChildNodes[1].ChildNodes[1].Attributes.FirstOrDefault(x => x.Name == "href")
                        .Value.replace("/update_search_count_for_employer/", "")).toInt(),
                    Name = node.ChildNodes[1].ChildNodes[1].ChildNodes[1].InnerHtml
                },
                Title = node.ChildNodes[3].ChildNodes[1].ChildNodes[1].InnerHtml,
                JobReviewId = node.ChildNodes[3].ChildNodes[1].ChildNodes[1].Attributes.FirstOrDefault(x => x.Name == "href")
                    .Value.replace("/update_search_count_for_job/", "").toInt(),
                Location = node.ChildNodes[5].InnerHtml,
                AverageSalary = node.ChildNodes[7].ChildNodes[1].InnerHtml,
                AverageRating = (node.ChildNodes[9].ChildNodes[1].InnerHtml).toInt(),
                NumberOfReviews = (node.ChildNodes[11].InnerHtml).toInt()
            }).ToList();

            return listOfJobs;
        }

        public static void PopulateRatingsField(JobReview job)
        {
            string url = "http://www.ratemycoopjob.com/job/" + job.JobReviewId;

            var htmlDocument = new HtmlDocument();

            string response = Client.DownloadString(url);
            htmlDocument.LoadHtml(response);

            HtmlNodeCollection body = htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"job_rating_list\"]").ChildNodes;


            IEnumerable<HtmlNode> ratingsList = body.Where(x => x.Name.Equals("div"));

            foreach (HtmlNode row in ratingsList)
            {
                HtmlAttribute firstOrDefault = row.ChildNodes[1].ChildNodes[1].ChildNodes[1].Attributes
                    .FirstOrDefault(x => x.Name == "alt");

                if (firstOrDefault != null)
                {
                    job.JobRatings.Add(new JobRating
                    {
                        Rating = firstOrDefault.Value.Replace("_stars", "").toDouble(),
                        Comment = row.ChildNodes[1].ChildNodes[3].ChildNodes[0].InnerHtml.Trim(),
                        Date =
                            row.ChildNodes[1].ChildNodes[3].ChildNodes[1].ChildNodes[0].InnerHtml.Trim(),
                        Salary = row.ChildNodes[1].ChildNodes[5].ChildNodes[3].InnerHtml.Trim()
                    });
                }
            }
        }
    }
}