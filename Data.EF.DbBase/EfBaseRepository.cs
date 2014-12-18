using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using ConnCar.Data.Contracts;
using ConnCar.Common.Cryptography;
using ConnCar.Business.Core.Exceptions;
using LogItLogger;

namespace ConnCar.Data.EF.BaseRepoDB.Repositories
{
    /// <summary>
    /// The EF-dependent, generic repository for data access
    /// </summary>
    /// <typeparam name="T">Type of entity for this Repository.</typeparam>
    /// <typeparam name="TCtx"> </typeparam>
    public class EfBaseRepository<TCtx, T> : IRepository<T> where TCtx : DbContext where T : class
    {
        private readonly ILogIt _logger = LogItManager.GetCurrentClassLogger();

        public EfBaseRepository(TCtx dbContext, IPiiCryptography crypto = null)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            
            DbContext = dbContext;
            Crypto = crypto;

            DbSet = DbContext.Set<T>();
        }

        protected IPiiCryptography Crypto { get; set; }

        protected TCtx DbContext { get; set; }

        protected DbSet<T> DbSet { get; set; }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public virtual T GetById(object id)
        {
            try
            {
                //return DbSet.FirstOrDefault(PredicateBuilder.GetByIdPredicate<T>(id));
                return DbSet.Find(id);
            }
            catch (InvalidOperationException e)
            {
                _logger.ErrorFormat("EfBaseRepository.GetById(id = {0})", e, id.ToString());
                throw new DelphiInfrastuctureException("EfBaseRepository.GetById", e);
            }
        }

        public virtual void Add(T entity)
        {
            if (entity == null)
                return;

            //check for existing DbEntityEntry removed for performance reasons, this extra check is too expensive
            //calling code is responsible for not calling Add() multiple times with the same entry
            DbSet.Add(entity);
        }

        public virtual void Update(T entity)
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

        public virtual void Delete(T entity)
        {
            if (entity == null)
                return;

            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry == null)
                return;

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public virtual void Delete(object id)
        {
            var entity = GetById(id);
            if (entity == null) 
                return; // not found; assume already deleted.

            Delete(entity);
        }
    }
}
