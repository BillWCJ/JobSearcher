using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using Data.EF.JseDb;
using Model.Entities;
using Model.Entities.JobMine;

namespace Business.Manager
{
    public class LocalShortListManager
    {
        public static bool AddJobToShortList(Job job, string shortListName)
        {
            //using (var db = new JseDbContext())
            //{
            //    try
            //    {
            //        var shortlist = db.JobShortLists.Include(s => s.Job).FirstOrDefault(j => j.Job.Id == job.Id && j.Data == shortListName);
            //        if (shortlist != null)
            //            return true;
            //        db.JobShortLists.Add(new JobShortList
            //        {
            //            Job = job,
            //            Data = shortListName
            //        });
            //        return db.SaveChanges() == 1;
            //    }
            //    catch (Exception e)
            //    {
            //        Trace.WriteLine(e);
            //    }
            //}
            return false;
        }

        public static bool RemoveJobFromShortList(int jobId, string shortListName)
        {
            //using (var db = new JseDbContext())
            //{
            //    try
            //    {
            //        var shortlist = db.JobShortLists.Include(s => s.Job).FirstOrDefault(j => j.Job.Id == jobId && j.Data == shortListName);
            //        if (shortlist == null)
            //            return true;
            //        db.JobShortLists.Remove(shortlist);
            //        return db.SaveChanges() == 1;
            //    }
            //    catch (Exception e)
            //    {
            //        Trace.WriteLine(e);
            //    }
            //}
            return false;
        }

        public static bool ChangeShortListName(string oldName, string newName)
        {
            //using (var db = new JseDbContext())
            //{
            //    try
            //    {
            //        var shortlists = db.JobShortLists.Where(j => j.Data == oldName);
            //        foreach (var shortList in shortlists)
            //        {
            //            shortList.Data = newName;
            //        }
            //        return db.SaveChanges() > 0;
            //    }
            //    catch (Exception e)
            //    {
            //        Trace.WriteLine(e);
            //    }
            //}
            return false;
        }

        public static string[] GetListOfShortListNames()
        {
            string[] shortlists = null;
            //using (var db = new JseDbContext())
            //{
            //    try
            //    {
            //        shortlists = db.JobShortLists.Select(j => j.Data).Distinct().ToArray();
            //    }
            //    catch (Exception e)
            //    {
            //        Trace.WriteLine(e);
            //    }
            //}
            return shortlists;
        }
    }
}