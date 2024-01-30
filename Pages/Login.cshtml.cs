using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClinic.Data;
using WebClinic.Models;

namespace WebClinic.Pages
{
    public class LoginModel : PageModel
    {
        readonly ClinicContext _context;

        public LoginModel(ClinicContext context)
        {
            _context = context;
        }

        public User user { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            //IQueryable<User> userQuery = _context.Users.Where(u => u.UserName == user.UserName && u.PasswordHash == user.PasswordHash);
           
            //if (!userQuery.Any()) return Page();

            return RedirectToPage("/Index");
        }

    }
}
