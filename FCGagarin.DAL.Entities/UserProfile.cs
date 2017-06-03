using System;
using System.ComponentModel.DataAnnotations;
using FCGagarin.DAL.Entities.Abstract;

namespace FCGagarin.DAL.Entities
{
    public class UserProfile : BaseEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string GUID { get; set; }

        public override string ToString()
        {
            if (FirstName != LastName && !string.IsNullOrEmpty(FirstName))
            {
                return $"{LastName} {FirstName}";
            }
            else
            {
                return Email;
            }
        }
    }
}
