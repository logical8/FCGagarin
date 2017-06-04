using System.Data.Entity;
using FCGagarin.DAL.Entities;
using FCGagarin.DAL.Repositories.Interfaces;

namespace FCGagarin.DAL.Repositories
{
    public class PhotoAlbumRepository : GenericRepository<PhotoAlbum>, IPhotoAlbumRepository
    {
        public PhotoAlbumRepository(DbContext context) : base(context)
        {
        }
    }
}