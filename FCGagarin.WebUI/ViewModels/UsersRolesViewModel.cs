using FCGagarin.WebUI.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FCGagarin.WebUI.ViewModels
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
        public virtual ICollection<ApplicationRole> Roles { get; set; }
    }
}