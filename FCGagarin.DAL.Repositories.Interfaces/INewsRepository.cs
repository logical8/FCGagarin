using FCGagarin.DAL.Entities;

namespace FCGagarin.DAL.Repositories.Interfaces
{
    public interface INewsRepository : IGenericRepository<News>
    {
        News GetById(int id);
    }
}