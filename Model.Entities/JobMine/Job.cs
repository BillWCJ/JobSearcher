using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Definition;

namespace Model.Entities.JobMine
{
    public class JobOverView
    {
        public JobOverView()
        {
            Employer = new Employer();
            JobLocation = new JobLocation();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public virtual Employer Employer { get; set; }

        public virtual JobLocation JobLocation { get; set; }

        public string JobTitle { get; set; }

        public int NumberOfOpening { get; set; }
        public int NumberOfApplied { get; set; }
        public bool AlreadyApplied { get; set; }
        public bool OnShortList { get; set; }
        public DateTime? LastDateToApply { get; set; }

        public string IdString
        {
            get
            {
                return Id.ToString("D8");
            }
        }
    }

    public class Job : JobOverView
    {
        //public DateTime PostingOpenDate { get; set; }
        //public bool GradesRequired { get; set; }
        //public string HiringProcessSupport { get; set; }
        //public string WorkTermSupport { get; set; }

        public Job()
        {
            if (Employer == null || JobLocation == null)
                throw new Exception("One of more of Job's related Entity is null and cannot set relationship");
            Employer.Jobs.Add(this);
            JobLocation.Jobs.Add(this);
            Levels = new Levels();
            Disciplines = new Disciplines();
        }

        [NotMapped]
        public int Score { get; set; }

        public virtual Levels Levels { get; set; }
        public virtual Disciplines Disciplines { get; set; }

        public string Comment { get; set; }
        public string JobDescription { get; set; }

        public string JobUrl
        {
            get
            {
                return JobMineDef.JobDetailBaseUrl + IdString;
            }
        }

        public override string ToString()
        {
            return JobDef.SeperationBar + Environment.NewLine + ToString("F");
        }

        /// <summary>
        ///     Convert into formatted string for displaying and file storing
        /// </summary>
        public string ToString(string format)
        {
            string toString = string.Empty;
            if (format == "F")
            {
                for (int i = 0; i < JobMineDef.JobDetailPageFieldNameTitles.Length; i++)
                {
                    string fieldValue = string.Empty;
                    switch (i)
                    {
                        case 0:
                            fieldValue = Employer.Name;
                            break;
                        case 1:
                            fieldValue = JobTitle;
                            break;
                        case 2:
                            fieldValue = JobLocation.Region;
                            break;
                        case 3:
                            fieldValue = Disciplines.ToString();
                            break;
                        case 4:
                            fieldValue = Levels.ToString();
                            break;
                        case 5:
                            fieldValue = Comment;
                            break;
                        case 6:
                            fieldValue = JobDescription;
                            break;
                        case 7:
                            fieldValue = JobUrl;
                            break;
                    }
                    toString += Environment.NewLine + JobMineDef.JobDetailPageFieldNameTitles[i] + fieldValue + Environment.NewLine;
                }
            }
            else
            {
                toString += Employer.Name + "                    " + JobTitle + "                    " + JobLocation.Region + Environment.NewLine;
                toString += Disciplines + "                    " + Levels + Environment.NewLine;
                toString += "Comment:" + Environment.NewLine + Comment + Environment.NewLine;
                toString += "JobDescription:" + Environment.NewLine + JobDescription + Environment.NewLine + Environment.NewLine;
            }


            return toString;
        }
    }
}