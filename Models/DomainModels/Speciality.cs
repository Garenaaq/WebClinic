using System;
using System.Collections.Generic;

namespace WebClinic.Models.DomainModels;

public partial class Speciality
{
    public int Id { get; set; }

    public string? NameSpeciality { get; set; }

    public int? DeleteFlag { get; set; }

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();

    public virtual ICollection<MedicalService> MedicalServices { get; set; } = new List<MedicalService>();
}
