using System.Windows;
using Business.Manager;
using JobBrowserModule.Services;
using JobBrowserModule.ViewModels;
using JobDetailModule;
using JobDownloaderModule;
using Microsoft.Practices.Prism.PubSubEvents;

namespace UWActuallyWorks
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : Window
    {
        private readonly EventAggregator _aggregator = new EventAggregator();

        public Shell()
        {
            if (!BrowserEmulationManager.IsBrowserEmulationSet())
            {
                BrowserEmulationManager.SetBrowserEmulationVersion();
            }
            InitializeComponent();
            JobDownloaderView.ViewModel = new JobDownloaderViewModel(_aggregator);
            FilterPanel.ViewModel = new FilterPanelViewModel(_aggregator);
            JobPostingTable.ViewModel = new PostingTableViewModel(_aggregator);
            JobDetailPanel.ViewModel = new JobDetailViewModel(_aggregator);
            GoogleSearchPanel.ViewModel = new GoogleSearchViewModel(_aggregator);
            GoogleMapSearchPanel.ViewModel = new GoogleMapSearchViewModel(_aggregator);
            JobRatingPanel.ViewModel = new JobRatingViewModel(_aggregator);
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