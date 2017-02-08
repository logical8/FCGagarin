using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FCGagarin.WebUI.ViewModels
{
    public class VideoFormModel
    {
        public VideoFormModel()
        {
            VideoDate = DateTime.Now.Date;
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Название видео")]
        public string Name { get; set; }
        [Display(Name = "Ссылка на видео (youtube)")]
        [Required]
        public string Url { get; set; }
        [Display(Name = "Дата видео")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VideoDate { get; set; }
        [HiddenInput]
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        [HiddenInput]
        public int AuthorId { get; set; }
    }
}