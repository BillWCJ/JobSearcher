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

namespace JobDetailModule
{
    /// <summary>
    /// Interaction logic for GoogleMapSearchView.xaml
    /// </summary>
    public partial class GoogleMapSearchView : UserControl
    {
        private GoogleMapSearchViewModel _viewModel;

        public GoogleMapSearchViewModel ViewModel
        {
            get
            {
                return _viewModel ?? new GoogleMapSearchViewModel();
            }
            set
            {
                _viewModel = value;
                this.DataContext = _viewModel;
            }
        }

        public GoogleMapSearchView()
        {
            InitializeComponent();
        }
    }
}
