using System;
using System.Collections.Generic;
using System.Linq;
using Common.Utility;
using JobBrowserModule.ViewModels;
using Model.Definition;

namespace JobBrowserModule.Services
{
    public static class FilterHelper
    {
        public static bool IsPostingVisible(JobPostingViewModel jobPosting, IList<FilterViewModel> filters)
        {
            foreach (FilterViewModel filter in filters)
            {
                Func<JobPostingViewModel, FilterViewModel, bool> filterOperation = null;

                switch (filter.Filter.Category)
                {
                    case FilterCategory.StringSearch:
                        filterOperation = StringSearchOperation;
                        break;
                    case FilterCategory.ValueFilter:
                        filterOperation = ValueFilterOperation;
                        break;
                    case FilterCategory.LevelSelection:
                        filterOperation = CategorySelectionOperation;
                        break;
                    case FilterCategory.LocationFilter:
                        filterOperation = LocationFilterOperation;
                        break;
                    case FilterCategory.ReviewFilter:
                        filterOperation = ReviewFilterOperation;
                        break;
                    case FilterCategory.DisciplineSelection:
                        filterOperation = CustomFilterOperation;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Does not Support Filter Category: " + filter.Filter.Category);
                }

                if (!filterOperation(jobPosting, filter))
                    return false;
            }
            return true;
        }

        private static bool CustomFilterOperation(JobPostingViewModel jobPosting, FilterViewModel filter)
        {
            return true;
        }

        private static bool ReviewFilterOperation(JobPostingViewModel jobPosting, FilterViewModel filter)
        {
            return true;
        }

        private static bool LocationFilterOperation(JobPostingViewModel jobPosting, FilterViewModel filter)
        {
            return true;
        }

        private static bool CategorySelectionOperation(JobPostingViewModel jobPosting, FilterViewModel filter)
        {
            bool isRightLevel = true;
            if (filter.Filter.IsJunior || filter.Filter.IsIntermediate || filter.Filter.IsSenior)
            {
                isRightLevel = (filter.Filter.IsJunior && jobPosting.Job.Levels.IsJunior)
                    || (filter.Filter.IsIntermediate && jobPosting.Job.Levels.IsIntermediate) 
                    || (filter.Filter.IsSenior && jobPosting.Job.Levels.IsSenior);
            }

            bool isRightDiscipline = true;
            if (filter.Filter.DisciplinesSearchTarget.Any())
            {
                isRightDiscipline = filter.Filter.DisciplinesSearchTarget.Any(discipline => jobPosting.Job.Disciplines.ContainDiscipline(discipline));
            }
            return isRightLevel && isRightDiscipline;
        }

        private static bool ValueFilterOperation(JobPostingViewModel jobPosting, FilterViewModel filter)
        {
            return true;
        }

        private static bool StringSearchOperation(JobPostingViewModel jobPosting, FilterViewModel filter)
        {
            var culture = StringComparison.InvariantCultureIgnoreCase;
            if(filter.Filter.MatchCase)
                culture = StringComparison.InvariantCulture;

            var isContained = false;

            foreach (StringSearchTarget target in filter.Filter.StringSearchTargets)
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
                        throw new ArgumentOutOfRangeException();
                }

                searchString = searchString.Replace("-\n", "").Replace("\n", "");
                if (searchString.IsNullSpaceOrEmpty())
                    continue;
                if (filter.Filter.StringSearchValues.Any(value => searchString.IndexOf(value, culture) >= 0))
                    isContained = true;
            }
            return isContained;
        }
    }
}


