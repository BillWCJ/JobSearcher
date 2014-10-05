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
    public class GVar
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
}
