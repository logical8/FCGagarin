using System.Collections.Generic;

namespace FCGagarin.BLL.DTO
{
    public class PhotoAlbumDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PhotoItemDTO> PhotoItems { get; set; }
    }
}
