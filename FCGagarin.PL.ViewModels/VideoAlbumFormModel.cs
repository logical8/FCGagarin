using System.ComponentModel.DataAnnotations;

namespace FCGagarin.PL.ViewModels
{
    public class VideoAlbumFormModel
    {
        public int Id { get; set; }
        [Display(Name = "Название альбома")]
        public string Name { get; set; }
    }
}