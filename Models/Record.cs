using System;
using System.Collections.Generic;

namespace WebClinic.Models;

public partial class Record
{
    public int Id { get; set; }

    public DateTime? DateRecords { get; set; }

    public int? FkPatient { get; set; }

    public int? FkEmployee { get; set; }

    public int? FkService { get; set; }

    public virtual Employe? FkEmployeeNavigation { get; set; }

    public virtual Patient? FkPatientNavigation { get; set; }

    public virtual MedicalService? FkServiceNavigation { get; set; }
}
