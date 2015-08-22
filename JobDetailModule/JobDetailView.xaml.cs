using System.Windows;
using System.Windows.Controls;

namespace JobDetailModule
{
    /// <summary>
    ///     Interaction logic for JobDetailView.xaml
    /// </summary>
    public partial class JobDetailView : UserControl
    {
        public JobDetailViewModel ViewModel;

        public JobDetailView()
        {
            InitializeComponent();
            ViewModel = DataContext as JobDetailViewModel;
            if (ViewModel == null)
            {
                ViewModel = new JobDetailViewModel();
                DataContext = ViewModel;
            }
            MinimizePanel(null, null);
        }

        private void MinimizePanel(object sender, RoutedEventArgs e)
        {
            ExpandedPanelContainer.Visibility = Visibility.Collapsed;
            MinimizedPanelContainer.Visibility = Visibility.Visible;
        }
        private void MaximizePanel(object sender, RoutedEventArgs e)
        {
            ExpandedPanelContainer.Visibility = Visibility.Visible;
            MinimizedPanelContainer.Visibility = Visibility.Hidden;
        }

        private void AddSelectedJobToShortList(object sender, RoutedEventArgs e)
        {
            string name = string.IsNullOrWhiteSpace(ShortListTextBox.Text) ? ShortListComboBox.SelectionBoxItem.ToString() : ShortListTextBox.Text;
            ViewModel.AddSelectedJobToShortList(name);
        }
    }
}