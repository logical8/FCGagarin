using System.Collections.Generic;
using FCGagarin.DAL.Entities.Abstract;

namespace FCGagarin.BLL.Services.Interfaces
{
    public interface IEntityService<T> : IService where T : BaseEntity
    {
        void Create(T entity);

        void Delete(T entity);

        IEnumerable<T> GetAll();

        void Update(T entity);
    }
}