using System;

namespace GlobalVariable
{
    public class UserAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string GoogleApisServerKey { get; set; }
        public string GoogleApisBrowserKey { get; set; }
    }
    public partial class GVar
    {
        public static UserAccount Account { get; set; }
        public const string SeperationBar = "\n\n-------------------------------------------------------------------------\n";
        public const string MasterFilePath = @"C:\Users\BillWenChao\Dropbox\Software Projects\";
        public const string FilePath =  MasterFilePath + @"JobSearchEnhancer\";
        public const string UserInfoFile = MasterFilePath + @"JobMineLogIn.txt";
        public const string JobMineBaseUrl =   @"https://jobmine.ccol.uwaterloo.ca/";
        public const string LogInUrl = JobMineBaseUrl + @"psp/SS/?cmd=login&languageCd=ENG&sessionId=";
        public const string JobDetailBaseUrl = JobMineBaseUrl + @"psc/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBDTLS?UW_CO_JOB_ID=";
        public const string MenuUrl = JobMineBaseUrl + @"psp/SS/EMPLOYEE/WORK/h/?tab=DEFAULT";
        public const string JobInquiryUrlShortpsp = JobMineBaseUrl + @"psp/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL";
        public const string JobInquiryUrlShortpsc = JobMineBaseUrl + @"psc/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL";
        public const string JobInquiryUrlpsp = JobMineBaseUrl + @"psp/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL?pslnkid=UW_CO_JOBSRCH_LINK&FolderPath=PORTAL_ROOT_OBJECT.UW_CO_JOBSRCH_LINK&IsFolder=false&IgnoreParamTempl=FolderPath%2cIsFolder";
        public const string JobInquiryUrlpsc = JobMineBaseUrl + @"psc/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL?pslnkid=UW_CO_JOBSRCH_LINK&FolderPath=PORTAL_ROOT_OBJECT.UW_CO_JOBSRCH_LINK&IsFolder=false&IgnoreParamTempl=FolderPath%2cIsFolder";
        public const string StudentProfileUrl = JobMineBaseUrl + @"psp/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_STUDENT.GBL?pslnkid=UW_CO_STUDENT_LINK&FolderPath=PORTAL_ROOT_OBJECT.UW_CO_STUDENT_LINK&IsFolder=false&IgnoreParamTempl=FolderPath%2cIsFolder";
        public const string TestJobId = "00240986";
        public const string TestJobDetailUrl = JobDetailBaseUrl + TestJobId;
    }

    public partial class GVar
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
