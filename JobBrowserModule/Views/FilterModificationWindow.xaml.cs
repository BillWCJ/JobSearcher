using System;
using System.Windows;
using System.Windows.Controls;
using JobBrowserModule.ViewModels;
using Model.Definition;

namespace JobBrowserModule.Views
{
    /// <summary>
    ///     Interaction logic for CreateFilterWindow.xaml
    /// </summary>
    public partial class FilterModificationWindow : Window
    {
        public FilterModificationWindow(FilterViewModel viewModel, Action saveChangeCallBack)
        {
            InitializeComponent();
            ViewModel = new FilterModificationViewModel(viewModel);
            DataContext = ViewModel;
            SaveChangeCallBack = saveChangeCallBack;
        }

        private FilterModificationViewModel ViewModel { get; set; }
        public Action SaveChangeCallBack { get; set; }

        private void SaveOrEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var errorInInput = ViewModel.ErrorInInput();
            if (errorInInput == null)
            {
                ViewModel.SaveChangeToBaseViewModel();
                SaveChangeCallBack();
                Close();
            }
            DisplayError(errorInInput);
        }

        private void DisplayError(string errorInInput)
        {
            ErrorTextBox.Text = errorInInput;
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddStringSearchTarget(object sender, RoutedEventArgs e)
        {
            ViewModel.Targets.Add((StringSearchTarget) Enum.Parse(typeof (StringSearchTarget), StringSearchTargetComboBox.SelectionBoxItem.ToString()));
        }

        private void AddStringSearchValue(object sender, RoutedEventArgs e)
        {
            ViewModel.Values.Add(StringSearchValueTextBox.Text);
            StringSearchValueTextBox.Text = string.Empty;
        }

        private void DeletedTarget(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null) ViewModel.Targets.Remove((StringSearchTarget) Enum.Parse(typeof (StringSearchTarget), button.CommandParameter.ToString()));
        }

        private void DeletedValue(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null) ViewModel.Values.Remove(button.CommandParameter.ToString());
        }
    }
}