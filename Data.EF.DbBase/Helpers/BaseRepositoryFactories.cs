using System;
using System.Collections.Generic;
using System.Data.Entity;
using ConnCar.Data.Contracts;
using ConnCar.Data.EF.BaseRepoDB.Repositories;

namespace ConnCar.Data.EF.BaseRepoDB.Helpers
{
    /// <summary>
    /// A maker of OBU Repositories.
    /// </summary>
    /// <remarks>
    /// An instance of this class contains repository factory functions for different types.
    /// Each factory function takes an EF <see cref="DbContext"/> and returns
    /// a repository bound to that DbContext.
    /// <para>
    /// Designed to be a "Singleton", configured at web application start with
    /// all of the factory functions needed to create any type of repository.
    /// Should be thread-safe to use because it is configured at app start,
    /// before any request for a factory, and should be immutable thereafter.
    /// </para>
    /// </remarks>
    public abstract class BaseRepositoryFactories<TCtx> where TCtx : DbContext
    {
        protected BaseRepositoryFactories()
        {}

        /// <summary>
        /// Constructor that initializes with an arbitrary collection of factories
        /// </summary>
        /// <param name="factories">
        /// The repository factory functions for this instance. 
        /// </param>
        /// <remarks>
        /// This ctor is primarily useful for testing this class
        /// </remarks>
        protected BaseRepositoryFactories(IDictionary<Type, Func<TCtx, object>> factories)
        {
            RepositoryFactories = factories;
        }
        
        /// <summary>
        /// Get the repository factory function for the type.
        /// </summary>
        /// <typeparam name="T">Type serving as the repository factory lookup key.</typeparam>
        /// <returns>The repository function if found, else null.</returns>
        /// <remarks>
        /// The type parameter, T, is typically the repository type 
        /// but could be any type (e.g., an entity type)
        /// </remarks>
        public Func<TCtx, object> GetRepositoryFactory<T>()
        {
            Func<TCtx, object> factory;
            RepositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        /// <summary>
        /// Get the factory for <see cref="IRepository{T}"/> where T is an entity type.
        /// </summary>
        /// <typeparam name="T">The root type of the repository, typically an entity type.</typeparam>
        /// <returns>
        /// A factory that creates the <see cref="IRepository{T}"/>, given an EF <see cref="RepositoryFactories"/>.
        /// </returns>
        /// <remarks>
        /// Looks first for a custom factory in <see cref="DefaultEntityRepositoryFactory{T}"/>.
        /// If not, falls back to the <see cref="RepositoryFactories"/>.
        /// You can substitute an alternative factory for the default one by adding
        /// a repository factory for type "T" to <see cref="IRepository{T}"/>.
        /// </remarks>
        public Func<TCtx, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        /// <summary>
        /// Default factory for a <see cref="IRepository{T}"/> where T is an entity.
        /// </summary>
        /// <typeparam name="T">Type of the repository's root entity</typeparam>
        protected virtual Func<TCtx, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            return dbContext => new EfBaseRepository<TCtx, T>(dbContext);
        }

        /// <summary>
        /// Get the dictionary of repository factory functions.
        /// </summary>
        /// <remarks>
        /// A dictionary key is a System.Type, typically a repository type.
        /// A value is a repository factory function
        /// that takes a <see cref="TCtx"/> argument and returns
        /// a repository object. Caller must know how to cast it.
        /// </remarks>
        protected abstract IDictionary<Type, Func<TCtx, object>> RepositoryFactories { get; set; }

    }
}
