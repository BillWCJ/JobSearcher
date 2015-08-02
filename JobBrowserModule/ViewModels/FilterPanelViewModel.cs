using System.Collections.Generic;
using System.Collections.ObjectModel;
using JobBrowserModule.Services;
using Model.Definition;

namespace JobBrowserModule.ViewModels
{
    public class FilterPanelViewModel
    {
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

        public ObservableCollection<FilterViewModel> Filters { get; set; }
        public bool IsAllSelected { get; set; }
    }
}