using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FCGagarin.WebUI.ViewModels
{
    public class PhotoAlbumFormModel
    {
        public int Id { get; set; }
        [Display(Name = "Название альбома")]
        public string Name { get; set; }
    }
}