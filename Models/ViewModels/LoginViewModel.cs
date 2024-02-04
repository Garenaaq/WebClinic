using System.ComponentModel.DataAnnotations;

namespace WebClinic.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите имя учётной записи")]
        [MaxLength(50)]
        public string Login { get; set; } = default!;

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [MaxLength(100)]
        public string Password { get; set; } = default!;

        public bool RememberMe { get; set; } = false;

        public string? ErrorMessage { get; set; }

    }
}
