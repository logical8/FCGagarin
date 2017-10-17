using FCGagarin.DAL.Entities;

namespace FCGagarin.DAL.Repositories.Interfaces
{
    public interface IRoundRepository : IGenericRepository<Round>
    {
        Round GetById(int id);
    }
}