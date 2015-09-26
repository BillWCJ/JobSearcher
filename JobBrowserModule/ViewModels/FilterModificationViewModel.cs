using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using Business.Manager;
using Common.Utility;
using Model.Definition;

namespace JobBrowserModule.ViewModels
{
    public interface IFilterModificationViewModel
    {
        ObservableCollection<StringSearchTarget> StringSearchTargets { get; set; }
        ObservableCollection<string> StringSearchValues { get; set; }
        ObservableCollection<DisciplineEnum> DisciplineSearchTargets { get; set; }
        FilterCategory SelectedFilterCategory { get; set; }

        string Name { get; set; }
        string Description { get; set; }
        bool IsAntiFilter { get; set; }
        bool MatchCase { get; set; }
        bool IsJunior { get; set; }
        bool IsIntermediate { get; set; }
        bool IsSenior { get; set; }
        double LowerLimit { get; set; }
        double UpperLimit { get; set; }
        ValueSearchTarget ValueSearchSelectedItem { get; set; }
        bool IsScoreFilter { get; set; }
        int PointValue { get; set; }
        TermType DurationSelectedItem { get; set; }
        double UpperRatingLimit { get; set; }
        int MaximumReviews { get; set; }
        double LowerRatingLimit { get; set; }
        void SaveChangeToBaseViewModel();
    }

    public class FilterModificationViewModelMock : IFilterModificationViewModel
    {
        public ObservableCollection<StringSearchTarget> StringSearchTargets { get; set; }
        public ObservableCollection<string> StringSearchValues { get; set; }
        public ObservableCollection<DisciplineEnum> DisciplineSearchTargets { get; set; }
        public FilterCategory SelectedFilterCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAntiFilter { get; set; }
        public bool MatchCase { get; set; }
        public bool IsJunior { get; set; }
        public bool IsIntermediate { get; set; }
        public bool IsSenior { get; set; }

        public double LowerLimit { get; set; }

        public double UpperLimit { get; set; }

        public ValueSearchTarget ValueSearchSelectedItem { get; set; }

        public bool IsScoreFilter { get; set; }
        public int PointValue { get; set; }

        public TermType DurationSelectedItem { get; set; }

        public double UpperRatingLimit { get; set; }
        public int MaximumReviews { get; set; }
        public double LowerRatingLimit { get; set; }

        public void SaveChangeToBaseViewModel()
        {
        }

        public FilterModificationViewModelMock()
        {
            StringSearchTargets = new ObservableCollection<StringSearchTarget>(new List<StringSearchTarget>(){StringSearchTarget.EmployerName});
            StringSearchValues = new ObservableCollection<string>(new List<string>(){"Mech"});
            Name = "Mech";
            Description = "Mechanical";
        }
    }

    public class FilterModificationViewModel : IFilterModificationViewModel
    {
        private readonly FilterViewModel _filterViewModel;

        public FilterModificationViewModel(FilterViewModel viewModel = null)
        {
            _filterViewModel = viewModel;
            if (_filterViewModel != null)
            {
                SelectedFilterCategory = _filterViewModel.Filter.Category;
                Name = _filterViewModel.Filter.Name;
                Description = _filterViewModel.Filter.Description;
                IsAntiFilter = _filterViewModel.Filter.IsAntiFilter;
                StringSearchTargets = new ObservableCollection<StringSearchTarget>(_filterViewModel.Filter.StringSearchTargets);
                StringSearchValues = new ObservableCollection<string>(_filterViewModel.Filter.StringSearchValues);
                MatchCase = _filterViewModel.Filter.MatchCase;
                DisciplineSearchTargets = new ObservableCollection<DisciplineEnum>(_filterViewModel.Filter.DisciplinesSearchTargets);
                IsJunior = _filterViewModel.Filter.IsJunior;
                IsIntermediate = _filterViewModel.Filter.IsIntermediate;
                IsSenior = _filterViewModel.Filter.IsSenior;
                ValueSearchSelectedItem = _filterViewModel.Filter.ValueSearchSelectedItem;
                LowerLimit = _filterViewModel.Filter.LowerLimit;
                UpperLimit = _filterViewModel.Filter.UpperLimit;
                PointValue = _filterViewModel.Filter.PointValue;
                IsScoreFilter = PointValue != 0;
                DurationSelectedItem = _filterViewModel.Filter.Duration;
                UpperRatingLimit = _filterViewModel.Filter.UpperRatingLimit;
                LowerRatingLimit = _filterViewModel.Filter.LowerRatingLimit;
                MaximumReviews = _filterViewModel.Filter.MaximumResult;
            }
            else
            {
                SelectedFilterCategory = FilterCategory.StringSearch;
                StringSearchTargets = new ObservableCollection<StringSearchTarget>();
                StringSearchValues = new ObservableCollection<string>();
                DisciplineSearchTargets = new ObservableCollection<DisciplineEnum>();
            }
        }

        public ObservableCollection<StringSearchTarget> StringSearchTargets { get; set; }
        public ObservableCollection<string> StringSearchValues { get; set; }
        public ObservableCollection<DisciplineEnum> DisciplineSearchTargets { get; set; }
        public FilterCategory SelectedFilterCategory { get; set; }
        public Dictionary<FilterCategory, bool> FilterCategorySelection { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAntiFilter { get; set; }
        public bool MatchCase { get; set; }
        public bool IsJunior { get; set; }
        public bool IsIntermediate { get; set; }
        public bool IsSenior { get; set; }
        public double LowerLimit { get; set; }
        public double UpperLimit { get; set; }
        public ValueSearchTarget ValueSearchSelectedItem { get; set; }
        public bool IsScoreFilter { get; set; }
        public int PointValue { get; set; }
        public TermType DurationSelectedItem { get; set; }        
        public double UpperRatingLimit { get; set; }
        public int MaximumReviews { get; set; }
        public double LowerRatingLimit { get; set; }
        public void SaveChangeToBaseViewModel()
        {
            _filterViewModel.Filter.Name = Name;
            _filterViewModel.Filter.Description = Description;
            _filterViewModel.Filter.IsAntiFilter = IsAntiFilter;
            _filterViewModel.Filter.Category = SelectedFilterCategory;
            _filterViewModel.Filter.StringSearchTargets = StringSearchTargets.ToList();
            _filterViewModel.Filter.StringSearchValues = StringSearchValues.ToList();
            _filterViewModel.Filter.MatchCase = MatchCase;
            _filterViewModel.Filter.DisciplinesSearchTargets = DisciplineSearchTargets.ToList();
            _filterViewModel.Filter.IsJunior = IsJunior;
            _filterViewModel.Filter.IsIntermediate = IsIntermediate;
            _filterViewModel.Filter.IsSenior = IsSenior;
            _filterViewModel.Filter.ValueSearchSelectedItem = ValueSearchSelectedItem;
            _filterViewModel.Filter.LowerLimit = LowerLimit;
            _filterViewModel.Filter.UpperLimit = UpperLimit;
            _filterViewModel.Filter.PointValue = IsScoreFilter ? PointValue : 0;
            _filterViewModel.Filter.Duration = DurationSelectedItem;
            _filterViewModel.Filter.UpperRatingLimit = UpperRatingLimit;
            _filterViewModel.Filter.LowerRatingLimit = LowerRatingLimit;
            _filterViewModel.Filter.MaximumResult = MaximumReviews;

        }
    }
}