using System.Windows;
using JobBrowserModule.Services;
using JobBrowserModule.ViewModels;
using JobDetailModule;

namespace UWActuallyWorks
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : Window
    {
        private readonly IReporter _aggregator = new Reporter();

        public Shell()
        {
            InitializeComponent();
            var filterPanelViewModel = new FilterPanelViewModel(_aggregator);
            var postingTableViewModel = new PostingTableViewModel(_aggregator);
            _aggregator.FilterChanged = postingTableViewModel.FilterChanged;
            _aggregator.SelectedJobChanged = JobDetailPanel.ViewModel.JobChanged;
            FilterPanel.ViewModel = filterPanelViewModel;
            JobPostingTable.ViewModel = postingTableViewModel;
            SelectPerspective(0);
        }

        private void SelectJobDownloaderPerspective(object sender, RoutedEventArgs e)
        {
            SelectPerspective(0);
        }

        private void SelectPerspective(int perspectiveIndex)
        {
            switch (perspectiveIndex)
            {
                case 0:
                    JobBrowserContainer.Visibility = Visibility.Hidden;
                    JobBrowserContainer.IsEnabled = false;
                    JobDownloaderContainer.Visibility = Visibility.Visible;
                    JobDownloaderContainer.IsEnabled = true;

                    break;
                case 1:
                    JobBrowserContainer.Visibility = Visibility.Visible;
                    JobBrowserContainer.IsEnabled = true;
                    JobDownloaderContainer.Visibility = Visibility.Collapsed;
                    JobDownloaderContainer.IsEnabled = false;
                    break;
                default:
                    break;
            }
        }

        private void SelectJobBrowserPerspective(object sender, RoutedEventArgs e)
        {
            SelectPerspective(1);
        }
    }
}