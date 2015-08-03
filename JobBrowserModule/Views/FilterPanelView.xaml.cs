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
        private IFilterPanelViewModel _viewModel;

        public IFilterPanelViewModel ViewModel
        {
            get
            {
                return _viewModel ?? new FilterPanelViewModelMock();
            }
            set
            {
                _viewModel = value;
                this.DataContext = _viewModel;
            }
        }

        public FilterPanelView()
        {
            InitializeComponent();
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

        private void FilterSelectionChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.FilterChanged();
        }
    }
}
