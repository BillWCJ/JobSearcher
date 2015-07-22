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

namespace UWActuallyWorks.PostingTable
{
    /// <summary>
    /// Interaction logic for PostingTable.xaml
    /// </summary>
    public partial class PostingTableView : UserControl
    {
        public PostingTableViewModel ViewModel;
        public PostingTableView()
        {
            InitializeComponent();
            this.ViewModel = new PostingTableViewModel();
            this.JobPostingTable.ItemsSource = ViewModel.JobPostings;
        }
    }
}
