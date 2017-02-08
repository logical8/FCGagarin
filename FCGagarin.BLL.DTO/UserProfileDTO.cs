using System;

namespace FCGagarin.BLL.DTO
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string GUID { get; set; }
    }
}
