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
using Microsoft.Practices.Prism.PubSubEvents;

namespace UWActuallyWorks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : Window
    {
        readonly IEventAggregator _aggregator = new EventAggregator();
        public Shell()
        {
            InitializeComponent();
            //PostingTableViewModel postingTableViewModel = PostingTable.DataContext as PostingTableViewModel;
            //FilterPanel.FilterChanged += postingTableViewModel.FilterChanged;
        }
    }
}
