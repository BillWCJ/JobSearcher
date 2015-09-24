using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using Data.Contract.JseDb;
using Data.Contract.JseDb.Interface;

namespace Data.EF.JseDb.Repository
{
    public class BaseRepository<TClass> : IBaseRepository<TClass>
        where TClass : class
    {
        public BaseRepository(IJseDbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            DbContext = dbContext;
            DbSet = DbContext.Set<TClass>();
        }

        protected IJseDbContext DbContext { get; set; }
        protected DbSet<TClass> DbSet { get; set; }

        public virtual IQueryable<TClass> GetAll()
        {
            try
            {
                return DbSet;
            }
            catch (Exception e)
            {
                string msg = typeof (TClass) + " " + this.GetType() + "::GetAll() : \n" + e;
                Trace.TraceError(msg);
                throw new DataException(msg, e);
            }
        }

        public virtual TClass GetById(object id)
        {
            try
            {
                return DbSet.Find(id);
            }
            catch (Exception e)
            {
                string msg = typeof (TClass) + " " + this.GetType() + "::GetById(" + id.GetType() + " " + id + ") : \n" + e;
                Trace.TraceError(msg);
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
                string msg = "void " + this.GetType() + "::Add(" + typeof(TClass) + " " + entity + ") : \n" + e;
                Trace.TraceError(msg);
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
                string msg = "void " + this.GetType() + "::Update(" + typeof(TClass) + " " + entity + ") : \n" + e;
                Trace.TraceError(msg);
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
                string msg = "void " + this.GetType() + "::Delete(" + typeof(TClass) + " " + entity + ") : \n" + e;
                Trace.TraceError(msg);
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
                string msg = "void " + this.GetType() + "::Delete(" + id.GetType() + " " + id + ") : \n" + e;
                Trace.TraceError(msg);
                throw new DataException(msg, e);
            }
        }
    }
}