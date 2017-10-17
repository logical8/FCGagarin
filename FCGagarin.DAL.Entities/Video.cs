using System;
using System.ComponentModel.DataAnnotations;
using FCGagarin.DAL.Entities.Abstract;

namespace FCGagarin.DAL.Entities
{
    public class Video : EntityInt
    {
        public Video()
        {
            UploadDate = DateTime.Now;
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
        public DateTime UploadDate { get; set; }
        [Required]
        public DateTime VideoDate { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public UserProfile Author { get; set; }
        [Required]
        public int AlbumId { get; set; }
        public virtual VideoAlbum Album { get; set; }
    }
}
