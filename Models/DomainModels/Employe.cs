using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebClinic.Models.DomainModels;

public partial class Employe
{
    public int Id { get; set; }

    public int? FkUsers { get; set; }

    public int? FkSpeciality { get; set; }
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    public string Surname { get; set; } = default!;
    public string? Patronymic { get; set; }

    [Required]
    public DateTime Birthdate { get; set; } = default!;

    [Required]
    public string Gender { get; set; } = default!;

    public int? DeleteFlag { get; set; }

    public virtual ICollection<DiseaseHistory> DiseaseHistories { get; set; } = new List<DiseaseHistory>();

    public virtual ICollection<Speciality> Specialities { get; set; } = default!;

    public virtual User? FkUsersNavigation { get; set; }

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}
