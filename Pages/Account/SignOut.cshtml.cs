using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClinic.Models.DomainModels;

namespace WebClinic.Pages.Account
{
    public class SignOutModel : PageModel
    {
        readonly SignInManager<User> _signInManager;

        public SignOutModel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("../Index");
        }
    }
}
