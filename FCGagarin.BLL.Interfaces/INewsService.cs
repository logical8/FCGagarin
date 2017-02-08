using FCGagarin.PL.ViewModels;
using System.Collections.Generic;

namespace FCGagarin.BLL.Interfaces
{
    public interface INewsService
    {
        IEnumerable<NewsItemViewModel> GetAllNews();
    }
}