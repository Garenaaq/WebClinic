using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models.DomainModels;
using WebClinic.Models.ViewModels;

namespace WebClinic.Pages.Account.Doctor
{
    [Authorize(Roles = "doctor")]
    public class IndexModel : PageModel
    {
        readonly ClinicContext _context;
        readonly UserManager<User> _userManager;

        public IndexModel(ClinicContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public DoctorViewModel ViewModel { get; set; } = default!;


        public async Task<IActionResult> OnGet()
        {

            User? doctorUser = await _userManager.FindByNameAsync(User.Identity.Name);
            
            if (doctorUser is null)
            {
                return NotFound();
            }

            ViewModel.Phone = doctorUser.PhoneNumber ?? "Не указан";

            Employe? doctorInfo = await _context.Employes
                .Include(d => d.Specialities)
                .FirstOrDefaultAsync(d => d.FkUsers == doctorUser.Id);
                
            if (doctorInfo is null)
            {
                return NotFound();
            }

            ViewModel.Name = doctorInfo.Name;
            ViewModel.Surname = doctorInfo.Surname;
            ViewModel.Patronymic = doctorInfo.Patronymic ?? "";
            ViewModel.Gender = doctorInfo.Gender;
            ViewModel.BirthDate = doctorInfo.Birthdate;
            ViewModel.Specialities = doctorInfo.Specialities.ToList();

            return Page();
        }
    }
}
