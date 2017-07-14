using System.Collections.Generic;

namespace FCGagarin.PL.ViewModels
{
    public class PhotoAlbumDetailsViewModel
    {
        public PhotoAlbumDetailsViewModel()
        {
            PhotoViewModelList = new List<PhotoViewModel>();
        }

        public PhotoAlbumViewModel PhotoAlbumViewModel { get; set; }
        public List<PhotoViewModel> PhotoViewModelList { get; set; }
        public FilesViewModel FilesViewModel { get; set; }
    }
}