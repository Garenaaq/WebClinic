using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol;
using System.Security.Claims;
using WebClinic.Models.DomainModels;

namespace WebClinic.Pages.Account
{
    
    public class IndexModel : PageModel
    {
        readonly UserManager<User> _userManager;
        readonly SignInManager<User> _signInManager;

        public IndexModel(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToPage("../Admin/Menu");
            }

            if (User.IsInRole("doctor"))
            {
                return RedirectToPage("/Doctor/Index");
            }

            if (User.IsInRole("patient"))
            {
                return RedirectToPage("/Patient/Index");
            }

            return NotFound();
        }
    }
}
