using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JobBrowserModule.Services;
using Model.Definition;
using Model.Entities.PostingFilter;
using Presentation.WPF;

namespace JobBrowserModule.ViewModels
{
    public interface IFilterPanelViewModel : IViewModelBase
    {
        ObservableCollection<FilterViewModel> Filters { get; set; }
        bool IsAllSelected { get; set; }
        void FilterChanged();
        void AddFilter(FilterViewModel newFilter);
        void FilterModified(FilterViewModel modifiedFilter);
    }

    internal class FilterPanelViewModelMock : ViewModelBase, IFilterPanelViewModel
    {
        public FilterPanelViewModelMock()
        {
            Filters = new ObservableCollection<FilterViewModel>(new List<FilterViewModel>
            {
                new FilterViewModel
                {
                    IsSelected = true,
                    Filter = new Filter
                    {
                        Name = "Mech",
                        Description = "Mechanical",
                        Category = FilterCategory.StringSearch,
                        DisciplinesSearchTarget = new List<DisciplineEnum> {DisciplineEnum.ENGMechatronics, DisciplineEnum.ENGSoftware},
                        MatchCase = true,
                        StringSearchTargets = new List<StringSearchTarget> {StringSearchTarget.JobDescription, StringSearchTarget.Disciplines},
                        StringSearchValues = new List<string> {"Solidworks", "Mech"}
                    }
                }
            });
        }
        public void FilterChanged()
        {
        }
        public ObservableCollection<FilterViewModel> Filters { get; set; }
        public bool IsAllSelected { get; set; }
        public void AddFilter(FilterViewModel newFilter)
        {
        }
        public void FilterModified(FilterViewModel modifiedFilter)
        {
        }
    }

    public class FilterPanelViewModel : ViewModelBase, IFilterPanelViewModel
    {
        private readonly IReporter _aggregator;

        public FilterPanelViewModel()
        {
            var item = new FilterViewModel
            {
                IsSelected = true,
                Filter = new Filter
                {
                    Name = "Mech",
                    Description = "Mechanical",
                    Category = FilterCategory.StringSearch,
                    DisciplinesSearchTarget = new List<DisciplineEnum> {DisciplineEnum.ENGMechatronics, DisciplineEnum.ENGSoftware},
                    MatchCase = true,
                    StringSearchTargets = new List<StringSearchTarget> {StringSearchTarget.JobDescription, StringSearchTarget.Disciplines},
                    StringSearchValues = new List<string> {"Solidworks", "Mech"}
                }
            };
            Filters = new ObservableCollection<FilterViewModel>(new List<FilterViewModel>(){item});
        }

        public FilterPanelViewModel(IReporter aggregator) : this()
        {
            _aggregator = aggregator;
        }

        public void FilterChanged()
        {
            var filterViewModels = Filters.Where(f => f.IsSelected);

            if (_aggregator != null && _aggregator.FilterChanged != null) 
                _aggregator.FilterChanged(filterViewModels.Where(f => f.IsSelected).ToList());
        }

        public ObservableCollection<FilterViewModel> Filters { get; set; }
        public bool IsAllSelected { get; set; }

        public void AddFilter(FilterViewModel newFilter)
        {
            Filters.Add(newFilter);
            OnPropertyChanged("Filters");
        }

        public void FilterModified(FilterViewModel modifiedFilter)
        {
            if (modifiedFilter == null)
                return;
            int index = Filters.ToList().FindIndex(f => f.Filter.Id == modifiedFilter.Filter.Id);
            Filters.RemoveAt(index);
            Filters.Insert(index, modifiedFilter);
        }
    }
}