namespace Model.Definition
{
    public struct IcAction
    {
        public const string Search = "UW_CO_JOBSRCHDW_UW_CO_DW_SRCHBTN";
        public const string Top = "UW_CO_JOBRES_VW$htop$0";
        public const string Up = "UW_CO_JOBRES_VW$hup$0";
        public const string Down = "UW_CO_JOBRES_VW$hdown$0";
        public const string End = "UW_CO_JOBRES_VW$hend$0";
        public const string ViewAll = "UW_CO_JOBRES_VW$hviewall$0";
    }

    public struct JobStatus
    {
        public const string Approved = "APPR";
        public const string AppsAvail = "APPA";
        public const string Cancelled = "CANC";
        public const string Posted = "POST";
    }

    public struct ColumnPath
    {
        public const string JobId = "//span[@id='UW_CO_JOBRES_VW_UW_CO_JOB_ID$";
        public const string JobTitle = "//span[@id='UW_CO_JOBTITLE_HL$";
        public const string EmployerName = "//span[@id='UW_CO_JOBRES_VW_UW_CO_PARENT_NAME$";
        public const string Region = "//span[@id='UW_CO_JOBRES_VW_UW_CO_WORK_LOCATN$";
        public const string UnitName = "//span[@id='UW_CO_JOBRES_VW_UW_CO_EMPLYR_NAME1$";
    }

    public class JobMineDef
    {
        /// <summary>
        ///     Because each real row are accompany by a row of empty text and first row is the title row, this is why the first
        ///     row index starts at 3
        /// </summary>
        public const int JobInquiryFirstRowIndex = 3;

        /// <summary>
        ///     Constant value that indicate it is the first time searching the job inquiry page, which means some extra operation
        ///     needs to be completed
        /// </summary>
        public const int JobInquiryFirstSearch = -1;

        public const string JobInquiryUrlShortpsp = CommonDef.JobMineBaseUrl + @"psp/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL";
        public const string JobInquiryUrlShortpsc = CommonDef.JobMineBaseUrl + @"psc/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL";
        public const string JobInquiryUrlpsp = CommonDef.JobMineBaseUrl + @"psp/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL?pslnkid=UW_CO_JOBSRCH_LINK&FolderPath=PORTAL_ROOT_OBJECT.UW_CO_JOBSRCH_LINK&IsFolder=false&IgnoreParamTempl=FolderPath%2cIsFolder";
        public const string JobInquiryUrlpsc = CommonDef.JobMineBaseUrl + @"psc/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL?pslnkid=UW_CO_JOBSRCH_LINK&FolderPath=PORTAL_ROOT_OBJECT.UW_CO_JOBSRCH_LINK&IsFolder=false&IgnoreParamTempl=FolderPath%2cIsFolder";
        public const string LogInUrl = CommonDef.JobMineBaseUrl + @"psp/SS/?cmd=login&languageCd=ENG&sessionId=";
        public const string JobDetailBaseUrl = CommonDef.JobMineBaseUrl + @"psc/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBDTLS?UW_CO_JOB_ID=";
        public const string MenuUrl = CommonDef.JobMineBaseUrl + @"psp/SS/EMPLOYEE/WORK/h/?tab=DEFAULT";
        public const string StudentProfileUrl = CommonDef.JobMineBaseUrl + @"psp/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_STUDENT.GBL?pslnkid=UW_CO_STUDENT_LINK&FolderPath=PORTAL_ROOT_OBJECT.UW_CO_STUDENT_LINK&IsFolder=false&IgnoreParamTempl=FolderPath%2cIsFolder";
        public const string TestJobId = "00240986";
        public const string TestJobDetailUrl = JobDetailBaseUrl + TestJobId;

        /// <summary>
        ///     Safe Extraction: var doc = new HtmlDocument();
        ///     string inquirypagehtml = client.DownloadString(JobMineDef.JobInquiryUrlpsp);
        ///     doc.LoadHtml(inquirypagehtml);
        ///     string src = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[3]/div[1]/iframe[1]").Attributes["src"].Value;
        ///     src = doc.DocumentNode.SelectSingleNode("//iframe[@id='ptifrmtgtframe']").Attributes["src"].Value;
        /// </summary>
        public const string InquiryResultTableIframeUrl = @"https://jobmine.ccol.uwaterloo.ca/psc/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL?pslnkid=UW_CO_JOBSRCH_LINK&amp;FolderPath=PORTAL_ROOT_OBJECT.UW_CO_JOBSRCH_LINK&amp;IsFolder=false&amp;IgnoreParamTempl=FolderPath%2cIsFolder&amp;PortalActualURL=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsc%2fSS%2fEMPLOYEE%2fWORK%2fc%2fUW_CO_STUDENTS.UW_CO_JOBSRCH.GBL%3fpslnkid%3dUW_CO_JOBSRCH_LINK&amp;PortalContentURL=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsc%2fSS%2fEMPLOYEE%2fWORK%2fc%2fUW_CO_STUDENTS.UW_CO_JOBSRCH.GBL%3fpslnkid%3dUW_CO_JOBSRCH_LINK&amp;PortalContentProvider=WORK&amp;PortalCRefLabel=Job%20Inquiry&amp;PortalRegistryName=EMPLOYEE&amp;PortalServletURI=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsp%2fSS%2f&amp;PortalURI=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsc%2fSS%2f&amp;PortalHostNode=WORK&amp;NoCrumbs=yes&amp;PortalKeyStruct=yes";

        public static readonly string[] JobDetailPageFieldNames =
        {
            "Employer",
            "Job Title",
            "Location",
            "Disciplines",
            "Levels",
            "Comment",
            "Description"
        };

        public static readonly string[] JobDetailPageFieldNameTitles =
        {
            "Employer:\n",
            "Job Title:\n",
            "Location:\n",
            "Disciplines:\n",
            "Levels:\n",
            "Comment:\n",
            "Description:\n",
            "UrlLink:\n"
        };

        public static readonly string[] FieldSearchString =
        {
            "id='UW_CO_JOBDTL_DW_UW_CO_EMPUNITDIV'>",
            "id='UW_CO_JOBDTL_VW_UW_CO_JOB_TITLE'>",
            "id='UW_CO_JOBDTL_VW_UW_CO_WORK_LOCATN'>",
            "id='UW_CO_JOBDTL_DW_UW_CO_DESCR'>",
            "id='UW_CO_JOBDTL_DW_UW_CO_DESCR_100'>",
            "id='UW_CO_JOBDTL_VW_UW_CO_JOB_SUMMARY'>",
            "id='UW_CO_JOBDTL_VW_UW_CO_JOB_DESCR'>"
        };
    }
}