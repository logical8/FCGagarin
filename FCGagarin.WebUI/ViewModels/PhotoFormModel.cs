using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FCGagarin.WebUI.ViewModels
{
    public class PhotoFormModel
    {
        public PhotoFormModel()
        {
            PhotoDate = DateTime.Now.Date;
            UploadDate = DateTime.Now.Date;
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Название фото")]
        public string Name { get; set; }
        [Required]
        public string PathToImage { get; set; }
        [Display(Name = "Дата фото")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PhotoDate { get; set; }
        [HiddenInput]
        public DateTime UploadDate { get; set; }
        [HiddenInput]
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        [HiddenInput]
        public int AuthorId { get; set; }
    }
}