using WebClinic.Models.DomainModels;

namespace WebClinic.Models.ViewModels
{
    public class PatientViewModel
    {
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string? Patronymic { get; set; }
        public string Gender { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public DateTime BirthDate { get; set; } = default!;
        public List<DiseaseHistory> ArchivedRecords { get; set; } = default!;
    }
}
