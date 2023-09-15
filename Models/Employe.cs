using System;
using System.Collections.Generic;

namespace WebClinic.Models;

public partial class Employe
{
    public int Id { get; set; }

    public int? FkUsers { get; set; }

    public int? FkSpeciality { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Patronymic { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public int? DeleteFlag { get; set; }

    public virtual ICollection<DiseaseHistory> DiseaseHistories { get; set; } = new List<DiseaseHistory>();

    public virtual Speciality? FkSpecialityNavigation { get; set; }

    public virtual User? FkUsersNavigation { get; set; }

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}
