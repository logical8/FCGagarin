using FCGagarin.DAL.Entities.Abstract;

namespace FCGagarin.DAL.Entities
{
    public class Score : EntityInt
    {
        public int HomeResult { get; set; }
        public int GuestResult { get; set; }
    }
}