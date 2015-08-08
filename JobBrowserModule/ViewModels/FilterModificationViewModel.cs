using System;
using System.Collections.ObjectModel;
using System.Linq;
using Common.Utility;
using Model.Definition;

namespace JobBrowserModule.ViewModels
{
    public interface IFilterModificationViewModel
    {
        ObservableCollection<StringSearchTarget> Targets { get; set; }
        ObservableCollection<string> Values { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool MatchCase { get; set; }
        void SaveChangeToBaseViewModel();
        string ErrorInInput();
    }

    public class FilterModificationViewModelMock : IFilterModificationViewModel
    {
        public ObservableCollection<StringSearchTarget> Targets { get; set; }
        public ObservableCollection<string> Values { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool MatchCase { get; set; }
        public void SaveChangeToBaseViewModel()
        {
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
                Targets = new ObservableCollection<StringSearchTarget>(_filterViewModel.Filter.StringSearchTargetData.Targets);
                Values = new ObservableCollection<string>(_filterViewModel.Filter.StringSearchTargetData.Values);
                Name = _filterViewModel.Filter.Name;
                Description = _filterViewModel.Filter.Description;
                MatchCase = _filterViewModel.Filter.StringSearchTargetData.MatchCase;
            }
            else
            {
                Targets = new ObservableCollection<StringSearchTarget>();
                Values = new ObservableCollection<string>();
            }
        }

        public ObservableCollection<StringSearchTarget> Targets { get; set; }
        public ObservableCollection<string> Values { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool MatchCase { get; set; }

        public void SaveChangeToBaseViewModel()
        {
            _filterViewModel.Filter.StringSearchTargetData.Targets = Targets.ToList();
            _filterViewModel.Filter.StringSearchTargetData.Values = Values.ToList();
            _filterViewModel.Filter.Name = Name;
            _filterViewModel.Filter.Description = Description;
            _filterViewModel.Filter.StringSearchTargetData.MatchCase = MatchCase;
        }

        public string ErrorInInput()
        {
            var errors = string.Empty;
            if (Name.IsNullSpaceOrEmpty())
                errors += "Name is Empty";
            if (!Targets.Any())
                errors += "Targets is Empty";
            if (!Values.Any())
                errors += "Values is Empty";
            return errors.IsNullSpaceOrEmpty() ? null : errors;
        }
    }
}