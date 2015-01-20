using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Business.Manager;
using Model.Entities.JobMine;
using Model.Entities.RateMyCoopJob;

namespace SimpleJobBrowser
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected int Count;
        protected int Index;
        protected JobReviewManager JobReviewManager;
        protected List<Job> Jobs;

        public MainWindow()
        {
            InitializeComponent();
            Jobs = JobSearcher.FindJobs();
            Count = Jobs.Count;
            Index = 0;
            //JobReviewManager = new JobReviewManager();
            Display();
        }

        private void Display()
        {
            OutputTextBox.Text = Jobs[Index].ToString("f");
            OutputTextBox2.Text = Jobs[Index].Score.ToString("Score: 0 \n") + Jobs[Index].NumberOfOpening.ToString("NumberOfOpening: 0 \n");
            //var review = JobReviewManager.GetJobReview(Jobs[Index].Employer.Name, Jobs[Index].JobTitle);
            //string reviewMsg = review.Aggregate<JobReview, string>(null, (current, r) => current + r.ToString());
            //OutputTextBox2.Text = reviewMsg;
        }

        private void PreviousJob(object sender, RoutedEventArgs e)
        {
            Index = (Index < 1) ? Index : Index - 1;
            Display();
        }

        private void NextJob(object sender, RoutedEventArgs e)
        {
            Index = (Index >= Count - 1) ? Index : Index + 1;
            Display();
        }
    }
}