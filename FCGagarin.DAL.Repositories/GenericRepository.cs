using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FCGagarin.DAL.Entities.Abstract;
using FCGagarin.DAL.Repositories.Interfaces;

namespace FCGagarin.DAL.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T :BaseEntity
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbSet;

        protected GenericRepository(DbContext context)
        {
            _entities = context;
            _dbSet = context.Set<T>();
        }


        public virtual T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public virtual T Delete(T entity)
        {
            return _dbSet.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = _dbSet.Where(predicate);
            return query;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }
    }
}