using System.Data.Entity;
using FCGagarin.DAL.Repositories.Interfaces;

namespace FCGagarin.DAL.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly DbContext _context;

        public NewsRepository(DbContext context)
        {
            _context = context;
        }

        public NewsRepository()
        {
            _context.
        }
    }
}