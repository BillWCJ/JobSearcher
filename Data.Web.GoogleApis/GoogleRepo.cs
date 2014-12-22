using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contract.GoogleApis;
using Data.Contract.GoogleApis.Interface;
using Model.Entities;

namespace Data.Web.GoogleApis
{
    public class GoogleRepo : IGoogleRepo
    {
        public GoogleRepo(List<string> googleApisKeys)
        {
            LocationRepo = new PlaceTextSearch(googleApisKeys);
        }

        public IPlaceTextSearch LocationRepo { get; set; }
    }
}
