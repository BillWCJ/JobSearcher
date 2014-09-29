using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GlobalVariable;

namespace Model.Entities
{
    public class JobOverView
    {
        public JobOverView()
        {
            Employer = new Employer();
            Location = new Location();
        }

        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Column(Order = 1)]
        public int JobMineId { get; set; }

        [Column(Order = 2)]
        public string JobTitle { get; set; }

        //[Column(Order = 3)]
        //[Key, ForeignKey("Employer")]
        //public int EmployerId { get; set; }

        public virtual Employer Employer { get; set; }

        //[Column(Order = 4)]
        //[Key, ForeignKey("Location")]
        //public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        //public uint NumberOfOpening { get; set; }
        //public uint NumberOfApplied { get; set; }
        //public bool AlreadyApplied { get; set; }
        //public bool OnShortList { get; set; }
        //public DateTime LastDateToApply { get; set; }
        public string IdString
        {
            get { return JobMineId.ToString("D8"); }
        }
    }

    //[Table("Jobs")]
    public class Job : JobOverView
    {
        //public DateTime PostingOpenDate { get; set; }
        //public bool GradesRequired { get; set; }
        //public string HiringProcessSupport { get; set; }
        //public string WorkTermSupport { get; set; }

        public Job()
        {
            Levels = new Levels();
            Disciplines = new Disciplines();
            SetRelationship();
        }

        public Job(string[] fields)
        {
            Employer.Name = fields[0];
            JobTitle = fields[1];
            Location.Region = fields[2];
            Disciplines = new Disciplines(fields[3]);
            Levels = new Levels(fields[4]);
            Comment = fields[5];
            JobDescription = fields[6];
            JobMineId = Convert.ToInt32(fields[7]);
            SetRelationship();
        }

        public virtual Levels Levels { get; set; }
        public virtual Disciplines Disciplines { get; set; }

        public string Comment { get; set; }
        public string JobDescription { get; set; }

        public string JobUrl
        {
            get { return GVar.JobDetailBaseUrl + IdString; }
        }

        private void SetRelationship()
        {
            Employer.Jobs.Add(this);
            Location.Jobs.Add(this);
            Levels.Job = this;
            Disciplines.Job = this;
        }

        public override string ToString()
        {
            return GVar.SeperationBar + Environment.NewLine + ToString("F");
        }

        /// <summary>
        ///     Convert into formatted string for displaying and file storing
        /// </summary>
        public string ToString(string format)
        {
            string toString = string.Empty;
            for (int i = 0; i < GVar.JobDetailPageFieldNameTitles.Length; i++)
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
                        fieldValue = Location.Region;
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
                    default:
                        ;
                        break;
                }
                toString += Environment.NewLine + GVar.JobDetailPageFieldNameTitles[i] + fieldValue +
                            Environment.NewLine;
            }
            return toString;
        }
    }
}