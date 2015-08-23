using System.Windows;
using System.Windows.Controls;

namespace JobDetailModule
{
    /// <summary>
    ///     Interaction logic for JobDetailView.xaml
    /// </summary>
    public partial class JobDetailView : UserControl
    {
        private JobDetailViewModel _viewModel;

        public JobDetailViewModel ViewModel
        {
            get
            {
                return _viewModel ?? new JobDetailViewModel();
            }
            set
            {
                _viewModel = value;
                this.DataContext = _viewModel;
            }
        }

        public JobDetailView()
        {
            InitializeComponent();
        }

        private void AddSelectedJobToShortList(object sender, RoutedEventArgs e)
        {
            string name = string.IsNullOrWhiteSpace(ShortListTextBox.Text) ? ShortListComboBox.SelectionBoxItem.ToString() : ShortListTextBox.Text;
            ViewModel.AddSelectedJobToShortList(name);
        }
    }
}