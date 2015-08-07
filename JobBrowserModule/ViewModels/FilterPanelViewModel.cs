using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JobBrowserModule.Annotations;
using JobBrowserModule.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using Model.Definition;
using Model.Entities.PostingFilter;

namespace JobBrowserModule.ViewModels
{
    public interface IFilterPanelViewModel : INotifyPropertyChanged
    {
        void FilterChanged();
        ObservableCollection<FilterViewModel> Filters { get; set; }
        bool IsAllSelected { get; set; }
        void AddFilter(FilterViewModel newFilter);
    }

    class FilterPanelViewModelMock : IFilterPanelViewModel
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
                        Name = "asdasd",
                        Description = "Mechanical",
                        Category = FilterCategory.StringSearch,
                        StringSearchTargetData = new StringSearchTargetData
                        {
                            MatchCase = true,
                            Targets = new List<StringSearchTarget> {StringSearchTarget.JobDescription, StringSearchTarget.Disciplines},
                            Values = new List<string> {"Solidworks", "Mech"}
                        }
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

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class FilterPanelViewModel : IFilterPanelViewModel
    {
        private IReporter _aggregator;

        public FilterPanelViewModel()
        {
            Filters = new ObservableCollection<FilterViewModel>();
            var item = new FilterViewModel
            {
                IsSelected = true,
                Filter = new Filter
                {
                    Name = "Mech",
                    Description = "Mechanical",
                    Category = FilterCategory.StringSearch,
                    StringSearchTargetData = new StringSearchTargetData
                    {
                        MatchCase = true,
                        Targets = new List<StringSearchTarget> { StringSearchTarget.JobDescription, StringSearchTarget.Disciplines },
                        Values = new List<string> { "Solidworks", "Mech" }
                    }
                }
            };
            Filters.Add(item);
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
        public void AddFilter(FilterViewModel newFilter)
        {
            Filters.Add(newFilter);
            NotifyPropertyChanged("Filters");
        }
        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}