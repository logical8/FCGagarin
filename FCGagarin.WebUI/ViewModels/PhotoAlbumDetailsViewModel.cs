using System.Collections.Generic;

namespace FCGagarin.WebUI.ViewModels
{
    public class PhotoAlbumDetailsViewModel
    {
        public PhotoAlbumViewModel PhotoAlbumViewModel { get; set; }
        public List<PhotoViewModel> PhotoViewModelList { get; set; }
    }
}