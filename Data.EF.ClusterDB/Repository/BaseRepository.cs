using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using Data.EF.ClusterDB.Interface;

namespace Data.EF.ClusterDB.Repository
{
    public class BaseRepository<TClass> : IBaseRepository<TClass> where TClass : class
    {
        public BaseRepository(DatabaseContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            DbContext = dbContext;
            DbSet = DbContext.Set<TClass>();
        }

        protected DatabaseContext DbContext { get; set; }
        protected DbSet<TClass> DbSet { get; set; }

        public virtual IQueryable<TClass> GetAll()
        {
            try
            {
                return DbSet;
            }
            catch (Exception e)
            {
                string msg = e.GetType() + " : " + e.Message + " at " + GetType().GetMethods();
                Trace.WriteLine(msg);
                throw new DataException(msg, e);
            }
        }

        public virtual TClass GetById(object id)
        {
            try
            {
                return DbSet.Find(id);
            }
            catch (InvalidOperationException e)
            {
                string msg = e.GetType() + " : " + e.Message + " at " + GetType().GetMethods() + " : id =" + id;
                Trace.WriteLine(msg);
                throw new DataException(msg, e);
            }
            catch (Exception e)
            {
                string msg = e.GetType() + " : " + e.Message + " at " + GetType().GetMethods() + " : id =" + id;
                Trace.WriteLine(msg);
                throw new DataException(msg, e);
            }
        }

        public virtual void Add(TClass entity)
        {
            try
            {
                if (entity == null)
                    return;

                DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
                if (dbEntityEntry == null)
                    return;

                if (dbEntityEntry.State != EntityState.Detached)
                    dbEntityEntry.State = EntityState.Added;
                else
                    DbSet.Add(entity);
            }
            catch (Exception e)
            {
                string msg = e.GetType() + " : " + e.Message + " at " + GetType().GetMethods() + " : " +
                             (DbContext == null ? "Null" : DbContext.GetType().ToString());
                Trace.WriteLine(msg);
                throw new DataException(msg, e);
            }
        }

        public virtual void Update(TClass entity)
        {
            try
            {
                if (entity == null)
                    return;

                DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
                if (dbEntityEntry == null)
                    return;

                if (dbEntityEntry.State == EntityState.Detached)
                    DbSet.Attach(entity);

                dbEntityEntry.State = EntityState.Modified;
            }
            catch (Exception e)
            {
                string msg = e.GetType() + " : " + e.Message + " at " + GetType().GetMethods() + " : " +
                             (DbContext == null ? "Null" : DbContext.GetType().ToString());
                Trace.WriteLine(msg);
                throw new DataException(msg, e);
            }
        }

        public virtual void Delete(TClass entity)
        {
            try
            {
                if (entity == null)
                    return;

                DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
                if (dbEntityEntry == null)
                    return;

                if (dbEntityEntry.State != EntityState.Deleted)
                    dbEntityEntry.State = EntityState.Deleted;
                else
                {
                    DbSet.Attach(entity);
                    DbSet.Remove(entity);
                }
            }
            catch (Exception e)
            {
                string msg = e.GetType() + " : " + e.Message + " at " + GetType().GetMethods() + " : " +
                             (DbContext == null ? "Null" : DbContext.GetType().ToString());
                Trace.WriteLine(msg);
                throw new DataException(msg, e);
            }
        }

        public virtual void Delete(object id)
        {
            try
            {
                TClass entity = GetById(id);
                if (entity == null)
                    return; // not found; assume already deleted.

                Delete(entity);
            }
            catch (Exception e)
            {
                string msg = e.GetType() + " : " + e.Message + " at " + GetType().GetMethods() + " : id =" + id;
                Trace.WriteLine(msg);
                throw new DataException(msg, e);
            }
        }
    }
}