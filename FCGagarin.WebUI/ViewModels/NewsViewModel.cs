﻿using FCGagarin.Domain.Model;
using FCGagarin.WebUI.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCGagarin.WebUI.ViewModels
{
    public class NewsViewModel
    {
        public NewsViewModel()
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
        [Display(Name = "Автор")]
        public string Author { get; set; }
        public string PathToImage { get; set; }
        
    }
}