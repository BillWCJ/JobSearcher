using System;
using System.Collections.Generic;
using Common.Utility;
using Model.Definition;
using Model.Entities;
using Presentation.WPF;

namespace JobBrowserModule.ViewModels
{
    public class FilterViewModel : ViewModelBase
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public Filter Filter { get; set; }
        public override string ToString()
        {
            return Filter + " " + Filter.Category.GetDescription();
        }

        public void FilterModified()
        {
            OnPropertyChanged("Filter");
        }
    }
}