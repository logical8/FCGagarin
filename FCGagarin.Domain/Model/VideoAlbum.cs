using FCGagarin.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCGagarin.Domain.Model
{
    public class VideoAlbum : AbstractAlbum
    {
        public ICollection<Video> Videos { get; set; }
    }
}
