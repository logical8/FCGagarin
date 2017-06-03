using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FCGagarin.PL.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Microsoft.Build.Framework.Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Microsoft.Build.Framework.Required]
        public string Provider { get; set; }

        [Microsoft.Build.Framework.Required]
        [Display(Name = "Код")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Запомнить этот браузер?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Microsoft.Build.Framework.Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Microsoft.Build.Framework.Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Microsoft.Build.Framework.Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Microsoft.Build.Framework.Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Microsoft.Build.Framework.Required]
        [StringLength(100, ErrorMessage = "{0} должен содержать хотя бы {2} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Microsoft.Build.Framework.Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Microsoft.Build.Framework.Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Microsoft.Build.Framework.Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Microsoft.Build.Framework.Required]
        [StringLength(100, ErrorMessage = "{0} должен содержать хотя бы {2} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Microsoft.Build.Framework.Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
