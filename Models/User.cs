using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebClinic.Models;

public class User: IdentityUser<int>
{
    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();
    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
    public virtual ICollection<Phonebook> Phonebooks { get; set; } = new List<Phonebook>();
}
