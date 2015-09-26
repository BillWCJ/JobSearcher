using System.Windows;
using System.Windows.Media;
using Business.Manager;
using JobBrowserModule.Services;
using JobBrowserModule.ViewModels;
using JobDetailModule;
using JobDownloaderModule;
using Microsoft.Practices.Prism.PubSubEvents;
using Presentation.WPF.Events;

namespace UWActuallyWorks
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EventAggregator _aggregator = new EventAggregator();
        private readonly UserAccountManager _userAccountManager = new UserAccountManager();
        private readonly JobDownloaderView _jobDownloaderView;
        private MainWindowViewModel ViewModel { get; set; }

        public MainWindow()
        {
            if (!BrowserEmulationManager.IsBrowserEmulationSet())
            {
                BrowserEmulationManager.SetBrowserEmulationVersion();
            }
            InitializeComponent();
            ViewModel = new MainWindowViewModel(_aggregator);
            this.DataContext = ViewModel;

            FilterPanel.ViewModel = new FilterPanelViewModel(_aggregator);
            JobPostingTable.ViewModel = new PostingTableViewModel(_aggregator);
            JobDetailPanel.ViewModel = new JobDetailViewModel(_aggregator, _userAccountManager);
            GoogleSearchPanel.ViewModel = new GoogleSearchViewModel(_aggregator);
            GoogleMapSearchPanel.ViewModel = new GoogleMapSearchViewModel(_aggregator);
            JobRatingPanel.ViewModel = new JobRatingViewModel(_aggregator);
            _jobDownloaderView = new JobDownloaderView() { ViewModel = new JobDownloaderViewModel(_aggregator, _userAccountManager) };
            DisplayJobDownloaderWindow();
        }

        private void DisplayJobDownloaderWindow()
        {
            lock (_jobDownloaderView)
            {
                var jobDownloaderWindow = new Window
                {
                    Background = Brushes.Transparent,
                    Owner = this.Parent as Window,
                    Title = "UWCoopJobFinder - SignOn & Data Import & Export",
                    Content = _jobDownloaderView
                };
                jobDownloaderWindow.ShowDialog();
            }
        }

        private void JobDownloadClick(object sender, RoutedEventArgs e)
        {
            DisplayJobDownloaderWindow();
        }
    }
}