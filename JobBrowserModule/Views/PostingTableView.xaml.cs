using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using JobBrowserModule.ViewModels;
using Model.Entities.JobMine;

namespace JobBrowserModule.Views
{
    /// <summary>
    ///     Interaction logic for PostingTable.xaml
    /// </summary>
    public partial class PostingTableView : UserControl
    {
        private IPostingTableViewModel _viewModel;

        public PostingTableView()
        {
            InitializeComponent();
        }

        public IPostingTableViewModel ViewModel
        {
            get
            {
                return _viewModel ?? new PostingTableViewModelMock();
            }
            set
            {
                _viewModel = value;
                DataContext = _viewModel;
            }
        }

        private void OpenPostingClicked(object sender, RoutedEventArgs e)
        {
            var jobPostingViewModel = GetViewModel(sender);
            if (jobPostingViewModel == null) return;
            Process.Start(jobPostingViewModel.Job.JobUrl);
        }

        private void GoogleEmployerClicked(object sender, RoutedEventArgs e)
        {
            var jobPostingViewModel = GetViewModel(sender);
            if (jobPostingViewModel == null) return;
            Process.Start(@"http://www.google.com/search?q=" + jobPostingViewModel.Job.Employer.Name);
        }

        private void SearchLocationClicked(object sender, RoutedEventArgs e)
        {
            var jobPostingViewModel = GetViewModel(sender);
            if (jobPostingViewModel == null) return;
            Process.Start(@"http://maps.google.com/?q=" + jobPostingViewModel.Job.Employer.Name + " " + jobPostingViewModel.Job.JobLocation.Region);
        }

        private static JobPostingViewModel GetViewModel(object sender)
        {
            var button = sender as Button;
            if (button == null) return null;
            var dataGridRow = button.CommandParameter as DataGridRow;
            if (dataGridRow == null) return null;
            return dataGridRow.Item as JobPostingViewModel;
        }

        private void SelectedJobChanged(object sender, SelectionChangedEventArgs e)
        {
            var jobPostingViewModel = this.JobPostingTable.SelectedItem as JobPostingViewModel;
            if (jobPostingViewModel != null)
            {
                var job = jobPostingViewModel.Job;
                _viewModel.SelectedJobChanged(job);
            }
        }

        //private void AddSelectedJobsToShortList(object sender, RoutedEventArgs e)
        //{
        //    string name = string.IsNullOrWhiteSpace(ShortListTextBox.Text) ? ShortListComboBox.SelectionBoxItem.ToString() : ShortListTextBox.Text;
        //    ViewModel.AddSelectedJobsToShortList(name);
        //}
        private void KeyWordSearchToggled(object sender, RoutedEventArgs e)
        {
            ViewModel.KeyWordSearchToggled();
        }
    }
}