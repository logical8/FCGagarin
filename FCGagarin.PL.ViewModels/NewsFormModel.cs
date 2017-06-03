using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using FCGagarin.BLL.Infrastructure.Validators;

namespace FCGagarin.PL.ViewModels
{
    public class NewsFormModel
    {
        public NewsFormModel()
        {
            CreateDate = DateTime.Now;
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Display(Name = "Текст")]
        [AllowHtml]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        [Display(Name = "Дата публикации/редактирования")]
        public DateTime CreateDate { get; set; }
        [HiddenInput]
        public int AuthorId { get; set; }
        [FileSize(2097152)] //2мб
        [FileTypes("jpg,jpeg,png")]
        [Display(Name = "Изображение")]
        public HttpPostedFileBase Image { get; set; }

    }
}