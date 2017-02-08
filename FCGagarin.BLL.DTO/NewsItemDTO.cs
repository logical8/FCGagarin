using System;

namespace FCGagarin.BLL.DTO
{
    public class NewsItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public int AuthorId { get; set; }
        public UserProfileDTO Author { get; set; }
        public string PathToImage { get; set; }
    }
}
