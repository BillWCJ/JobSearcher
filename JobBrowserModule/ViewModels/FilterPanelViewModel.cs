using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JobBrowserModule.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using Model.Definition;

namespace JobBrowserModule.ViewModels
{
    public interface IFilterPanelViewModel
    {
        void FilterChanged();
        ObservableCollection<FilterViewModel> Filters { get; set; }
        bool IsAllSelected { get; set; }
    }

    class FilterPanelViewModelMock : IFilterPanelViewModel
    {
        public FilterPanelViewModelMock()
        {
            Filters = new ObservableCollection<FilterViewModel>(new List<FilterViewModel> {new FilterViewModel
            {
                Name = "Mech",
                Description = "Mechanical",
                IsSelected = true,
                Category = FilterCategory.StringSearch,
                StringSearchTargetData = new StringSearchTargetData
                {
                    MatchCase = true,
                    Targets = new List<StringSearchTarget> {StringSearchTarget.JobDescription, StringSearchTarget.Disciplines},
                    Values = new List<string> {"Solidworks", "Mech"}
                }
            }});
        }
        public void FilterChanged()
        {
        }

        public ObservableCollection<FilterViewModel> Filters { get; set; }
        public bool IsAllSelected { get; set; }
    }

    public class FilterPanelViewModel : IFilterPanelViewModel
    {
        private IReporter _aggregator;

        public FilterPanelViewModel()
        {
            var item = new FilterViewModel
            {
                Name = "Mech",
                Description = "Mechanical",
                IsSelected = true,
                Category = FilterCategory.StringSearch,
                StringSearchTargetData = new StringSearchTargetData
                {
                    MatchCase = true,
                    Targets = new List<StringSearchTarget> {StringSearchTarget.JobDescription, StringSearchTarget.Disciplines},
                    Values = new List<string> {"Solidworks", "Mech"}
                }
            };
            Filters = new ObservableCollection<FilterViewModel>(new List<FilterViewModel> {item});
        }

        public FilterPanelViewModel(IReporter aggregator) : this()
        {
            this._aggregator = aggregator;
        }
        

        public void FilterChanged()
        {
            IEnumerable<FilterViewModel> filterViewModels = Filters.Where(f => f.IsSelected);

            if (_aggregator != null && _aggregator.FilterChanged != null) _aggregator.FilterChanged(filterViewModels.Where(f => f.IsSelected).ToList());
        }

        public ObservableCollection<FilterViewModel> Filters { get; set; }
        public bool IsAllSelected { get; set; }
    }
}