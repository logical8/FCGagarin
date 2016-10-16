using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCGagarin.Domain.Model
{
    public class Video
    {
        public Video()
        {
            UploadDate = DateTime.Now;
        }
        public int Id { get; set; }
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
