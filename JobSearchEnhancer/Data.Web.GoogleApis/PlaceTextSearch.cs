using System;
using System.Xml;
using GlobalVariable;
using Model.Entities;

namespace Data.Web.GoogleApis
{
    public class PlaceTextSearch
    {
        public const string PlaceTextSearchBaseUrl = @"https://maps.googleapis.com/maps/api/place/textsearch/xml?query=";

        public static string ApiKeyGetHeaderString
        {
            get { return @"&key=" + GVar.Account.GoogleApisBrowserKey; }
        }

        public static Location GetLocation(Employer employer, string region, CookieEnabledWebClient client = null)
        {
            if (client == null)
                client = new CookieEnabledWebClient();

            var xml = new XmlDocument();
            string url = GetPlaceTextSearchUrl(employer, region);
            string result = client.DownloadString(url);

            xml.LoadXml(result);
            if (xml.DocumentElement == null) return null;

            XmlNode response = xml.DocumentElement.ChildNodes[0].ChildNodes[0];
            if (response == null || response.InnerText != "OK") return null;

            XmlNodeList resultList = xml.GetElementsByTagName("result");
            return PickLocation(region, resultList);
        }

        private static Location PickLocation(string region, XmlNodeList resultList)
        {
            if (resultList[0] == null)
                return null;
            XmlNode formattedAddress = resultList[0].SelectSingleNode("descendant::formatted_address");
            XmlNode lat = resultList[0].SelectSingleNode("descendant::lat");
            XmlNode lng = resultList[0].SelectSingleNode("descendant::lng");
            if (formattedAddress != null && lat != null && lng != null)
                return new Location
                {
                    Region = region,
                    FullAddress = formattedAddress.InnerXml,
                    Longitude = Convert.ToDecimal(lng.InnerXml),
                    Latitude = Convert.ToDecimal(lat.InnerXml)
                };
            return null;
        }

        private static string GetPlaceTextSearchUrl(Employer employer, string region)
        {
            string url = PlaceTextSearchBaseUrl + employer.Name + " " +
                         (string.IsNullOrEmpty(employer.UnitName) ? "" : employer.UnitName + " ") + region +
                         ApiKeyGetHeaderString;
            return url;
        }
    }
}