using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentSharp.CoreLib;
using HtmlAgilityPack;
using Model.Entities;
using Model.Entities.RateMyCoopJob;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Data.Web.RateMyCoopJob
{
    public static class RateMyCoopJob
    {
        private static readonly CookieEnabledWebClient Client = new CookieEnabledWebClient();

        public static List<JobRating> GetListofJobs()
        {
            const string url = "http://www.ratemycoopjob.com/jobs";

            var htmlDocument = new HtmlDocument();

            var response = Client.DownloadString(url);
            htmlDocument.LoadHtml(response);

            var body = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[3]/div[3]/table[1]/tbody[1]").ChildNodes;

            var listOfJobs = body.Where(x => x.Name.Equals("tr")).Select(node => new JobRating
            {
                Employer = new EmployerRating
                {
                    EmployerId = (node.ChildNodes[1].ChildNodes[1].ChildNodes[1].Attributes.FirstOrDefault(x => x.Name == "href")
                                .Value.replace("/update_search_count_for_employer/", "")).toInt(), 
                    Name = node.ChildNodes[1].ChildNodes[1].ChildNodes[1].InnerHtml
                },
                Title = node.ChildNodes[3].ChildNodes[1].ChildNodes[1].InnerHtml, 
                JobId = node.ChildNodes[3].ChildNodes[1].ChildNodes[1].Attributes.FirstOrDefault(x => x.Name == "href")
                        .Value.replace("/update_search_count_for_job/", "").toInt(), 
                Location = node.ChildNodes[5].InnerHtml,
                Salary = node.ChildNodes[7].ChildNodes[1].InnerHtml, 
                Rating = (node.ChildNodes[9].ChildNodes[1].InnerHtml).toInt(), 
                Reviews = (node.ChildNodes[11].InnerHtml).toInt()
            }).ToList();

            return listOfJobs;
        }

        public static List<Rating> GetRatings(JobRating job)
        {
            var url = "http://www.ratemycoopjob.com/job/" + "229";

            var htmlDocument = new HtmlDocument();

            var response = Client.DownloadString(url);
            htmlDocument.LoadHtml(response);

            HtmlNodeCollection list;

            using (var sw = new StreamWriter(@"C:\Users\psoladoye\Desktop\GetRatings.html"))
            {
                list = htmlDocument.DocumentNode.SelectNodes("//*[@id=\"job_rating_list\"]");//"/html/body/div[2]/div[2]");
                sw.WriteLine(list[0].InnerHtml);
            }

            //job.Ratings = job.Ratings. ??  new List<Rating>();
            foreach (var row in list)
            {
                var newRating = new Rating();
                Console.WriteLine("Rating "+row.ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].Attributes.FirstOrDefault(x => x.Name == "alt").Value);
                Console.WriteLine("Comment "+row.ChildNodes[1].ChildNodes[1].ChildNodes[3].ChildNodes[0].InnerHtml);
                Console.WriteLine("Date "+row.ChildNodes[1].ChildNodes[1].ChildNodes[3].ChildNodes[1].ChildNodes[0].InnerHtml);



            }
            

            return null;
        }
    }
}