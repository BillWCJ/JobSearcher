using System.Collections.Specialized;
using Model.Definition;

namespace Data.Web.JobMine.Common
{
    public static class PostData
    {
        /// <summary>
        ///     Get the JobMine login data collection
        /// </summary>
        public static NameValueCollection GetLoginData(string userName, string password)
        {
            return new NameValueCollection
            {
                {"userid", userName},
                {"pwd", password},
                {"submit", "Submit"}
            };
        }

        /// <summary>
        ///     Return the JobInquiryData Navevaluecollection for post operation to JobMine JobInquiryPage
        /// </summary>
        public static NameValueCollection GetJobInquiryData(string iCStateNum, string iCAction, string iCsid, string term, string jobStatus = JobStatus.Posted, string jobTitle = null, string employerName = null, string location = null, string discipline1 = null, string discipline2 = null, string discipline3 = null)
        {
            var searchData = new NameValueCollection
            {
                {"ICAJAX", "1"},
                {"ICNAVTYPEDROPDOWN", "1"},
                {"ICType", "Panel"},
                {"ICElementNum", "0"},
                {"ICStateNum", iCStateNum},
                {"ICAction", iCAction},
                {"ICXPos", "0"},
                {"ICYPos", "110"},
                {"ResponsetoDiffFrame", "-1"},
                {"TargetFrameName", "None"},
                {"ICFocus", ""},
                {"ICSaveWarningFilter", "0"},
                {"ICChanged", "-1"},
                {"ICResubmit", "0"},
                {"ICSID", iCsid},
                {"ICModalWidget", "0"},
                {"ICZoomGrid", "0"},
                {"ICZoomGridRt", "0"},
                {"ICModalLongClosed", ""},
                {"ICActionPrompt", "false"},
                {"ICFind", ""},
                {"ICAddCount", ""},
                {"UW_CO_JOBSRCH_UW_CO_WT_SESSION", term},
                {"UW_CO_JOBSRCH_UW_CO_JOB_TITLE", jobTitle},
                {"UW_CO_JOBSRCH_UW_CO_EMPLYR_NAME", employerName},
                {"UW_CO_JOBSRCH_UW_CO_LOCATION", location},
                {"UW_CO_JOBSRCH_UW_CO_ADV_DISCP1", discipline1},
                {"UW_CO_JOBSRCH_UW_CO_ADV_DISCP2", discipline2},
                {"UW_CO_JOBSRCH_UW_CO_ADV_DISCP3", discipline3},
                {"UW_CO_JOBSRCH_UW_CO_JS_JOBSTATUS", jobStatus}
            };
            return searchData;
        }
    
        /// <summary>
        ///     Return the JobInquiryData Navevaluecollection for post operation to JobMine JobInquiryPage
        /// </summary>
        public static NameValueCollection GetAddJobToShortListData(string iCStateNum, string iCAction, string iCsid, string jobTitle = null, string employerName = null)
        {
            var data = new NameValueCollection
            {
                {"ICAJAX", "1"},
                {"ICNAVTYPEDROPDOWN", "0"},
                {"ICType", "Panel"},
                {"ICElementNum", "0"},
                {"ICStateNum", iCStateNum},
                {"ICAction", iCAction},
                {"ICXPos", "0"},
                {"ICYPos", "228"},
                {"ResponsetoDiffFrame", "-1"},
                {"TargetFrameName", "None"},
                {"ICFocus", ""},
                {"ICSaveWarningFilter", "0"},
                {"ICChanged", "-1"},
                {"ICResubmit", "0"},
                {"ICSID", iCsid},
                {"ICModalWidget", "0"},
                {"ICZoomGrid", "0"},
                {"ICZoomGridRt", "0"},
                {"ICModalLongClosed", ""},
                {"ICActionPrompt", "true"},
                {"ICFind", ""},
                {"ICAddCount", ""},
                {"TYPE_COOP", "1"},
                {"UW_CO_JOBSRCH_UW_CO_JOB_TITLE", jobTitle},
                {"UW_CO_JOBSRCH_UW_CO_EMPLYR_NAME", employerName},
                {"UW_CO_JOBSRCH_UW_CO_LOCATION", ""}
            };
            return data;
        }
    }
}