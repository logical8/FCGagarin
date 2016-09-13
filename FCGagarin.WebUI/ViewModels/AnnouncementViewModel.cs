using FCGagarin.Domain.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FCGagarin.WebUI.ViewModels
{
    public class AnnouncementViewModel
    {
        public AnnouncementViewModel()
        {
            CreateDate = DateTime.Now;
        }
        public int Id { get; set; }
        [Display(Name ="Заголовок")]
        public string Title { get; set; }
        [Display(Name ="Текст")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        [Display(Name = "Дата публикации/редактирования")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Автор")]
        public UserProfile AnnouncemetCreator { get; set; }
    }
}