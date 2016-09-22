using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCGagarin.Domain.Model
{
    public class UserProfile
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
                return string.Format("{0} {1}", LastName, FirstName);
            }
            else
            {
                return Email;
            }
        }
    }
}
