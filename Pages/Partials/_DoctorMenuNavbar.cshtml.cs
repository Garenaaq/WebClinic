using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClinic.Pages.Partials
{
    public class _DoctorMenuNavbarModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
