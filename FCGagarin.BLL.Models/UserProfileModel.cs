using System;

namespace FCGagarin.BLL.Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string GUID { get; set; }
    }
}