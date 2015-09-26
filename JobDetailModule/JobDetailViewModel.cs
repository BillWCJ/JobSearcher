using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Business.Manager;
using Common.Utility;
using JobBrowserModule.ViewModels;
using Microsoft.Practices.Prism.PubSubEvents;
using Model.Definition;
using Model.Entities.JobMine;
using Presentation.WPF;
using Presentation.WPF.Events;

namespace JobDetailModule
{
    public class JobDetailViewModel : JobDetailViewModelBase
    {
        private UserAccountManager _userAccountManager;

        public JobDetailViewModel() : base(new EventAggregator())
        { }

        public JobDetailViewModel(EventAggregator aggregator, UserAccountManager userAccountManager) : base(aggregator)
        {
            this._userAccountManager = userAccountManager;
        }

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
            string jobstring = "Id: {0}, title: {1}, employer {2}".FormatString(CurrentJob.Id, CurrentJob.JobTitle, CurrentJob.Employer.Name);
            Task.Factory.StartNew(() =>
            {
                if (JobMineCommunicator.AddToShortList(CurrentJob, _userAccountManager.UserAccount))
                {
                    Aggregator.GetEvent<CurrentStatusMessageChangedEvent>().Publish(CommonDef.CurrentStatus + "Successfully added to JobMine shortList. {0}".FormatString(jobstring));

                }
                else
                {
                    Aggregator.GetEvent<CurrentStatusMessageChangedEvent>().Publish(CommonDef.CurrentStatus + "Fail to Add to JobMine shortList. {0}".FormatString(jobstring));
                }
            });
        }

        public ObservableCollection<string> ShortListNames { get; set; }
    }
}