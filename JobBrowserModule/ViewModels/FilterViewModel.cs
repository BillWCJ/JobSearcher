using System;
using Common.Utility;
using JobBrowserModule.Services;
using Model.Definition;

namespace JobBrowserModule.ViewModels
{
    public class FilterViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public FilterCategory Category { get; set; }
        public StringSearchTargetData StringSearchTargetData { get; set; }
        public bool IsSelected { get; set; }

        public string ToolTipString
        {
            get
            {
                return Name + Environment.NewLine + Description;
            }
        }

        public override string ToString()
        {
            return ToolTipString + " " + Category.GetDescription() + " " + StringSearchTargetData;
        }
    }
}