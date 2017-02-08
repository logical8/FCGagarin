using System;
using System.Collections.Generic;
using System.Data.Entity;
using FCGagarin.BLL.Interfaces;
using FCGagarin.PL.ViewModels;

namespace FCGagarin.BLL.Services
{
    public class NewsService : INewsService
    {
        private readonly DbContext _context;

        public NewsService(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<NewsItemViewModel> GetAllNews()
        {
            
            _context.
            throw new NotImplementedException();
        }
    }
}