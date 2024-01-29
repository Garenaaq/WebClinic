using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClinic.Data;

namespace WebClinic.Pages.Admin.Services
{
    public class ActiveModel : PageModel
    {
        readonly ClinicContext _context;
        readonly IHttpContextAccessor _httpContextAccessor;

        public ActiveModel(ClinicContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        

        public IActionResult OnGet()
        {


        }
    }
}
