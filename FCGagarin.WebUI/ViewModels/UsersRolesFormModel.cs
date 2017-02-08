using FCGagarin.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FCGagarin.WebUI.ViewModels
{
    public class UsersRolesFormModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime? DateOfBirth { get; set; }
        public string GUID { get; set; }
        [Display(Name = "Роли")]
        public IEnumerable<string> SelectedRoleIds { get; set; }
        public IEnumerable<ApplicationRole> Roles { get; set; }
    }
}