using System;
using System.Collections.Generic;
using System.Xml;
using Model.Entities;
using Model.Entities.JobMine;
using Model.Entities.Web;

namespace Data.Web.GoogleApis
{
    public class PlaceTextSearch : IPlaceTextSearch
    {
        List<string> GoogleApisKeys { get; set; }
        CookieEnabledWebClient Client { get; set; }
        public PlaceTextSearch(List<string> googleApisKeys)
        {
            GoogleApisKeys = googleApisKeys;
            Client = new CookieEnabledWebClient();
        }

        private const string PlaceTextSearchBaseUrlXml = @"https://maps.googleapis.com/maps/api/place/textsearch/xml?query=";
        private const string PlaceTextSearchBaseUrlJson = @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=";

        private string ApiKeyGetHeaderString
        {
            get { return @"&key=" + GoogleApisKeys[0]; }
        }

        public JobLocation GetLocation(Employer employer, string region)
        {
            var xml = new XmlDocument();
            string url = GetPlaceTextSearchUrl(employer, region);
            string result = Client.DownloadString(url);

            xml.LoadXml(result);
            if (xml.DocumentElement == null) return null;

            XmlNode response = xml.DocumentElement.ChildNodes[0].ChildNodes[0];
            if (response == null || response.InnerText != "OK") return null;

            XmlNodeList resultList = xml.GetElementsByTagName("result");
            return PickLocation(region, resultList);
        }

        private static JobLocation PickLocation(string region, XmlNodeList resultList)
        {
            if (resultList[0] != null)
            {
                XmlNode formattedAddress = resultList[0].SelectSingleNode("descendant::formatted_address");
                XmlNode lat = resultList[0].SelectSingleNode("descendant::lat");
                XmlNode lng = resultList[0].SelectSingleNode("descendant::lng");
                if (formattedAddress != null && lat != null && lng != null)
                    return new JobLocation
                    {
                        Region = region,
                        FullAddress = formattedAddress.InnerXml,
                        Longitude = Convert.ToDecimal(lng.InnerXml),
                        Latitude = Convert.ToDecimal(lat.InnerXml)
                    };
            }
            return null;
        }

        private string GetPlaceTextSearchUrl(Employer employer, string region)
        {
            string url = PlaceTextSearchBaseUrlXml + employer.Name + " " +
                         (string.IsNullOrEmpty(employer.UnitName) ? "" : employer.UnitName + " ") + region +
                         ApiKeyGetHeaderString;
            return url;
        }
    }
}