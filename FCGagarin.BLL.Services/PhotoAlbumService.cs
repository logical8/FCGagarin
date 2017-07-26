using System.Data.Entity;
using System.Linq;
using FCGagarin.BLL.Services.Interfaces;
using FCGagarin.DAL.Entities;
using FCGagarin.DAL.Repositories.Interfaces;
using System.IO;
using System.Web.Hosting;

namespace FCGagarin.BLL.Services
{
    public class PhotoAlbumService : EntityService<PhotoAlbum>, IPhotoAlbumService
    {
        private readonly IPhotoAlbumRepository _photoAlbumRepository;

        readonly string _serverMapPath = "~/Data/uploads/albums/";
        private string StorageRoot => Path.Combine(HostingEnvironment.MapPath(_serverMapPath));

        public PhotoAlbumService(IPhotoAlbumRepository photoAlbumRepository, DbContext context) : base(photoAlbumRepository, context)
        {
            _photoAlbumRepository = photoAlbumRepository;
        }

        public PhotoAlbum GetById(int id)
        {
            return _photoAlbumRepository.FindBy(x => x.Id == id).FirstOrDefault();
        }

        public string GetFullPathToAlbum(int albumId)
        {
            var album = GetById(albumId);
            return Path.Combine(StorageRoot, album.Id.ToString());
        }
    }
}