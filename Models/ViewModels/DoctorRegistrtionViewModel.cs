using System.ComponentModel.DataAnnotations;
using WebClinic.Models.DomainModels;

namespace WebClinic.Models.ViewModels
{
    public class DoctorRegistrtionViewModel : RegisterViewModel
    {
        [Required]
        public string Phonenumber { get; set; } = default!;
        public List<Speciality> Specialities { get; set; } = new List<Speciality>();
    }
}
