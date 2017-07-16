using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FCGagarin.PL.ViewModels
{
    public class PhotoAlbumViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название альбома")]
        public string Name { get; set; }

        [Display(Name = "Количество")]
        public int NumberOfPhoto { get; set; }

        [Display(Name = "Дата обновления")]
        public DateTime? LastUploadDate { get; set; }
    }
}