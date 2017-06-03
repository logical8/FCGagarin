using System.Collections.Generic;
using System.Linq;
using FCGagarin.BLL.Services.Interfaces;
using FCGagarin.DAL.EF;
using FCGagarin.DAL.Entities;
using FCGagarin.DAL.Repositories.Interfaces;

namespace FCGagarin.BLL.Services
{
    public class PhotoAlbumService : EntityService<PhotoAlbum>, IPhotoAlbumService
    {
        private readonly IPhotoAlbumRepository _photoAlbumRepository;
        private IContext _context;

        public PhotoAlbumService(IPhotoAlbumRepository photoAlbumRepository, IContext context) : base(photoAlbumRepository, context)
        {
            _photoAlbumRepository = photoAlbumRepository;
            _context = context;
        }

        public PhotoAlbum GetById(int id)
        {
            return _photoAlbumRepository.FindBy(x => x.Id == id).FirstOrDefault();
        }
    }
}