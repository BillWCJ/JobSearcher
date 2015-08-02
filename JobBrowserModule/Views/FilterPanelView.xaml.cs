using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using JobBrowserModule.ViewModels;

namespace JobBrowserModule.Views
{
    /// <summary>
    /// Interaction logic for FilterPanel.xaml
    /// </summary>
    public partial class FilterPanelView : UserControl
    {
        public FilterPanelViewModel ViewModel { get; set; }
        public FilterPanelView()
        {
            InitializeComponent();
            this.ViewModel = new FilterPanelViewModel();
            this.DataContext = ViewModel;
        }

        private void EditFilterClicked(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;
            var dataGridRow = button.CommandParameter as DataGridRow;
            if (dataGridRow == null) return;
            var filterViewModel = dataGridRow.Item as FilterViewModel;
            StartModifyingFilter(filterViewModel);
            
        }

        private void AddFilterCLicked(object sender, RoutedEventArgs e)
        {
            var newFilter = new FilterViewModel();
            StartModifyingFilter(newFilter);
        }

        private void StartModifyingFilter(FilterViewModel newFilter)
        {
            bool success = false;
            var dialog = new FilterModificationWindow(newFilter, () => success=true);
            dialog.Owner = this.Parent as Window;
            if (dialog.ShowDialog() == true)
            {
            }
        }

        private void FilterSelectionChange(object sender, RoutedEventArgs e)
        {
            if (FilterChanged != null)
            {
                var viewModel = DataContext as FilterPanelViewModel;
                if (viewModel != null)
                {
                    IEnumerable<FilterViewModel> filterViewModels = viewModel.Filters.Where(f => f.IsSelected);
                    FilterChanged(filterViewModels);
                }
            }
        }

        public Action<IEnumerable<FilterViewModel>> FilterChanged;
    }
}
