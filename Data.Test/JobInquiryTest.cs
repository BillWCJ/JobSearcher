using System;
using System.Collections.Specialized;
using System.Net;
using Data.Web.JobMine.DataSource;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Definition;
using Moq;

namespace Data.Test
{
    [TestClass]
    public class JobInquiryTest : JobMineTestBase
    {
        [TestMethod]
        public void GetJobIds_ShouldReturnIds_WhenExecute()
        {
            const string term = "1149";
            const string jobstatus = "POST";
            //CookieContainer.Setup(c => c.Count).Returns(7);
            Client.Setup(c => c.DownloadString(JobInquiryTestData.IframeSrcUrl())).Returns(JobInquiryTestData.InquiryPageDownload_SetInquiryDataResult);
            Client.Setup(c => c.UploadValues(JobMineDef.JobInquiryUrlShortpsc, "POST", It.Is<NameValueCollection>(n =>
                n["ICAction"] == IcAction.Search && n["UW_CO_JOBSRCH_UW_CO_WT_SESSION"] == term && n["ICSID"] == "jFweF6+AOE2mbyLhh9I+0o9f77r6SdV9+DQB/w8/k08=" &&
                n["ICStateNum"] == "1" && n["UW_CO_JOBSRCH_UW_CO_JS_JOBSTATUS"] == "POST"))).Returns(GetBytes(JobInquiryTestData.InquiryPageSearch_GetJobinfoResult()));
            var ids = JobMineRepo.JobInquiry.GetJobIds(term, jobstatus);

        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}