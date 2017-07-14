using FCGagarin.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCGagarin.BLL.Services.Interfaces
{
    public interface IPhotoService : IEntityService<Photo>
    {
        List<Photo> GetPhotosByAlbumId(int albumId);
    }
}
