using System.Collections.Generic;

namespace FCGagarin.BLL.DTO
{
    public class VideoAlbumDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<VideoItemDTO> VideoItems { get; set; }
    }
}
