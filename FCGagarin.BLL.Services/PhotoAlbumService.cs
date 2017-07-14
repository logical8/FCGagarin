using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FCGagarin.BLL.Services.Interfaces;
using FCGagarin.DAL.EF;
using FCGagarin.DAL.Entities;
using FCGagarin.DAL.Repositories.Interfaces;
using System.IO;
using System.Web.Hosting;

namespace FCGagarin.BLL.Services
{
    public class PhotoAlbumService : EntityService<PhotoAlbum>, IPhotoAlbumService
    {
        private readonly IPhotoAlbumRepository _photoAlbumRepository;
        private DbContext _context;

        readonly string _serverMapPath = "~/Files/somefiles/";
        private string StorageRoot => Path.Combine(HostingEnvironment.MapPath(_serverMapPath));

        public PhotoAlbumService(IPhotoAlbumRepository photoAlbumRepository, DbContext context) : base(photoAlbumRepository, context)
        {
            _photoAlbumRepository = photoAlbumRepository;
            _context = context;
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