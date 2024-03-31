using System;
using System.Collections.Generic;

namespace WebClinic.Models;

public partial class MedicalService
{
    public int Id { get; set; }

    public string? NameService { get; set; }

    public string? Description { get; set; }

    public int? Price { get; set; }

    public int? DeleteFlag { get; set; }

    public int? FkSpeciality { get; set; }

    public virtual Speciality? FkSpecialityNavigation { get; set; }

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}
