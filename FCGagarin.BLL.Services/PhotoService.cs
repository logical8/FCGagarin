using FCGagarin.BLL.Services;
using FCGagarin.BLL.Services.Interfaces;
using FCGagarin.DAL.Entities;
using FCGagarin.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCGagarin.BLL.Services
{
    public class PhotoService : EntityService<Photo>, IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;
        private DbContext _context;

        public PhotoService(IPhotoRepository photoRepository, DbContext context) : base(photoRepository, context)
        {
            _photoRepository = photoRepository;
            _context = context;
        }

        public List<Photo> GetPhotosByAlbumId(int albumId)
        {
            throw new NotImplementedException();
        }

        public Photo GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
