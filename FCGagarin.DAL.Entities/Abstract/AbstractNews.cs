﻿using System;

namespace FCGagarin.DAL.Entities.Abstract
{
    public abstract class AbstractNews : EntityInt
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public int AuthorId { get; set; }
        public UserProfile Author { get; set; }
        public string PathToImage { get; set; }
    }
}
