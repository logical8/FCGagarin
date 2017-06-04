using System.Data.Entity;
using FCGagarin.BLL.Services.Interfaces;
using FCGagarin.DAL.Entities;
using FCGagarin.DAL.Repositories.Interfaces;
using FCGagarin.DAL.EF;

namespace FCGagarin.BLL.Services
{
    public class NewsService : EntityService<News>, INewsService
    {
        private INewsRepository _newsRepository;
        private DbContext _context;

        public NewsService(INewsRepository newsRepository, DbContext context) : base (newsRepository, context)
        {
            _newsRepository = newsRepository;
            _context = context;
        }
    }
}