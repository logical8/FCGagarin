using System;
using System.ComponentModel.DataAnnotations;

namespace FCGagarin.WebUI.ViewModels
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Название фото")]
        public string Name { get; set; }
        public string PathToImage { get; set; }
        [Display(Name = "Дата обновления")]
        public DateTime UploadDate { get; set; }
        [Display(Name = "Дата фото")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PhotoDate { get; set; }
        [Display(Name = "Автор")]
        public string Author { get; set; }
        public int AlbumId { get; set; }
    }
}