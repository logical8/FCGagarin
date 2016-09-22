using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCGagarin.Domain.Model
{
    public class PhotoAlbum : Abstract.AbstractAlbum
    {
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
