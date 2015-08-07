using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Common.Utility;
using JobBrowserModule.ViewModels;

namespace JobBrowserModule.Views
{
    /// <summary>
    ///     Interaction logic for CreateFilterWindow.xaml
    /// </summary>
    public partial class FilterModificationWindow : Window
    {
        public FilterModificationWindow(FilterViewModel viewModel, Action callBack)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = ViewModel;
            CallBack = callBack;
        }

        private FilterViewModel ViewModel { get; set; }
        public Action CallBack { get; set; }

        private void SaveOrEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var errorInInput = ContainErrorInInput();
            if (errorInInput == null)
            {
                CallBack();
                Close();
            }
            DisplayError(errorInInput);
        }

        private void DisplayError(string errorInInput)
        {
            ErrorTextBox.Text = errorInInput;
        }

        private string ContainErrorInInput()
        {
            var errors = string.Empty;
            if (ViewModel.Filter.Name.IsNullSpaceOrEmpty())
                errors += "Name is Empty";
            if (!ViewModel.Filter.StringSearchTargetData.Targets.Any())
                errors += "Targets is Empty";
            if (!ViewModel.Filter.StringSearchTargetData.Values.Any())
                errors += "Values is Empty";
            return errors.IsNullSpaceOrEmpty() ? null : errors;
        }

        private void AddStringSearchTarget(object sender, RoutedEventArgs e)
        {
            ViewModel.AddTarget(StringSearchTargetComboBox.SelectionBoxItem.ToString());
            TargetListBox.ItemsSource = ViewModel.Filter.StringSearchTargetData.Targets;
        }

        private void AddStringSearchValue(object sender, RoutedEventArgs e)
        {
            ViewModel.AddValue(StringSearchValueTextBox.Text);
            StringSearchValueTextBox.Text = string.Empty;
            ValueListBox.ItemsSource = ViewModel.Filter.StringSearchTargetData.Values;
        }

        private void DeletedTarget(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null) ViewModel.DeleteTarget(button.CommandParameter.ToString());
            TargetListBox.ItemsSource = ViewModel.Filter.StringSearchTargetData.Targets;
        }

        private void DeletedValue(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null) ViewModel.DeleteValue(button.CommandParameter.ToString());
            ValueListBox.ItemsSource = ViewModel.Filter.StringSearchTargetData.Values;
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}