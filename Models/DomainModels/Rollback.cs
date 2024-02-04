using System.ComponentModel.DataAnnotations;

namespace WebClinic.Models.DomainModels;

public partial class Rollback
{
    [Key]
    public int Id { get; set; }
    public DateTime Time { get; set; }
    public string Changes { get; set; }
    public string Method_ { get; set; }
    public string TabName { get; set; }
}
