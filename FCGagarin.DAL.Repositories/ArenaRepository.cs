using System.Data.Entity;
using FCGagarin.DAL.Entities;
using FCGagarin.DAL.Repositories.Interfaces;

namespace FCGagarin.DAL.Repositories
{
    public class ArenaRepository : GenericRepository<Arena>, IArenaRepository
    {
        public ArenaRepository(DbContext context) : base(context)
        {
        }
    }
}