using FCGagarin.DAL.Entities;
using FCGagarin.DAL.Repositories.Interfaces;
using System.Data.Entity;

namespace FCGagarin.DAL.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(DbContext context) : base(context)
        {
        }
    }
}
