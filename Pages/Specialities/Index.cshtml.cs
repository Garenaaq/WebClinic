using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models;

namespace WebClinic.Pages.Specialities
{
    public class IndexModel : PageModel
    {

        readonly ClinicContext _context;
        readonly IHttpContextAccessor _contextAccessor;

        public IndexModel(ClinicContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public List<MedicalService> services { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {

            if (id is null)
            {
                return NotFound();
            }

            services = _context.MedicalServices
                .Where(service => service.FkSpeciality == id)
                .AsNoTracking()
                .ToList();

            return Page();
        }
    }
}
