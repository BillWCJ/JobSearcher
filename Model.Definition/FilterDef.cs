using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Definition
{
    public class FilterDef
    {
    }

    public enum StringSearchTarget
    {
        [Description("EmployerName")]
        EmployerName,
        [Description("Full Address")]
        FullAddress,
        [Description("Region")]
        Region,
        [Description("Job Title")]
        JobTitle,
        [Description("Number Of Openings")]
        NumberOfOpening,
        [Description("Number Of Applicants Applied")]
        NumberOfApplied,
        [Description("Last Date To Apply")]
        LastDateToApply,
        [Description("Levels")]
        Levels,
        [Description("Disciplines")]
        Disciplines,
        [Description("Comment")]
        Comment,
        [Description("Job Description")]
        JobDescription,
        [Description("All Job Detail")]
        Job,
    }

    public enum FilterCategory
    {
        [Description("String Search")]
        StringSearch,
        [Description("Value Filter")]
        ValueFilter,
        [Description("Category Selection")]
        CategorySelection,
        [Description("Location Filter")]
        LocationFilter,
        [Description("Review Filter (Beta - RateCoopJob.com Data)")]
        ReviewFilter,
        [Description("Custom Filter (Beta)")]
        CustomFilter,
    }
}
