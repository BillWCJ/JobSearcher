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
            using (var db = new JseDbContext())
            {
                try
                {
                    var newJobToAdd = db.Jobs.FirstOrDefault(j => j.Id == job.Id);
                    if (newJobToAdd == null)
                        return false;

                    var shortList = db.LocalShortLists.FirstOrDefault(s => s.Name.ToLower() == shortListName.ToLower());
                    if (shortList == null)
                    {
                        shortList = new LocalShortList { Name = shortListName };
                        db.LocalShortLists.Add(shortList);
                    }
                    shortList.Jobs.Add(newJobToAdd);
                    return db.SaveChanges() > 0;
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
            }
            return false;
        }

        public static bool RemoveJobFromShortList(int jobId, string shortListName)
        {
            using (var db = new JseDbContext())
            {
                try
                {
                    var jobToRemove = db.Jobs.FirstOrDefault(j => j.Id == jobId);
                    if (jobToRemove == null)
                        return false;

                    var shortList = db.LocalShortLists.FirstOrDefault(s => s.Name.ToLower() == shortListName.ToLower());
                    if (shortList == null)
                    {
                        shortList = new LocalShortList { Name = shortListName };
                        db.LocalShortLists.Add(shortList);
                    }
                    shortList.Jobs.Add(jobToRemove);

                    return db.SaveChanges() > 0;
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
            }
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