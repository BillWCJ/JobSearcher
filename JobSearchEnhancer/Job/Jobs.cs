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
    public class Employer {
        public string EmployerName { get; set; }
        public uint EmployerID { get; set; }
        //public string UnitName1 { get; set; }
        //public string UnitName2 { get; set; }
        public string EmployerWebSite { get; set; }
        //public string Description { get; set; }

        public Employer()
        {
            EmployerName = string.Empty;
            EmployerID = 0;
            EmployerWebSite = string.Empty;
        }
        public Employer(string name, uint id) {
            EmployerName = name;
            EmployerID = id;
        }
        public Employer(Employer newEmployer)
        {
            EmployerName = newEmployer.EmployerName;
            EmployerID = newEmployer.EmployerID;
            EmployerWebSite = newEmployer.EmployerWebSite;
        }

        public string FullIDString() {return EmployerID.ToString("D8");}
    }

    public class Levels 
    {
        public bool [] IsLevel  {get; private set;}
        public enum Level : int { Junior, Intermediate, Senior, Bachelor, Master, PhD };
        
        public Levels (bool isJunior, bool isIntermediate, bool isSenior) 
        {
            IsLevel = new bool[3];
            IsLevel[(int)Level.Junior] = isJunior;
            IsLevel[(int)Level.Intermediate] = isIntermediate;
            IsLevel[(int)Level.Senior] = isSenior;
        }
        public Levels (bool [] newLevels)
        {
            IsLevel = newLevels;
            if (newLevels.Length != 3)
                throw new ArgumentException("Wrong Number of Levels Passed");
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
        bool[] Discipline {get; set;}
        public Disciplines(bool[] newDisciplines) 
        {
            Discipline = newDisciplines;
        }

        public override string ToString()
        {
            string toString = string.Empty;
            for (int i = 0; i < Discipline.Length; i++)
                if (Discipline[i])
                    toString += GVar.DisciplinesNames[i] + ", ";
            return toString.TrimEnd(',', ' ');
        }
    }

    public class Job : Employer
    {
        public string JobTitle { get; set; }
        public string Location { get; set; }
        //public uint NumberOfOpening { get; set; }
        //public uint NumberOfApplied { get; set; }
        //public bool AlreadyApplied { get; set; }
        //public bool OnShortList { get; set; }
        public uint JobID { get; set; }
        //public string UnitName { get; set; }
        //public DateTime LastDateToApply { get; set; }
        //public DateTime PostingOpenDate { get; set; }
        //public bool GradesRequired { get; set; }
        public Disciplines Disciplines { get; set; }
        public Levels Levels { get; set; }
        //public string HiringProcessSupport { get; set; }
        //public string WorkTermSupport { get; set; }
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
    }
}



