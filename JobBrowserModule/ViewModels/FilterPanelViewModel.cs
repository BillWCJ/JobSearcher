using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JobBrowserModule.Annotations;
using JobBrowserModule.Services;
using Model.Definition;
using Model.Entities.PostingFilter;

namespace JobBrowserModule.ViewModels
{
    public interface IFilterPanelViewModel : INotifyPropertyChanged
    {
        ObservableCollection<FilterViewModel> Filters { get; set; }
        bool IsAllSelected { get; set; }
        void FilterChanged();
        void AddFilter(FilterViewModel newFilter);
    }

    internal class FilterPanelViewModelMock : IFilterPanelViewModel
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
                        Name = "M",
                        Category = FilterCategory.StringSearch,
                        StringSearchTargetData = new StringSearchTargetData
                        {
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

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
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
                    StringSearchTargetData = new StringSearchTargetData
                    {
                        MatchCase = true,
                        Targets = new List<StringSearchTarget> {StringSearchTarget.JobDescription, StringSearchTarget.Disciplines},
                        Values = new List<string> {"Solidworks", "Mech"}
                    }
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
            NotifyPropertyChanged("Filters");
        }
    }
}