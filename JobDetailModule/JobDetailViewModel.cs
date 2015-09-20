using System;
using System.Collections.ObjectModel;
using Business.Manager;
using JobBrowserModule.ViewModels;
using Microsoft.Practices.Prism.PubSubEvents;
using Model.Entities.JobMine;
using Presentation.WPF;
using Presentation.WPF.Events;

namespace JobDetailModule
{
    public class JobDetailViewModel : JobDetailViewModelBase
    {
        public JobDetailViewModel() : base(new EventAggregator())
        { }

        public JobDetailViewModel(EventAggregator aggregator) : base (aggregator)
        { }

        public string CurrentJobString
        {
            get
            {
                return PlainTextToRtf(CurrentJob != null ? CurrentJob.ToString("") : "No Job Selected");
            }
        }
        public static string PlainTextToRtf(string plainText)
        {
            string escapedPlainText = plainText.Replace(@"\", @"\\").Replace("{", @"\{").Replace("}", @"\}");
            string rtf = @"{\rtf1\ansi{\fonttbl\f0\fswiss Helvetica;}\f0\pard ";
            rtf += escapedPlainText.Replace(Environment.NewLine, @" \par ");
            rtf += " }";
            return rtf;
        }

        protected override void NotifyPropertyChanged()
        {
            OnPropertyChanged("CurrentJobString");
        }

        //public void AddSelectedJobToShortList(string name)
        //{
        //    if (LocalShortListManager.AddJobToShortList(CurrentJob, name))
        //    {
        //        if (!ShortListNames.Contains(name))
        //            ShortListNames.Add(name);
        //    }
        //}

        public void AddCurrentJobToJobMineShortList()
        {
            JobMineCommunicator.AddToShortList(CurrentJob);
        }

        public ObservableCollection<string> ShortListNames { get; set; }
    }
}