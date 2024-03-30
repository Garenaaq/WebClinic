using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebClinic.Models;

public partial class Employe
{
    public int Id { get; set; }

    public int? FkUsers { get; set; }

    public int? FkSpeciality { get; set; }
    [Required(ErrorMessage = "Вы не ввели имя")]
    [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "Имя должно содержать только буквы")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Вы не ввели фамилию")]
    [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "Фамилия должна содержать только буквы")]
    public string Surname { get; set; }
    [Required(ErrorMessage = "Вы не ввели отчество")]
    [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "Отчество должно содержать только буквы")]
    public string Patronymic { get; set; }


    [DataType(DataType.DateTime)]
    public DateTime? Birthdate { get; set; }
    [Required(ErrorMessage = "Вы не указали пол")]
    public string Gender { get; set; }

    public int? DeleteFlag { get; set; }

    public virtual ICollection<DiseaseHistory> DiseaseHistories { get; set; } = new List<DiseaseHistory>();

    public virtual Speciality? FkSpecialityNavigation { get; set; }

    public virtual User? FkUsersNavigation { get; set; }

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}
