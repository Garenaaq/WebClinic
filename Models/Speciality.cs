using System;
using System.Collections.Generic;

namespace WebClinic.Models;

public partial class Speciality
{
    public int Id { get; set; }

    public string? NameSpeciality { get; set; }

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();
}
