using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClinic.Models.DomainModels;
using WebClinic.Models.ViewModels;

namespace WebClinic.Pages.Account
{
    public class LoginModel : PageModel
    {
        readonly SignInManager<User> _signInManager;

        public LoginModel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public LoginViewModel UserLoginModel { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(UserLoginModel.Login, UserLoginModel.Password, UserLoginModel.RememberMe, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("ErrorMessage", "Неправильные логин или пароль");
                return Page();
            }

            return RedirectToPage("/Index");
        }

    }
}
