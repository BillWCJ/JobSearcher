using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalVariable
{
    public class GVar
    {
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
        public static readonly string[] LevelNames = { "Junior", "Intermediate", "Senior", "Bachelor", "Master", "PhD"};
        public const string UserAgent = @"Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
        public const string UserID = "w52jiang";
        public const string UserPW = "Ss332640747:)";
        public const string LogInUrl = @"https://jobmine.ccol.uwaterloo.ca/psp/SS/?cmd=login&languageCd=ENG&sessionId=";
        public const string JobDetailBaseUrl = @"https://jobmine.ccol.uwaterloo.ca/psc/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBDTLS?UW_CO_JOB_ID=";
        public const string LocationFilePath = @"C:\Users\BillWenChao\Dropbox\Software Projects\JobSearchEnhancer";
        public const string JobSearchUrl = @"https://jobmine.ccol.uwaterloo.ca/psp/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL?pslnkid=UW_CO_JOBSRCH_LINK&FolderPath=PORTAL_ROOT_OBJECT.UW_CO_JOBSRCH_LINK&IsFolder=false&IgnoreParamTempl=FolderPath%2cIsFolder";

    }
}
