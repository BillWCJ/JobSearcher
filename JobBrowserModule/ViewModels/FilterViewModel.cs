using System;
using System.Collections.Generic;
using Common.Utility;
using Model.Definition;
using Model.Entities.PostingFilter;

namespace JobBrowserModule.ViewModels
{
    public class FilterViewModel : ViewModelBase
    {
        public bool IsSelected { get; set; }
        public Filter Filter { get; set; }

        public string ToolTipString
        {
            get
            {
                return Filter.Name + Environment.NewLine + Filter.Description;
            }
        }

        public IList<StringSearchTarget> Targets
        {
            get
            {
                return Filter.StringSearchTargetData.Targets;
            }
        }

        public IList<string> Values
        {
            get
            {
                return Filter.StringSearchTargetData.Values;
            }
        }

        public override string ToString()
        {
            return ToolTipString + " " + Filter.Category.GetDescription() + " " + Filter.StringSearchTargetData;
        }

        public void AddTarget(string targetString)
        {
            StringSearchTarget newTarget;
            if (Enum.TryParse(targetString, true, out newTarget))
                if (!Filter.StringSearchTargetData.Targets.Contains(newTarget))
                    Filter.StringSearchTargetData.Targets.Add(newTarget);
            NotifyPropertyChanged("Targets");
        }

        public void DeleteTarget(string targetString)
        {
            StringSearchTarget newTarget;
            if (Enum.TryParse(targetString, true, out newTarget))
                Filter.StringSearchTargetData.Targets.Remove(newTarget);
            NotifyPropertyChanged("Targets");
        }

        public void AddValue(string newValue)
        {
            if (!newValue.IsNullSpaceOrEmpty() && !Filter.StringSearchTargetData.Values.Contains(newValue))
                Filter.StringSearchTargetData.Values.Add(newValue);
            NotifyPropertyChanged("Values");
        }

        public void DeleteValue(string newValue)
        {
            if (!newValue.IsNullSpaceOrEmpty())
                Filter.StringSearchTargetData.Values.Remove(newValue);
            NotifyPropertyChanged("Values");
        }
    }
}