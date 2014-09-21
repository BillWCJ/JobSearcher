using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;
using System.Net;
using System.Windows;
using System.Diagnostics;
using GlobalVariable;

namespace Jobs
{
    //----------Employer----------
    public class Employer 
    {
        public uint EmployerID { get; set; }
        public string EmployerName { get; set; }
        public string UnitName1 { get; set; }
        public string UnitName2 { get; set; }
        public string EmployerWebSite { get; set; }
        public string EmployerDescription { get; set; }
        public string EmployerIDString { get { return EmployerID.ToString("D8"); } }

        public Employer(){}
        public Employer(string name) {EmployerName = name;}
        public Employer(uint id, string name) 
        {
            EmployerID = id;
            EmployerName = name;
        }
        public Employer(uint id, string name, string websiteurl)
        {
            EmployerID = id;
            EmployerName = name;
            EmployerWebSite = websiteurl;
        }
        public Employer(Employer employer) // add get enumerator to get foreach or for loop to set this
        {
            EmployerID = employer.EmployerID;
            EmployerName = employer.EmployerName;
            UnitName1 = employer.UnitName1;
            UnitName2 = employer.UnitName2;
            EmployerWebSite = employer.EmployerWebSite;
            EmployerDescription = employer.EmployerDescription;
        }
    }

    //----------Levels----------
    public class Levels 
    {
        public bool [] IsLevel  {get; set;}
        public Levels() { IsLevel = new bool[GVar.LevelNames.Length]; }
        public Levels(bool[] newLevels) { IsLevel = newLevels; }
        public Levels(bool isJunior, bool isIntermediate, bool isSenior) : this() //calls public Levels()
        {
            IsLevel[(int)GVar.Level.Junior] = isJunior;
            IsLevel[(int)GVar.Level.Intermediate] = isIntermediate;
            IsLevel[(int)GVar.Level.Senior] = isSenior;
        }
        public Levels(string levelString) : this() //calls public Levels()
        {
            try
            {
                for (int i = 0; i < IsLevel.Length; i++)
			    {
                    IsLevel[i] = IsAtLevel(GVar.LevelNames[i], levelString);
			    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static bool IsAtLevel(string level, string levelString)
        {
            return levelString.IndexOf(level) > -1;
        }

        public override string ToString()
        {
            string toString = string.Empty;
            for (int i = 0; i < IsLevel.Length; i++)
                if (IsLevel[i])
                    toString += GVar.LevelNames[i] + ", ";
            return toString.TrimEnd(',',' ');
        }
    }

    public class Disciplines
    {
        bool[] Discipline { get; set; }

        public Disciplines() { Discipline = new bool[GVar.DisciplinesNames.Length]; }
        public Disciplines(string data) : this() // Callspublic Disciplines()
        {
            for (int i = 0; i < Discipline.Length; i++)
            {
                Discipline[i] = data.IndexOf(GVar.DisciplinesNames[i]) > -1;
            }
        }

        public Disciplines(bool[] newDisciplines) 
        {
            Discipline = newDisciplines;
        }
        public bool Get(uint index) { return Discipline[index]; }
        public override string ToString()
        {
            string toString = string.Empty;
            try
            {
                for (int i = 0; i < Discipline.Length; i++)
                    if (Discipline[i])
                        toString += GVar.DisciplinesNames[i] + ", ";
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_{1}: {2}\n", e.GetType().ToString(),this.GetType().ToString(),e.Message);
            }
            return toString.TrimEnd(',', ' ');
        }
    }

    public class Job : Employer
    {
        public string JobTitle { get; set; }
        public string Location { get; set; }
        public uint NumberOfOpening { get; set; }
        public uint NumberOfApplied { get; set; }
        public bool AlreadyApplied { get; set; }
        public bool OnShortList { get; set; }
        public uint JobID { get; set; }
        public string UnitName { get; set; }
        public DateTime LastDateToApply { get; set; }
        public DateTime PostingOpenDate { get; set; }
        public bool GradesRequired { get; set; }
        public Disciplines Disciplines { get; set; }
        public Levels Levels { get; set; }
        public string HiringProcessSupport { get; set; }
        public string WorkTermSupport { get; set; }
        public string Comment { get; set; }
        public string JobDescription { get; set; }
        public string JobUrl { get; set; }

        public Job () { ;} 
        public Job (Employer newEmployer) 
        {
            EmployerName = newEmployer.EmployerName;
            EmployerID = newEmployer.EmployerID;
            EmployerWebSite = newEmployer.EmployerWebSite;
        }

        public Job (string[] fields)
        {
            EmployerName = fields[0];
            JobTitle = fields[1];
            Location = fields[2];
            Disciplines = new Disciplines(fields[3]);
            Levels = new Levels(fields[4]); ;
            Comment = fields[5];
            JobDescription = fields[6];
            JobUrl = fields[7];
        }

        public override string ToString()
        {
            return GVar.SeperationBar + System.Environment.NewLine + ToString("F");
        }
        /// <summary>
        /// Convert into formatted string for displaying and file storing
        /// </summary>
        public string ToString(string format)
        {
            string toString = string.Empty;
            for (int i = 0; i < GVar.JobDetailPageFieldNameTitles.Length; i++)
            {
                string fieldValue = string.Empty;
                switch (i)
                {
                    case 0: fieldValue = EmployerName;
                        break;
                    case 1: fieldValue = JobTitle;
                        break;
                    case 2: fieldValue = Location;
                        break;
                    case 3: fieldValue = Disciplines.ToString();
                        break;
                    case 4: fieldValue = Levels.ToString();
                        break;
                    case 5: fieldValue = Comment;
                        break;
                    case 6: fieldValue = JobDescription;
                        break;
                    case 7: fieldValue = JobUrl;
                        break;
                    default: ;
                        break;
                }
                toString += Environment.NewLine + GVar.JobDetailPageFieldNameTitles[i] + fieldValue + Environment.NewLine;
            }
            return toString;
        }
    }
}



