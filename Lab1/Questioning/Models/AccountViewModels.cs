using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Questioning.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Ім'я користувача")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Довжина імені повинна бути не манше {2} символів.", MinimumLength = 4)]
        [Display(Name = "Ім'я користувача")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Довжина пароля повинна бути не манше {2} символів.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження пароля")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають.")]
        public string ConfirmPassword { get; set; }
    }
}
