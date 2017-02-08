using FCGagarin.DAL.Entities.Abstract;
using System.Collections.Generic;

namespace FCGagarin.DAL.Entities
{
    public class PhotoAlbum : AbstractAlbum
    {
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
