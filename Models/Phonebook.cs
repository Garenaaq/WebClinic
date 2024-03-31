using System;
using System.Collections.Generic;

namespace WebClinic.Models;

public partial class Phonebook
{
    public int Id { get; set; }

    public int? FkUsers { get; set; }

    public string? Phone { get; set; }

    public virtual User? FkUsersNavigation { get; set; }
}
