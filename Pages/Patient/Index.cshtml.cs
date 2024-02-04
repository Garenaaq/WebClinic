using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models.DomainModels;
using WebClinic.Models.ViewModels;

namespace WebClinic.Pages.Patient
{
    [Authorize(Roles = "patient")]
    public class IndexModel : PageModel
    {
        readonly ClinicContext _context;
        readonly UserManager<User> _userManager;

        public IndexModel(ClinicContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public PatientViewModel ViewModel { get; set; } = new PatientViewModel();


        public async Task<IActionResult> OnGet()
        {
            User? patientUser = await _userManager.FindByNameAsync(User.Identity.Name);

            if (patientUser == null)
            {
                return NotFound();
            }

            ViewModel.Phone = patientUser.PhoneNumber ?? "Ќомер не указан";

            //Ёто €вно требует переработки
            Models.DomainModels.Patient? patientInfo = await _context.Patients
                                                            .Include(x=>x.DiseaseHistories)
                                                            .ThenInclude(x => x.FkEmployeeNavigation)
                                                            .ThenInclude(x => x.FkUsersNavigation)
                                                            .FirstOrDefaultAsync(x=>x.FkUsers == patientUser.Id);

            if (patientInfo == null)
            {
                return NotFound();
            }

            ViewModel.Name = patientInfo.Name;
            ViewModel.Surname = patientInfo.Surname;
            ViewModel.Patronymic = patientInfo.Patronymic;
            ViewModel.Gender = patientInfo.Gender;
            ViewModel.BirthDate = patientInfo.Birthdate.Value.ToLocalTime();
            ViewModel.ArchivedRecords = patientInfo.DiseaseHistories.ToList();

            return Page();

        }
    }
}
