using FCGagarin.DAL.Entities.Abstract;
using System.Collections.Generic;

namespace FCGagarin.DAL.Entities
{
    public class VideoAlbum : AbstractAlbum
    {
        public ICollection<Video> Videos { get; set; }
    }
}
