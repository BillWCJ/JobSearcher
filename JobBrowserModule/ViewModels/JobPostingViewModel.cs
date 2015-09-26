using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Business.Manager;
using Common.Utility;
using Model.Definition;
using Model.Entities.JobMine;
using Model.Entities.RateMyCoopJob;

namespace JobBrowserModule.ViewModels
{
    public class JobPostingViewModel
    {
        private string _qualification = string.Empty;

        public JobPostingViewModel(Job job)
        {
            Job = job;
            Duration = JobManager.GetTermDuration(Job).GetDescription();
            //EmployerReviews = JobReviewManager.GetEmployerReview(this.Job.Employer.Name);
        }

        public List<EmployerReview> EmployerReviews { get; set; }

        public Job Job { get; private set; }
        public bool IsSelected { get; set; }
        public string Duration { get; private set; }

        public string Qualification
        {
            get
            {
                if (_qualification == string.Empty)
                    _qualification = GetQualification();
                return _qualification ?? Job.JobDescription;
            }
        }

        public int Score { get; set; }

        private string GetQualification()
        {
            var qualification = string.Empty;
            try
            {
                var lines = Job.JobDescription.Split('\n');
                var lastBeginCharacter = lines.First()[0];
                var copy = false;
                for (var i = 1; i < lines.Length; i++)
                {
                    if (copy)
                    {
                        if (!string.IsNullOrWhiteSpace(lines[i]) && lines[i] != "")
                        {
                            copy = false;
                            continue;
                        }

                        qualification += lines[i] + Environment.NewLine;
                    }
                    if (lines[i][0] == lastBeginCharacter)
                    {
                        qualification += lines[i - 1] + Environment.NewLine + lines[i] + Environment.NewLine;
                        copy = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return qualification == string.Empty ? null : qualification;
        }
        
        public string Details
        {
            get
            {
                return Job.Comment.IsNullSpaceOrEmpty() ? "No comments..." : Job.Comment;
            }
        }

        private string _shortString = null;

        public string ShortString
        {
            get
            {
                return _shortString ?? (_shortString = GetShortString());
            }
        }

        private string GetShortString()
        {
            string shortString = string.Empty;
            try
            {
                if (Job.Levels.IsJunior) shortString += "[J]";
                if (Job.Levels.IsIntermediate) shortString += "[I]";
                if (Job.Levels.IsSenior) shortString += "[S]";
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                shortString = "[???]";
            }
            return shortString;
        }
    }
}