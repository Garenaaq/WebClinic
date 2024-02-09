using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;
using WebClinic.Data;
using WebClinic.Models.DomainModels;
using WebClinic.Models.ViewModels;

namespace WebClinic.Pages.Doctors
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        readonly ClinicContext _context;
        readonly UserManager<User> _userManager;

        public CreateModel(ClinicContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private string[] gendersNames = { "Мужской", "Женский" };

        public DoctorRegistrtionViewModel ViewModel { get; set; }

        public SelectList Genders { get; set; }
        public SelectList Specialities {  get; set; }

        public IActionResult OnGet()
        {
            ViewModel = new();
            FillGendersList();
            FillSpecialitiesList();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                FillGendersList();
                FillSpecialitiesList();
                return Page();
            }

            IdentityResult creationResult = await _userManager.CreateAsync(new User()
            {
                UserName = ViewModel.Login,
                PhoneNumber = ViewModel.Phonenumber,
                Email = ViewModel.Email,
            }, ViewModel.Password);

            if (!creationResult.Succeeded) 
            {
                return Page();
            }

            await _context.Employes.AddAsync(new Employe()
            {
                Name = ViewModel.Name,
                Surname = ViewModel.Surname,
                Patronymic = ViewModel.Patronymic,
                Birthdate = ViewModel.BirthDate,
                Gender = ViewModel.Gender,
                Specialities = ViewModel.Specialities,
            });

            await _context.SaveChangesAsync();

            return RedirectToPage("/Doctors/Active");
        }


        private void FillGendersList()
        {
            Genders = new SelectList(gendersNames.Select(sex => new SelectListItem()
            { Text = sex, Value = sex }), "Text", "Value");
        }

        private void FillSpecialitiesList()
        {
            Specialities = new SelectList(_context.Specialities.Where(speciality => speciality.DeleteFlag == 0)
                .Select(speciality => new SelectListItem()
            {
                Text = speciality.NameSpeciality,
                Value = speciality.NameSpeciality
            }), "Text", "Value");
        }

    }
}
