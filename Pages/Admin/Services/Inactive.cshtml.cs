using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models;

namespace WebClinic.Pages.Admin.Services
{
    public class InactiveModel : PageModel
    {
        readonly ClinicContext _context;
        readonly IHttpContextAccessor _httpContextAccessor;

        public InactiveModel(ClinicContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<MedicalService> services { get; set; } = default!; 

        public IActionResult OnGet()
        {

            services = _context.MedicalServices
                            .Where(services => services.DeleteFlag == 1)
                            .AsNoTracking()
                            .ToList();

            return Page();

        }
    }
}
