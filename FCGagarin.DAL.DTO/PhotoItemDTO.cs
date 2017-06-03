using System;

namespace FCGagarin.DAL.DTO
{
    public class PhotoItemDTO
    {
        public int Id { get; set; }
        public DateTime UploadDate { get; set; }
        public string PathToImage { get; set; }
        public DateTime PhotoDate { get; set; }
        public int AuthorId { get; set; }
        public UserProfileDTO Author { get; set; }
        public int AlbumId { get; set; }
        public virtual PhotoAlbumDTO Album { get; set; }
    }
}
