using System.Data.Entity;
using FCGagarin.DAL.Entities;
using FCGagarin.DAL.Repositories.Interfaces;

namespace FCGagarin.DAL.Repositories
{
    public class RoundRepository : GenericRepository<Round>, IRoundRepository
    {
        public RoundRepository(DbContext context) : base(context)
        {
        }

        public Round GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}