using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCGagarin.WebUI.ViewModels
{
    public class VideoAlbumDetailsViewModel
    {
        public VideoAlbumViewModel VideoAlbumViewModel { get; set; }
        public List<VideoViewModel> VideoViewModelList { get; set; }
    }
}