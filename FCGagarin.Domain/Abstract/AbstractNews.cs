using FCGagarin.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCGagarin.Domain.Abstract
{
    public abstract class AbstractNews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public int AuthorId { get; set; }
        public UserProfile Author { get; set; }
        public string PathToImage { get; set; }
    }
}
