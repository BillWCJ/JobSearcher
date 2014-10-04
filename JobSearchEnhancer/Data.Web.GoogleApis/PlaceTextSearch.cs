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

        public static void SetLocationIfIncomplete(Employer employer, Location location,
            CookieEnabledWebClient client = null)
        {
            if (location.AlreadySet)
                return;

            if (client == null)
                client = new CookieEnabledWebClient();

            var xml = new XmlDocument();
            string url = GetPlaceTextSearchUrl(employer, location);
            string result = client.DownloadString(url);
            xml.LoadXml(result);

            if (xml.DocumentElement != null)
            {
                XmlNode response = xml.DocumentElement.ChildNodes[0].ChildNodes[0];
                if (response != null && response.InnerText == "OK")
                {
                    XmlNodeList returnAddresses = xml.GetElementsByTagName("formatted_address");
                    PickAndAssignAddress(location, returnAddresses);
                    Console.WriteLine(response.InnerText);
                }
            }
        }

        private static void PickAndAssignAddress(Location location, XmlNodeList returnAddresses)
        {
            if (returnAddresses[0] != null)
                location.FullAddress = returnAddresses[0].InnerXml;
        }

        private static string GetPlaceTextSearchUrl(Employer employer, Location location)
        {
            string url = PlaceTextSearchBaseUrl + employer.Name + " " +
                         (string.IsNullOrEmpty(employer.UnitName) ? "" : employer.UnitName + " ") + location.Region +
                         ApiKeyGetHeaderString;
            return url;
        }
    }
}