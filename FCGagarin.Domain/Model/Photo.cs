using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCGagarin.Domain.Model
{
    public class Photo
    {
        public int Id { get; set; }
        public DateTime UploadDate { get; set; }
        public string PathToImage { get; set; }
        public DateTime PhotoDate { get; set; }
        public int AuthorId { get; set; }
        public UserProfile Author { get; set; }
        public int AlbumId { get; set; }
        public virtual PhotoAlbum Album { get; set; }
    }
}
