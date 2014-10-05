﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class JobMineDef
    {
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
