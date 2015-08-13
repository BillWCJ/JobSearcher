using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;

namespace JobDownloaderModule
{
    /// <summary>
    /// Interaction logic for JobDownloaderView.xaml
    /// </summary>
    public partial class JobDownloaderView : UserControl
    {
        private JobDownloaderViewModel _viewModel;

        public JobDownloaderView()
        {
            InitializeComponent(); 
            _viewModel = this.DataContext as JobDownloaderViewModel;
            if (_viewModel == null)
            {
                _viewModel = new JobDownloaderViewModel();
                this.DataContext = _viewModel;
            }
            SetDownloadOption(true);
        }

        private void DownloadToDbOptionRadioButton_OnClick(object sender, RoutedEventArgs e)
        {
            SetDownloadOption(true);
        }

        private void SetDownloadOption(bool isDownloadToDb)
        {
            this.DownloadToDbOptionRadioButton.IsChecked = isDownloadToDb;
            this.DownloadToDbOptionInputGrid.IsEnabled = isDownloadToDb;
            this.DownloadToLocalOptionRadioButton.IsChecked = !isDownloadToDb;
            this.DownloadToLocalOptionInputGrid.IsEnabled = !isDownloadToDb;
        }

        private void DownloadToDbButton_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.DownloadAndSeedJobIntoDb();
        }

        private void DownloadToLocalOptionRadioButton_OnClick(object sender, RoutedEventArgs e)
        {
            SetDownloadOption(false);
        }

        private void SelectFileLocationButton_OnClick(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            string fileLocation = folderBrowserDialog.SelectedPath;
            fileLocation = fileLocation.TrimEnd(' ', '\\') + '\\';
            FileLocationTextBox.Text = fileLocation;
        }

        private void ExportJob(object sender, RoutedEventArgs e)
        {
            _viewModel.ExportFromDbToLocal();
        }

        private void DownloadToLocal(object sender, RoutedEventArgs e)
        {
            _viewModel.DownloadJobToLocal();
        }
    }
}