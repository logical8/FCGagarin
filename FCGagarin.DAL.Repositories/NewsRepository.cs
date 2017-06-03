using System.Data.Entity;
using System.Linq;
using FCGagarin.DAL.EF;
using FCGagarin.DAL.Entities;
using FCGagarin.DAL.Repositories.Interfaces;

namespace FCGagarin.DAL.Repositories
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository(FCGagarinContext context) : base(context)
        {
        }


        public News GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }
    }
}