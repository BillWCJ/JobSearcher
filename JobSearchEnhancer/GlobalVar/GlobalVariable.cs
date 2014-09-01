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
        public const string UserAgent = @"Mozilla/5.0 (Windows NT 6.3; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0";
        public const string UserID = "w52jiang";
        public const string UserPW = "Ss332640747:)";
        public const string LogInUrl = @"https://jobmine.ccol.uwaterloo.ca/psp/SS/?cmd=login&languageCd=ENG&sessionId=";
        public const string JobDetailBaseUrl = @"https://jobmine.ccol.uwaterloo.ca/psc/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBDTLS?UW_CO_JOB_ID=";
        public const string LocationFilePath = @"C:\Users\BillWenChao\Dropbox\Software Projects\JobSearchEnhancer\";
        public const string JobSearchUrl = @"https://jobmine.ccol.uwaterloo.ca/psp/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL?pslnkid=UW_CO_JOBSRCH_LINK&FolderPath=PORTAL_ROOT_OBJECT.UW_CO_JOBSRCH_LINK&IsFolder=false&IgnoreParamTempl=FolderPath%2cIsFolder";
        public const string MenuUrl = @"https://jobmine.ccol.uwaterloo.ca/psp/SS/EMPLOYEE/WORK/h/?tab=DEFAULT";
        public const string JobInquiryUrlShort = @"https://jobmine.ccol.uwaterloo.ca/psp/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL";
        public const string JobInquiryUrl = @"https://jobmine.ccol.uwaterloo.ca/psc/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL?pslnkid=UW_CO_JOBSRCH_LINK&FolderPath=PORTAL_ROOT_OBJECT.UW_CO_JOBSRCH_LINK&IsFolder=false&IgnoreParamTempl=FolderPath%2cIsFolder&PortalActualURL=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsc%2fSS%2fEMPLOYEE%2fWORK%2fc%2fUW_CO_STUDENTS.UW_CO_JOBSRCH.GBL%3fpslnkid%3dUW_CO_JOBSRCH_LINK&PortalContentURL=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsc%2fSS%2fEMPLOYEE%2fWORK%2fc%2fUW_CO_STUDENTS.UW_CO_JOBSRCH.GBL%3fpslnkid%3dUW_CO_JOBSRCH_LINK&PortalContentProvider=WORK&PortalCRefLabel=Job%20Inquiry&PortalRegistryName=EMPLOYEE&PortalServletURI=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsp%2fSS%2f&PortalURI=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsc%2fSS%2f&PortalHostNode=WORK&NoCrumbs=yes&PortalKeyStruct=yes' -H 'Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8' -H 'Accept-Encoding: gzip, deflate' -H 'Accept-Language: en-US,en;q=0.5' -H 'Connection: keep-alive' -H 'Cookie: __utma=; __utmz=; __utma=201735050.883598278.1400078208.1409591528.1409594598.13; __utmz=201735050.1400078208.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); __utma=131842571.381134168.1400209258.1400548276.1409591520.4; __utmz=131842571.1400209258.1.1.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided); __utmc=131842571; cecsprod-80-PORTAL-PSJSESSIONID=QpIyZHkdY6o0gUpsL_L0ykhhdrEKVnz-!1479216573; __utmc=201735050; ExpirePage=https://jobmine.ccol.uwaterloo.ca/psp/SS/; PS_LOGINLIST=https://jobmine.ccol.uwaterloo.ca/SS; PS_TOKENEXPIRE=1_Sep_2014_18:05:21_GMT; SignOnDefault=W52JIANG; __utmb=201735050.2.10.1409594598; PS_TOKEN=AAAApgECAwQAAQAAAAACvAAAAAAAAAAsAARTaGRyAgBOcQgAOAAuADEAMBTtJsjIQ2jvTyWnV8NfrE8EzoeznAAAAGYABVNkYXRhWnicHYpRCkBQEEXPQz5lI15GT/iUJD4kJZ/WYH8W5zLTnFP3DnC7KE5waKLnY85JTcXCTM/KlDJ+yjjUXAzajT3opcQIFHL300STva4WTbmnlSsaueEFk98L2g==; HPTabName=DEFAULT; https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsp%2fss%2femployee%2fwork%2frefresh=list:%20' -H 'Host: jobmine.ccol.uwaterloo.ca' -H 'Referer: https://jobmine.ccol.uwaterloo.ca/psp/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL?pslnkid=UW_CO_JOBSRCH_LINK&FolderPath=PORTAL_ROOT_OBJECT.UW_CO_JOBSRCH_LINK&IsFolder=false&IgnoreParamTempl=FolderPath%2cIsFolder";
    }
}
