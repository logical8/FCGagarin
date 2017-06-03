using FCGagarin.DAL.Entities;

namespace FCGagarin.BLL.Services.Interfaces
{
    public interface IPhotoAlbumService : IEntityService<PhotoAlbum>
    {
        PhotoAlbum GetById(int id);
    }
}