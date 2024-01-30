using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebClinic.Data;
using WebClinic.Models;
using WebClinic.Models.ViewModels;

namespace WebClinic.Pages
{
    public class RegistrationModel : PageModel
    {
        readonly ClinicContext _context;
        readonly UserManager<User> _userManager;
        readonly SignInManager<User> _signInManager;

        public RegistrationModel(UserManager<User> userManager, SignInManager<User> signInManager, ClinicContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [BindProperty]
        public RegisterViewModel PatientRegisterModel { get; set; } = default!;

        public SelectList? Genders { get; set; }

        private List<string> gendersNames = new List<string>() { "Мужской", "Женский" };

        public IActionResult OnGet()
        {
            FillGendersList();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                FillGendersList();
                return Page();
            }

            User newUser = new User()
            {
                UserName = PatientRegisterModel.Login,
                Email = PatientRegisterModel.Email,
            };

            IdentityResult addingAction = await _userManager.CreateAsync(newUser, PatientRegisterModel.Password);

            if (!addingAction.Succeeded)
            {
                foreach(var item in addingAction.Errors) { Console.WriteLine(item.Code); }
                FillGendersList();
                return Page();
            }

            Patient newPatient = new Patient()
            {
                Name = PatientRegisterModel.Name,
                Surname = PatientRegisterModel.Surname,
                Patronymic = PatientRegisterModel.Patronymic,
                Birthdate = PatientRegisterModel.BirthDate,
                Gender = PatientRegisterModel.Gender,
                FkUsersNavigation = newUser
                
            };

            _context.Patients.Add(newPatient);

            await _context.SaveChangesAsync();

            await _signInManager.SignInAsync(newUser, false);

            return RedirectToPage("Index");

        }

        private void FillGendersList()
        {
            Genders = new SelectList(gendersNames.Select(sex => new SelectListItem() 
            { Text = sex, Value = sex }), "Text", "Value");
        }

    }
}
