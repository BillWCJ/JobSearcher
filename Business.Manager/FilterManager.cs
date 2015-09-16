using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.EF.JseDb;
using Model.Definition;
using Model.Entities;

namespace Business.Manager
{
    public class FilterManager
    {
        public static ICollection<Filter> GetFilters()
        {
            using (var db = new JseDbContext())
            {
                return db.Filters.ToList();
            }
        }

        public static void AddFilter(Filter filter)
        {
            using (var db = new JseDbContext())
            {
                db.Filters.Add(filter);
                db.SaveChanges();
            }
        }

        public static void UpdateFilter(Filter filter)
        {
            using (var db = new JseDbContext())
            {
                db.Filters.Attach(filter);
                DbEntityEntry<Filter> entry = db.Entry(filter);
                entry.State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DeleteFilter(Filter filter)
        {
            using (var db = new JseDbContext())
            {
                db.Filters.Attach(filter);
                DbEntityEntry<Filter> entry = db.Entry(filter);
                entry.State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}
