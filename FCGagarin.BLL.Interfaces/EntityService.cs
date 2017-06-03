using System;
using System.Collections.Generic;
using FCGagarin.DAL.EF;
using FCGagarin.DAL.Entities.Abstract;
using FCGagarin.DAL.Repositories.Interfaces;

namespace FCGagarin.BLL.Services.Interfaces
{
    public abstract class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        private readonly IContext _context;
        private readonly IGenericRepository<T> _repository;

        protected EntityService(IGenericRepository<T> repository, IContext context)
        {
            _repository = repository;
            _context = context;
        }

        public void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Add(entity);
            _context.Save();
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Delete(entity);
            _context.Save();
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Edit(entity);
            _context.Save();
        }
    }
}