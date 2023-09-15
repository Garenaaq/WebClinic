using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebClinic.Models;

public partial class Patient
{
    [Key]
    public int Id { get; set; }
    [ValidateNever]
    public int? FkUsers { get; set; }
    [Required(ErrorMessage = "Вы не ввели имя")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Вы не ввели фамилию")]
    public string? Surname { get; set; }
    [Required(ErrorMessage = "Вы не ввели отчество")]
    public string? Patronymic { get; set; }
    [Range(14, int.MaxValue, ErrorMessage = "Регистрироваться можно от 14 лет")]
    [Required(ErrorMessage = "Вы не указали возраст")]
    public int? Age { get; set; }
    [Required(ErrorMessage = "Вы не указали пол")]
    public string? Gender { get; set; }

    public virtual ICollection<DiseaseHistory> DiseaseHistories { get; set; } = new List<DiseaseHistory>();

    public virtual User? FkUsersNavigation { get; set; }

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}
