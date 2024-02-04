using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebClinic.Models.DomainModels;

public partial class Patient
{
    [Key]
    public int Id { get; set; }

    public int? FkUsers { get; set; }

    public string Name { get; set; } = default!;

    public string Surname { get; set; } = default!;

    public string? Patronymic { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? Birthdate { get; set; }

    public string Gender { get; set; } = default!;

    public virtual ICollection<DiseaseHistory> DiseaseHistories { get; set; } = new List<DiseaseHistory>();

    public virtual User? FkUsersNavigation { get; set; }

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}
