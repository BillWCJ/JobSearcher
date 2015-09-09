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
            ViewModel.SaveChangeToBaseViewModel();
            SaveChangeCallBack();
            Close();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddStringSearchTarget(object sender, RoutedEventArgs e)
        {
            ViewModel.StringSearchTargets.Add((StringSearchTarget) Enum.Parse(typeof (StringSearchTarget), StringSearchTargetComboBox.SelectionBoxItem.ToString()));
        }

        private void AddStringSearchValue(object sender, RoutedEventArgs e)
        {
            ViewModel.StringSearchValues.Add(StringSearchValueTextBox.Text);
            StringSearchValueTextBox.Text = string.Empty;
        }

        private void DeletedStringSearchTarget(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null) ViewModel.StringSearchTargets.Remove((StringSearchTarget) Enum.Parse(typeof (StringSearchTarget), button.CommandParameter.ToString()));
        }

        private void DeletedStringSearchValue(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null) ViewModel.StringSearchValues.Remove(button.CommandParameter.ToString());
        }

        private void AddDisciplineSearchTarget(object sender, RoutedEventArgs e)
        {
            ViewModel.DisciplineSearchTargets.Add((DisciplineEnum)Enum.Parse(typeof(DisciplineEnum), DisciplineSearchTargetComboBox.SelectionBoxItem.ToString()));
        }

        private void DeletedDisciplineSearchTarget(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null) ViewModel.DisciplineSearchTargets.Remove((DisciplineEnum)Enum.Parse(typeof(DisciplineEnum), button.CommandParameter.ToString()));
        }
    }
}