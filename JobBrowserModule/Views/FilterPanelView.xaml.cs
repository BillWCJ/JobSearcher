using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using JobBrowserModule.ViewModels;
using Model.Definition;
using Model.Entities;

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
            StartModifyingFilter(null);
        }

        private void StartModifyingFilter(FilterViewModel filter)
        {
            bool newFilter = (filter == null);
            filter = filter ?? new FilterViewModel {Filter = new Filter()};
            bool? result = false;
            Action<bool?> callBack = (value) => result = value;
            var filterModificationWindow = new FilterModificationWindow(filter, callBack);
            filterModificationWindow.WindowStyle = WindowStyle.None;
            filterModificationWindow.Background = Brushes.Transparent;
            filterModificationWindow.AllowsTransparency = true;
            filterModificationWindow.Owner = this.Parent as Window;
            filterModificationWindow.ShowDialog();

            //result: true=save/update, false=cancel, null=delete 
            if (result == true)
            {
                if (newFilter)
                    ViewModel.AddFilter(filter);
                else
                    ViewModel.FilterModified(filter);
            }
            else if (result == null && !newFilter)
            {
                ViewModel.RemoveFilter(filter);
            }
        }

        private void FilterSelectionChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.FilterChanged();
        }
    }
}
