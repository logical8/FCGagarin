using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FCGagarin.PL.ViewModels
{
    public class UsersRolesViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string GUID { get; set; }
        [Display(Name = "Роли")]
        public virtual ICollection<ApplicationRoleViewModel> Roles { get; set; }
    }
}