using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
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
        bool MatchCase { get; set; }
        bool IsJunior { get; set; }
        bool IsIntermediate { get; set; }
        bool IsSenior { get; set; }
        void SaveChangeToBaseViewModel();
        string ErrorInInput();
    }

    public class FilterModificationViewModelMock : IFilterModificationViewModel
    {
        public ObservableCollection<StringSearchTarget> StringSearchTargets { get; set; }
        public ObservableCollection<string> StringSearchValues { get; set; }
        public ObservableCollection<DisciplineEnum> DisciplineSearchTargets { get; set; }
        public FilterCategory SelectedFilterCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool MatchCase { get; set; }
        public bool IsJunior { get; set; }
        public bool IsIntermediate { get; set; }
        public bool IsSenior { get; set; }

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
            

        public string ErrorInInput()
        {
            return String.Empty;
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
                StringSearchTargets = new ObservableCollection<StringSearchTarget>(_filterViewModel.Filter.StringSearchTargets);
                StringSearchValues = new ObservableCollection<string>(_filterViewModel.Filter.StringSearchValues);
                DisciplineSearchTargets = new ObservableCollection<DisciplineEnum>(_filterViewModel.Filter.DisciplinesSearchTarget);
                Name = _filterViewModel.Filter.Name;
                Description = _filterViewModel.Filter.Description;
                MatchCase = _filterViewModel.Filter.MatchCase;
            }
            else
            {
                StringSearchTargets = new ObservableCollection<StringSearchTarget>();
                StringSearchValues = new ObservableCollection<string>();
                DisciplineSearchTargets = new ObservableCollection<DisciplineEnum>();
            }
            SelectedFilterCategory = FilterCategory.StringSearch;
        }

        public ObservableCollection<StringSearchTarget> StringSearchTargets { get; set; }
        public ObservableCollection<string> StringSearchValues { get; set; }
        public ObservableCollection<DisciplineEnum> DisciplineSearchTargets { get; set; }
        public FilterCategory SelectedFilterCategory { get; set; }
        public Dictionary<FilterCategory, bool> FilterCategorySelection { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool MatchCase { get; set; }
        public bool IsJunior { get; set; }
        public bool IsIntermediate { get; set; }
        public bool IsSenior { get; set; }

        public void SaveChangeToBaseViewModel()
        {
            _filterViewModel.Filter.StringSearchTargets = StringSearchTargets.ToList();
            _filterViewModel.Filter.StringSearchValues = StringSearchValues.ToList();
            _filterViewModel.Filter.Name = Name;
            _filterViewModel.Filter.Description = Description;
            _filterViewModel.Filter.MatchCase = MatchCase;
        }

        public string ErrorInInput()
        {
            var errors = string.Empty;
            if (Name.IsNullSpaceOrEmpty())
                errors += "Name is Empty";
            if (!StringSearchTargets.Any())
                errors += "Targets is Empty";
            if (!StringSearchValues.Any())
                errors += "Values is Empty";
            return errors.IsNullSpaceOrEmpty() ? null : errors;
        }
    }
}