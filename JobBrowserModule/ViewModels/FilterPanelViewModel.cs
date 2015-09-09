using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JobBrowserModule.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using Model.Definition;
using Model.Entities.PostingFilter;
using Presentation.WPF;
using Presentation.WPF.Events;

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
                        DisciplinesSearchTarget = new List<DisciplineEnum> {DisciplineEnum.EngMechatronics, DisciplineEnum.EngSoftware},
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
        private readonly EventAggregator _aggregator;

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
                    DisciplinesSearchTarget = new List<DisciplineEnum> {DisciplineEnum.EngMechatronics, DisciplineEnum.EngSoftware},
                    MatchCase = true,
                    StringSearchTargets = new List<StringSearchTarget> {StringSearchTarget.JobDescription, StringSearchTarget.Disciplines},
                    StringSearchValues = new List<string> {"Solidworks", "Mech"}
                }
            };
            Filters = new ObservableCollection<FilterViewModel>(new List<FilterViewModel>(){item});
        }

        public FilterPanelViewModel(EventAggregator aggregator) : this()
        {
            _aggregator = aggregator;
        }

        public bool IsAllSelected { get; set; }

        public void FilterChanged()
        {
                    IEnumerable<Filter> filters = Filters.Where(f => f.IsSelected).Select(f => f.Filter);
                    if (_aggregator != null)
                        _aggregator.GetEvent<FilterSelectionChangedEvent>().Publish(filters);
        }

        public ObservableCollection<FilterViewModel> Filters { get; set; }

        public void AddFilter(FilterViewModel newFilter)
        {
            Filters.Add(newFilter);
            OnPropertyChanged("Filters");
        }

        public void FilterModified(FilterViewModel modifiedFilter)
        {
            //save filter into db
            modifiedFilter.FilterModified();
            FilterChanged();
        }
    }
}