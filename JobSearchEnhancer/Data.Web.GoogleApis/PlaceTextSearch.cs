using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Data.EF.ClusterDB;
using GlobalVariable;
using Model.Entities;

namespace Data.Web.GoogleApis
{
    public class PlaceTextSearch
    {
        public const string PlaceTextSearchBaseUrl = @"https://maps.googleapis.com/maps/api/place/textsearch/xml?query=";
        public const string ApiKeyBase = @"&key=";

        public static void SetLocationIfIncomplete(CookieEnabledWebClient client, Employer employer, Location location)
        {
            if (!string.IsNullOrEmpty(location.FullAddress) || !(location.Longitude == 0 && location.Latitude == 0))
                return;
            string url = PlaceTextSearchBaseUrl + employer.Name + " " +
                         (string.IsNullOrEmpty(employer.UnitName) ? "" : employer.UnitName + " ") + location.Region +
                         ApiKeyBase + GVar.Account.GoogleApisBrowserKey;
            string result = client.DownloadString(url);

            var xml = new XmlDocument();
            xml.LoadXml(result);
            if (xml.DocumentElement != null)
            {
                XmlNode response = xml.DocumentElement.ChildNodes[0].ChildNodes[0];
                XmlNodeList firstReturn = xml.GetElementsByTagName("formatted_address");
                if (response != null)
                    Console.WriteLine(response.InnerText);
                    if (response.InnerText == "OK" && firstReturn[0] != null)
                        location.FullAddress = firstReturn[0].InnerXml;
            }
        }
    }
}
