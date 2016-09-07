using System;
using System.ComponentModel.DataAnnotations;

namespace FCGagarin.WebUI.ViewModels
{
    public class AnnouncementViewModel
    {
        [Display(Name ="Заголовок")]
        public string Title { get; set; }
        [Display(Name ="Текст")]
        public string Text { get; set; }
        [Display(Name = "Дата создания")]
        private DateTime _createDate;
        public DateTime CreateDate { get { return _createDate; } set { _createDate = DateTime.Now; } }
        [Display(Name = "Автор")]
        public User AnnouncemetCreator { get; set; }
    }
}