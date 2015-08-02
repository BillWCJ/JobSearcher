using System;
using System.Linq;
using Model.Entities.JobMine;

namespace JobBrowserModule.ViewModels
{
    public class JobPostingViewModel
    {
        public Job Job { get; private set; }

        public bool IsSelected { get; set; }

        public JobPostingViewModel(Job job)
        {
            this.Job = job;
        }


        private string _qualification = string.Empty;
        private int _score;

        public string Qualification
        {
            get
            {
                if (_qualification == string.Empty)
                    _qualification = GetQualification();
                return _qualification ?? Job.JobDescription;
            }
        }

        private string GetQualification()
        {
            string qualification = string.Empty;
            try
            {
                string[] lines = Job.JobDescription.Split('\n');
                char lastBeginCharacter = lines.First()[0];
                bool copy = false;
                for (int i = 1; i < lines.Length; i++)
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
            return qualification == String.Empty ? null : qualification;
        }

        public int Score
        {
            get
            {
                return _score;
            }
        }

        private void GenerateScore()
        {
            _score = 0;
        }
    }
}
