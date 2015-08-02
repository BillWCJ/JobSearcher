using System;
using System.Windows;
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
            TargetListBox.ItemsSource = ViewModel.StringSearchTargetData.Targets;
            ValueListBox.ItemsSource = ViewModel.StringSearchTargetData.Values;
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
        }

        private string ContainErrorInInput()
        {
            return null;
        }
    }
}