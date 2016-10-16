using FCGagarin.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FCGagarin.WebUI.ViewModels
{
    public class VideoAlbumViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Название альбома")]
        public string Name { get; set; }
        [Display(Name = "Количество")]
        public int NumberOfVideo { get { return Videos.Count; } }
        [Display(Name = "Дата обновления")]
        public DateTime? LastUploadDate
        {
            get
            {
                if (Videos.Count != 0)
                {
                    return Videos.Select(v => v.UploadDate).Max();
                }
                return null;
            }
        }
        public ICollection<VideoViewModel> Videos { get; set; }
    }
}