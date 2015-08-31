using System;
using System.Collections.Generic;
using System.Linq;
using Common.Utility;
using JobBrowserModule.ViewModels;
using Model.Definition;
using Model.Entities.PostingFilter;

namespace JobBrowserModule.Services
{
    public static class FilterHelper
    {
        public static bool IsPostingVisible(JobPostingViewModel jobPosting, IEnumerable<Filter> filters)
        {
            foreach (Filter filter in filters)
            {
                Func<JobPostingViewModel, Filter, bool> filterOperation = null;

                switch (filter.Category)
                {
                    case FilterCategory.StringSearch:
                        filterOperation = StringSearchOperation;
                        break;
                    case FilterCategory.DisciplineSelection:
                        filterOperation = DisciplineSelectionOperation;
                        break;
                    case FilterCategory.LevelSelection:
                        filterOperation = LevelSelectionOperation;
                        break;
                    case FilterCategory.ValueFilter:
                        filterOperation = ValueFilterOperation;
                        break;
                    case FilterCategory.LocationFilter:
                        filterOperation = LocationFilterOperation;
                        break;
                    case FilterCategory.ReviewFilter:
                        filterOperation = ReviewFilterOperation;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Does not Support Filter Category: " + filter.Category);
                }

                if (!filterOperation(jobPosting, filter))
                    return false;
            }
            return true;
        }

        private static bool DisciplineSelectionOperation(JobPostingViewModel jobPosting, Filter filter)
        {

            bool isRightDiscipline = true;
            if (filter.DisciplinesSearchTarget.Any())
            {
                isRightDiscipline = filter.DisciplinesSearchTarget.Any(discipline => jobPosting.Job.Disciplines.ContainDiscipline(discipline));
            }
            return isRightDiscipline;
        }

        private static bool ReviewFilterOperation(JobPostingViewModel jobPosting, Filter filter)
        {
            return true;
        }

        private static bool LocationFilterOperation(JobPostingViewModel jobPosting, Filter filter)
        {
            return true;
        }

        private static bool LevelSelectionOperation(JobPostingViewModel jobPosting, Filter filter)
        {
            bool isRightLevel = true;
            if (filter.IsJunior || filter.IsIntermediate || filter.IsSenior)
            {
                isRightLevel = (filter.IsJunior && jobPosting.Job.Levels.IsJunior)
                    || (filter.IsIntermediate && jobPosting.Job.Levels.IsIntermediate) 
                    || (filter.IsSenior && jobPosting.Job.Levels.IsSenior);
            }
            return isRightLevel;
        }

        private static bool ValueFilterOperation(JobPostingViewModel jobPosting, Filter filter)
        {
            return true;
        }

        private static bool StringSearchOperation(JobPostingViewModel jobPosting, Filter filter)
        {
            var culture = StringComparison.InvariantCultureIgnoreCase;
            if(filter.MatchCase)
                culture = StringComparison.InvariantCulture;

            var isContained = false;

            foreach (StringSearchTarget target in filter.StringSearchTargets)
            {
                string searchString;
                switch (target)
                {
                    case StringSearchTarget.EmployerName:
                        searchString = jobPosting.Job.Employer.Name;
                        break;
                    case StringSearchTarget.FullAddress:
                        searchString = jobPosting.Job.JobLocation.FullAddress;
                        break;
                    case StringSearchTarget.Region:
                        searchString = jobPosting.Job.JobLocation.Region;
                        break;
                    case StringSearchTarget.JobTitle:
                        searchString = jobPosting.Job.JobTitle;
                        break;
                    case StringSearchTarget.Levels:
                        searchString = jobPosting.Job.Levels.ToString();
                        break;
                    case StringSearchTarget.Disciplines:
                        searchString = jobPosting.Job.Disciplines.ToString();
                        break;
                    case StringSearchTarget.Comment:
                        searchString = jobPosting.Job.Comment;
                        break;
                    case StringSearchTarget.JobDescription:
                        searchString = jobPosting.Job.JobDescription;
                        break;
                    case StringSearchTarget.Job:
                        searchString = jobPosting.Job.ToString();
                        break;
                    default:
                        searchString = string.Empty;
                        break;
                }

                searchString = searchString.Replace("-\n", "").Replace("\n", "");
                if (searchString.IsNullSpaceOrEmpty())
                    continue;
                if (filter.StringSearchValues.Any(value => searchString.IndexOf(value, culture) >= 0))
                    isContained = true;
            }
            return isContained;
        }
    }
}


