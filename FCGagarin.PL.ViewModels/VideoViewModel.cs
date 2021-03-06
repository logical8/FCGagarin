﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FCGagarin.PL.ViewModels
{
    public class VideoViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Название видео")]
        public string Name { get; set; }
        [Display(Name = "Ссылка на видео (youtube)")]
        public string Url { get; set; }
        [Display(Name = "Дата обновления")]
        public DateTime UploadDate { get; set; }
        [Display(Name = "Дата видео")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VideoDate { get; set; }
        [Display(Name = "Автор")]
        public string Author { get; set; }
        public int AlbumId { get; set; }
    }
}