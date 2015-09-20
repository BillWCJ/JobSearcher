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

            var window = new Window
            {
                Background = Brushes.Transparent,
                Owner = this.Parent as Window,
                Title = "UWCoopJobFinder - SignOn & Data Import & Export",
                Content = new JobDownloaderView() {ViewModel = new JobDownloaderViewModel(_aggregator, _userAccountManager)}
            };
            window.ShowDialog();

            FilterPanel.ViewModel = new FilterPanelViewModel(_aggregator);
            JobPostingTable.ViewModel = new PostingTableViewModel(_aggregator);
            JobDetailPanel.ViewModel = new JobDetailViewModel(_aggregator);
            GoogleSearchPanel.ViewModel = new GoogleSearchViewModel(_aggregator);
            GoogleMapSearchPanel.ViewModel = new GoogleMapSearchViewModel(_aggregator);
            JobRatingPanel.ViewModel = new JobRatingViewModel(_aggregator);
        }
    }
}