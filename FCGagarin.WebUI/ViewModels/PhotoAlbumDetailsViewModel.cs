using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCGagarin.WebUI.ViewModels
{
    public class PhotoAlbumDetailsViewModel
    {
        public PhotoAlbumViewModel PhotoAlbumViewModel { get; set; }
        public List<PhotoViewModel> PhotoViewModelList { get; set; }
    }
}