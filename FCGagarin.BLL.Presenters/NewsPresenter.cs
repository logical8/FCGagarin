using System;
using System.Collections.Generic;
using FCGagarin.BLL.Presenters.Interfaces;
using FCGagarin.PL.ViewModels;

namespace FCGagarin.BLL.Presenters
{
    public class NewsPresenter : INewsPresenter
    {
        public IEnumerable<NewsItemViewModel> GetAllNews()
        {
            throw new NotImplementedException();
        }
    }
}