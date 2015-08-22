using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using JobBrowserModule.ViewModels;
using Model.Definition;
using Model.Entities.PostingFilter;

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
            MaximizePanel(null, null);
        }

        private void EditFilterClicked(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;
            var dataGridRow = button.CommandParameter as DataGridRow;
            if (dataGridRow == null) return;
            var filterViewModel = dataGridRow.Item as FilterViewModel;
            var modifiedFilter = StartModifyingFilter(filterViewModel);
            ViewModel.FilterModified(modifiedFilter);

        }

        private void AddFilterCLicked(object sender, RoutedEventArgs e)
        {
            var newFilter = StartModifyingFilter(new FilterViewModel{Filter = new Filter()});
            if (newFilter != null) ViewModel.AddFilter(newFilter);
        }

        private FilterViewModel StartModifyingFilter(FilterViewModel newFilter)
        {
            bool success = false;
            var filterModificationWindow = new FilterModificationWindow(newFilter, () => success=true);
            filterModificationWindow.WindowStyle = WindowStyle.None;
            filterModificationWindow.Background = Brushes.Transparent;
            filterModificationWindow.ShowInTaskbar = false;
            filterModificationWindow.AllowsTransparency = true;
            filterModificationWindow.Owner = this.Parent as Window;
            filterModificationWindow.ShowDialog();
            if (success)
                return newFilter;
            return null;
        }

        private void FilterSelectionChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.FilterChanged();
        }

        private void MinimizePanel(object sender, RoutedEventArgs e)
        {
            ExpandedPanelContainer.Visibility = Visibility.Collapsed;
            MinimizedPanelContainer.Visibility = Visibility.Visible;
        }
        private void MaximizePanel(object sender, RoutedEventArgs e)
        {
            ExpandedPanelContainer.Visibility = Visibility.Visible;
            MinimizedPanelContainer.Visibility = Visibility.Hidden;
        }
    }
}
