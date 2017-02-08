using System;

namespace FCGagarin.BLL.DTO
{
    public class VideoItemDTO
    {
        public VideoItemDTO()
        {
            UploadDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime VideoDate { get; set; }
        public int AuthorId { get; set; }
        public virtual UserProfileDTO Author { get; set; }
        public int AlbumId { get; set; }
        public virtual VideoAlbumDTO Album { get; set; }
    }
}
