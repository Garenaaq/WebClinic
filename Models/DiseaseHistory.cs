using System;
using System.Collections.Generic;

namespace WebClinic.Models;

public partial class DiseaseHistory
{
    public int Id { get; set; }

    public DateTime? DateRecord { get; set; }

    public int? FkPatient { get; set; }

    public int? FkEmployee { get; set; }

    public string? Diagnosis { get; set; }

    public string? Therapy { get; set; }

    public virtual Employe? FkEmployeeNavigation { get; set; }

    public virtual Patient? FkPatientNavigation { get; set; }
}
