using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using Business.DataBaseSeeder;
using Business.Manager;
using Data.EF.JseDb;
using Data.IO.Local;
using Model.Definition;
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
            SetUp();
        }

        private JseDbContext Db { get; set; }

        private void SetUp()
        {
            JobReviewManager = new JobReviewManager();
            Jobs = JobSearcher.FindJobs();
            Count = Jobs.Count;
            Index = 0;
            Display();
        }

        private void Display()
        {
            if (Count <= 0)
            {
                MainTextBox.Text = "No Jobs Found";
                return;
            }

            MainTextBox.Text = Jobs[Index].ToString("f");
            SideTextBox.Text = Jobs[Index].Score.ToString("Score: 0 \n") + Jobs[Index].NumberOfOpening.ToString("NumberOfOpening: 0 \n");
            //IEnumerable<JobReview> review = JobReviewManager.GetJobReview(Jobs[Index].Id, Jobs[Index].Employer.Id);
            //string reviewMsg = review.Aggregate<JobReview, string>(null, (current, r) => current + r.ToString());
            //SideTextBox.Text = reviewMsg;
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

        private void UpdateData(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                Dispatcher.Invoke((() => MainTextBox.Text = ""));
                Dispatcher.Invoke((() => SideTextBox.Text = ""));

                foreach (string msg in MasterSeeder.SeedAll("1155", JobStatus.Posted, new JseLocalRepo().GetAccount(), true, true, "ottawa"))
                {
                    string message = msg + Environment.NewLine;
                    Dispatcher.Invoke((() => MainTextBox.AppendText(message)));
                }
            });
            SetUp();
        }
    }
}