using System;
using Common.Utility;
using Model.Definition;
using Model.Entities.PostingFilter;
using Presentation.WPF;

namespace JobBrowserModule.ViewModels
{
    public class FilterViewModel : ViewModelBase
    {
        public bool IsSelected { get; set; }
        public Filter Filter { get; set; }

        public override string ToString()
        {
            return Filter + " " + Filter.Category.GetDescription() + " " + Filter.StringSearchTargetData;
        }
    }
}