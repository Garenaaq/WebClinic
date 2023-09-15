using System;
using System.Collections.Generic;

namespace WebClinic.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Pass { get; set; }

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<Phonebook> Phonebooks { get; set; } = new List<Phonebook>();
}
