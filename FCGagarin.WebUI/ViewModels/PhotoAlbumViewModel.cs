﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FCGagarin.WebUI.ViewModels
{
    public class PhotoAlbumViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Название альбома")]
        public string Name { get; set; }
        [Display(Name = "Количество")]
        public int NumberOfPhoto { get { return Photos.Count; } }
        [Display(Name = "Дата обновления")]
        public DateTime? LastUploadDate
        {
            get
            {
                if (Photos.Count != 0)
                {
                    return Photos.Select(v => v.UploadDate).Max();
                }
                return null;
            }
        }
        public ICollection<PhotoViewModel> Photos { get; set; }
    }
}