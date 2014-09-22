using System;

namespace GlobalVariable
{
    public partial class GVar
    {
        public static AccountInfo Account { get; set; }
        public enum Level : int { Junior, Intermediate, Senior, Bachelor, Master, PhD };
        public const string SeperationBar = "\n\n-------------------------------------------------------------------------\n";
        public const string UserAgent = @"Mozilla/5.0 (Windows NT 6.3; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0";
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
        public const string TestJobID = "00240986";
        public const string TestJobDetailUrl = JobDetailBaseUrl + TestJobID;

        public static System.Collections.Specialized.NameValueCollection LoginData()
        {
            System.Collections.Specialized.NameValueCollection loginData = new System.Collections.Specialized.NameValueCollection();
            loginData.Add("userid", GVar.Account.User.UserName);
            loginData.Add("pwd", GVar.Account.User.Password);
            loginData.Add("submit", "Submit");
            loginData.Add("timezoneOffset", "240");
            return loginData;
        }
    }

    public partial class GVar
    {
        public struct ICAction
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

        public static readonly string[] LevelNames = { "Junior", "Intermediate", "Senior", "Bachelor", "Master", "PhD" };
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
            
        public static readonly string[] DisciplinesNames =
        {
            "AHS-(unspecified)","AHS-Health Promotion","AHS-Hlth Studies & Gerontology",
            "AHS-Kinesiology","AHS-Rec. & Leisure Studies",@"AHS-Rec./Business Management",
            "ARCH-Architecture","ARTS MASTERS-Economics","ARTS MASTERS-Exp Digital Media",
            "ARTS MASTERS-Literary Studies","ARTS MASTERS-Political Science","ARTS MASTERS-Public Service",
            @"ARTS MASTERS-Rhet/Comm Design","ARTS-(unspecified)","ARTS-Anthropology",
            "ARTS-Arts & Business","ARTS-Digital Arts Comm","ARTS-Economics",
            "ARTS-English","ARTS-English Lit & Rhetoric","ARTS-Financial Management",
            "ARTS-Global Engagement","ARTS-HR Management","ARTS-History",
            "ARTS-International Trade","ARTS-Legal Studies","ARTS-Management Accounting",
            "ARTS-Mathematical Economics","ARTS-Philosophy","ARTS-Political Science",
            "ARTS-Psychology","ARTS-Rhetoric & Prof Writing","ARTS-Sociology",
            "ARTS-Speech Communication","All Business (unspecified)","All Finance (unspecified)",
            "All Health Informatics","All Info Tech (unspecified)","CA-Chart Prof Acct (CPA)",
            "ENG MASTERS-Civil","ENG MASTERS-Management Science","ENG-(unspecified)",
            "ENG-Chemical","ENG-Civil","ENG-Computer",
            "ENG-Electrical","ENG-Environmental","ENG-Geological",
            "ENG-Management","ENG-Mechanical","ENG-Mechatronics",
            "ENG-Nanotechnology","ENG-Software","ENG-Systems Design",
            "ENV- (unspecified)","ENV-Env & Resource Studies","ENV-Environment & Business",
            "ENV-Geog & Env Management","ENV-Geomatics","ENV-International Development",
            "ENV-Knowledge Integration","ENV-Planning","MATH MASTERS-Health Info",
            "MATH- (unspecified)","MATH-Actuarial Science","MATH-Applied Mathematics",
            "MATH-Bioinformatics","MATH-Business Administration","MATH-Combinatorics & Optimizat",
            "MATH-Computational Math","MATH-Computer Science","MATH-Computing & Financial Mgm",
            "MATH-Fin Analysis & Risk Mgmt","MATH-IT Management","MATH-Mathematical Economics",
            "MATH-Mathematical Finance","MATH-Mathematical Optimization","MATH-Mathematical Physics",
            "MATH-Mathematical Studies","MATH-Pure Mathematics","MATH-Scientific Computation",
            "MATH-Statistics","MATH-Statistics for Health","MATH-Teaching",
            "SCI- (unspecified)","SCI-Biochemistry","SCI-Bioinformatics",
            "SCI-Biology",@"SCI-Biotechnology/Economics","SCI-Chemistry",
            "SCI-Earth Sciences","SCI-Environmental Science","SCI-Geology and Hydrogeology",
            "SCI-Optometry","SCI-Pharmacy","SCI-Physics",
            "SCI-Psychology",@"SCI-Science/Business",
        };
    }

    public class AccountInfo
    {
        public System.Net.NetworkCredential User { get; set; }

        public AccountInfo(string username, string password)
        {
            User = new System.Net.NetworkCredential(username, password);
        }
        public AccountInfo()
        {
            System.IO.StreamReader reader = System.IO.StreamReader.Null;
            try
            {
                reader = new System.IO.StreamReader(GVar.UserInfoFile);
                System.String username = reader.ReadLine();
                System.String password = reader.ReadLine();
                User = new System.Net.NetworkCredential(username, password);
                reader.Close();
            }
            catch (System.IO.FileNotFoundException e)
            {
                Console.WriteLine("!Error-{0}_In_{1}: {2}\n", e.GetType().ToString(), "Gvar", e.Message);
                AskForUserInfo();
                StoreUserInfo();
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_{1}: {2}\n", e.GetType().ToString(), "Gvar", e.Message);
            }
            if (reader != null)
            {
                reader.Close();
            }
        }

        private void AskForUserInfo()
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("cmd.exe")
            {
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            try
            {
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(psi);
                System.IO.StreamWriter sw = p.StandardInput;
                System.IO.StreamReader sr = p.StandardOutput;

                sw.WriteLine("Please Enter JobMine UserName:");
                System.String username = sr.ReadLine();
                sw.WriteLine("Please Enter JobMine Password:");
                System.String password = sr.ReadLine();
                User = new System.Net.NetworkCredential(username, password);

                sw.Close();
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_{1}: {2}\n", e.GetType().ToString(), "Gvar" , e.Message);
            }
        }

        private void StoreUserInfo()
        {
            if (User == null)
            {
                User = new System.Net.NetworkCredential("UserName", "Password");
            }
            try
            {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(GVar.UserInfoFile);
                writer.Write("{0}\n{1}", User.UserName, User.Password);
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_{1}: {2}\n", e.GetType().ToString(), "Gvar", e.Message);
            }
        }
    }
}
