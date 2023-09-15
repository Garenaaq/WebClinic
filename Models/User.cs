using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebClinic.Models;

public partial class User
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Вы не ввели логин")]
    public string? Login { get; set; }
    public string? Pass { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();
    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
    public virtual ICollection<Phonebook> Phonebooks { get; set; } = new List<Phonebook>();
}
