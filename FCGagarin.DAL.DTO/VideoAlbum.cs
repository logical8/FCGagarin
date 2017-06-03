using System.Collections.Generic;

namespace FCGagarin.DAL.DTO
{
    public class VideoAlbumDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<VideoItemDTO> VideoItems { get; set; }
    }
}
