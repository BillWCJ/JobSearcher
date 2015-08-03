using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JobBrowserModule.Services;
using JobBrowserModule.ViewModels;
using Microsoft.Practices.Prism.PubSubEvents;

namespace UWActuallyWorks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : Window
    {
        readonly IReporter _aggregator = new Reporter();
        public Shell()
        {
            InitializeComponent();
            var filterPanelViewModel = new FilterPanelViewModel(_aggregator);
            var postingTableViewModel = new PostingTableViewModel(_aggregator);
            _aggregator.FilterChanged = postingTableViewModel.FilterChanged;
            this.FilterPanel.ViewModel = filterPanelViewModel;
            this.JobPostingTable.ViewModel = postingTableViewModel;
        }
    }

}
