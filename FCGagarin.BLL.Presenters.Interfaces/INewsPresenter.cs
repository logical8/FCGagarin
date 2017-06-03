using FCGagarin.PL.ViewModels;
using System.Collections.Generic;

namespace FCGagarin.BLL.Presenters.Interfaces
{
    public interface INewsPresenter
    {
        IEnumerable<NewsItemViewModel> GetAllNews();
    }
}