using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Business.Manager;
using Microsoft.Practices.Prism.PubSubEvents;
using Model.Definition;
using Model.Entities;
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
        void RemoveFilter(FilterViewModel filter);
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
                        DisciplinesSearchTargets = new List<DisciplineEnum> {DisciplineEnum.EngMechatronics, DisciplineEnum.EngSoftware},
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

        public void RemoveFilter(FilterViewModel filter)
        {
        }
    }

    public class FilterPanelViewModel : ViewModelBase, IFilterPanelViewModel
    {
        private readonly EventAggregator _aggregator;

        public FilterPanelViewModel()
        {
            Filters = new ObservableCollection<FilterViewModel>(FilterManager.GetFilters().Select(a => new FilterViewModel{Filter = a}));
        }

        public FilterPanelViewModel(EventAggregator aggregator) : this()
        {
            _aggregator = aggregator;
        }

        public bool IsAllSelected { get; set; }

        public void FilterChanged()
        {
            var filters = Filters.Where(f => f.IsSelected).Select(f => f.Filter);
            if (_aggregator != null)
                _aggregator.GetEvent<FilterSelectionChangedEvent>().Publish(filters);
        }

        public ObservableCollection<FilterViewModel> Filters { get; set; }

        public void AddFilter(FilterViewModel newFilter)
        {
            FilterManager.AddFilter(newFilter.Filter);
            Filters.Add(newFilter);
            OnPropertyChanged("Filters");
        }

        public void FilterModified(FilterViewModel modifiedFilter)
        {
            FilterManager.UpdateFilter(modifiedFilter.Filter);
            modifiedFilter.FilterModified();
            FilterChanged();
        }

        public void RemoveFilter(FilterViewModel filter)
        {
            Filters.Remove(filter);
            FilterManager.DeleteFilter(filter.Filter);
            FilterChanged();
        }
    }
}