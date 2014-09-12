using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalVariable
{
    public partial class GVar
    {
        public enum Level : int { Junior, Intermediate, Senior, Bachelor, Master, PhD };
        public const string SeperationBar = "\n\n-------------------------------------------------------------------------\n";
        public const string UserAgent = @"Mozilla/5.0 (Windows NT 6.3; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0";
        public const string UserID = "w52jiang";
        public const string UserPW = "";
        public const string LocationFilePath = @"C:\Users\BillWenChao\Dropbox\Software Projects\JobSearchEnhancer\";
        public const string JobMineBaseUrl =   @"https://jobmine.ccol.uwaterloo.ca/";
        public const string LogInUrl = JobMineBaseUrl + @"psp/SS/?cmd=login&languageCd=ENG&sessionId=";
        public const string JobDetailBaseUrl = JobMineBaseUrl + @"psc/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBDTLS?UW_CO_JOB_ID=";
        public const string MenuUrl = JobMineBaseUrl + @"psp/SS/EMPLOYEE/WORK/h/?tab=DEFAULT";
        public const string JobInquiryUrlShort = JobMineBaseUrl + @"psp/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL";
        public const string JobInquiryUrlpsc = JobMineBaseUrl + @"psc/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL?pslnkid=UW_CO_JOBSRCH_LINK&FolderPath=PORTAL_ROOT_OBJECT.UW_CO_JOBSRCH_LINK&IsFolder=false&IgnoreParamTempl=FolderPath%2cIsFolder";
        public const string StudentProfileUrl = JobMineBaseUrl + @"psp/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_STUDENT.GBL?pslnkid=UW_CO_STUDENT_LINK&FolderPath=PORTAL_ROOT_OBJECT.UW_CO_STUDENT_LINK&IsFolder=false&IgnoreParamTempl=FolderPath%2cIsFolder";
        public const string TestJobID = "00240986";
        public const string TestJobDetailUrl = JobDetailBaseUrl + TestJobID;
    }

    public partial class GVar
    {
        public static readonly string[] LevelNames = { "Junior", "Intermediate", "Senior", "Bachelor", "Master", "PhD" };
        public static readonly string[] JobDetailPageFieldNames = 
            { "Employer"
            , "Job Title"
            , "Location"
            , "Disciplines"
            , "Levels"
            , "Comment"
            , "Description" 
            };

        public static readonly string[] JobDetailPageFieldNameTitles = 
            { "Employer:\n"
            , "Job Title:\n"
            , "Location:\n"
            , "Disciplines:\n"
            , "Levels:\n"
            , "Comment:\n"
            , "Description:\n" 
            , "UrlLink:\n" 
            };

        public static readonly string[] FieldSearchString = 
            {"id='UW_CO_JOBDTL_DW_UW_CO_EMPUNITDIV'>",
            "id='UW_CO_JOBDTL_VW_UW_CO_JOB_TITLE'>",
            "id='UW_CO_JOBDTL_VW_UW_CO_WORK_LOCATN'>",
            "id='UW_CO_JOBDTL_DW_UW_CO_DESCR'>",
            "id='UW_CO_JOBDTL_DW_UW_CO_DESCR_100'>",
            "id='UW_CO_JOBDTL_VW_UW_CO_JOB_SUMMARY'>",
            "id='UW_CO_JOBDTL_VW_UW_CO_JOB_DESCR'>"
            };
            
        public static readonly string[] DisciplinesNames =
            {"AHS-(unspecified)","AHS-Health Promotion","AHS-Hlth Studies & Gerontology"
            ,"AHS-Kinesiology","AHS-Rec. & Leisure Studies",@"AHS-Rec./Business Management"
            ,"ARCH-Architecture","ARTS MASTERS-Economics","ARTS MASTERS-Exp Digital Media"
            ,"ARTS MASTERS-Literary Studies","ARTS MASTERS-Political Science","ARTS MASTERS-Public Service"
            ,@"ARTS MASTERS-Rhet/Comm Design","ARTS-(unspecified)","ARTS-Anthropology"
            ,"ARTS-Arts & Business","ARTS-Digital Arts Comm","ARTS-Economics"
            ,"ARTS-English","ARTS-English Lit & Rhetoric","ARTS-Financial Management"
            ,"ARTS-Global Engagement","ARTS-HR Management","ARTS-History"
            ,"ARTS-International Trade","ARTS-Legal Studies","ARTS-Management Accounting"
            ,"ARTS-Mathematical Economics","ARTS-Philosophy","ARTS-Political Science"
            ,"ARTS-Psychology","ARTS-Rhetoric & Prof Writing","ARTS-Sociology"
            ,"ARTS-Speech Communication","All Business (unspecified)","All Finance (unspecified)"
            ,"All Health Informatics","All Info Tech (unspecified)","CA-Chart Prof Acct (CPA)"
            ,"ENG MASTERS-Civil","ENG MASTERS-Management Science","ENG-(unspecified)"
            ,"ENG-Chemical","ENG-Civil","ENG-Computer"
            ,"ENG-Electrical","ENG-Environmental","ENG-Geological"
            ,"ENG-Management","ENG-Mechanical","ENG-Mechatronics"
            ,"ENG-Nanotechnology","ENG-Software","ENG-Systems Design"
            ,"ENV- (unspecified)","ENV-Env & Resource Studies","ENV-Environment & Business"
            ,"ENV-Geog & Env Management","ENV-Geomatics","ENV-International Development"
            ,"ENV-Knowledge Integration","ENV-Planning","MATH MASTERS-Health Info"
            ,"MATH- (unspecified)","MATH-Actuarial Science","MATH-Applied Mathematics"
            ,"MATH-Bioinformatics","MATH-Business Administration","MATH-Combinatorics & Optimizat"
            ,"MATH-Computational Math","MATH-Computer Science","MATH-Computing & Financial Mgm"
            ,"MATH-Fin Analysis & Risk Mgmt","MATH-IT Management","MATH-Mathematical Economics"
            ,"MATH-Mathematical Finance","MATH-Mathematical Optimization","MATH-Mathematical Physics"
            ,"MATH-Mathematical Studies","MATH-Pure Mathematics","MATH-Scientific Computation"
            ,"MATH-Statistics","MATH-Statistics for Health","MATH-Teaching"
            ,"SCI- (unspecified)","SCI-Biochemistry","SCI-Bioinformatics"
            ,"SCI-Biology",@"SCI-Biotechnology/Economics","SCI-Chemistry"
            ,"SCI-Earth Sciences","SCI-Environmental Science","SCI-Geology and Hydrogeology"
            ,"SCI-Optometry","SCI-Pharmacy","SCI-Physics"
            ,"SCI-Psychology",@"SCI-Science/Business"
            };
    }
}
