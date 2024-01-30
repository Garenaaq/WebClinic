using System.ComponentModel.DataAnnotations;

namespace WebClinic.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите своё имя")]
        [MaxLength(25)]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "Имя содержит недопустимые символы")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Введите свою фамилию")]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "Фамилия содержит недопустимые символы")]
        public string Surname { get; set; } = default!;

        [RegularExpression(@"^[a-zA-Zа-яА-Я\s]+$", ErrorMessage = "Имя содержит недопустимые символы")]
        public string? Patronymic { get; set; }

        [Required(ErrorMessage = "Введите свой E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Введите имя учётной записи")]
        [MaxLength(50)]
        public string Login { get; set; } = default!;

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [MaxLength(100)]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "Введите дату рождения")]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; } = default!;

        [Required(ErrorMessage = "Укажите пол")]
        public string Gender { get; set; } = default!;

    }
}
