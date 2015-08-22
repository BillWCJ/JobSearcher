using System.Windows.Controls;
using JobBrowserModule.Services;
using JobBrowserModule.ViewModels;

namespace JobBrowserModule.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class JobBrowserModuleView : UserControl
    {
        public JobBrowserModuleView()
        {
            InitializeComponent();
        }

        public void Setup(IReporter aggregator)
        {
        }
    }
}
