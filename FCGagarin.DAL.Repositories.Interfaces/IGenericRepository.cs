using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FCGagarin.DAL.Entities.Abstract;

namespace FCGagarin.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        IEnumerable<T> GetAll();

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        T Add(T entity);

        T Delete(T entity);

        void Edit(T entity);

        void Save();
    }
}