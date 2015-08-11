using System;
using System.Linq;
using Business.Manager;
using Model.Definition;
using Model.Entities.JobMine;

namespace JobBrowserModule.ViewModels
{
    public class JobPostingViewModel
    {
        private string _qualification = string.Empty;

        public JobPostingViewModel(Job job)
        {
            Job = job;
            Duration = JobManager.GetTermDuration(Job);
        }

        public Job Job { get; private set; }
        public bool IsSelected { get; set; }
        public TermType Duration { get; private set; }

        public string Qualification
        {
            get
            {
                if (_qualification == string.Empty)
                    _qualification = GetQualification();
                return _qualification ?? Job.JobDescription;
            }
        }

        public int Score { get; private set; }

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

        private void GenerateScore()
        {
            Score = 0;
        }
    }
}