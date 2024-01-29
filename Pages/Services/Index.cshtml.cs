using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClinic.Data;
using WebClinic.Models;

namespace WebClinic.Pages.Services
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

        public MedicalService medicalService { get; set; } = default!;


        public IActionResult OnGet(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            IQueryable<MedicalService> serviceQuery = _context.MedicalServices
                                                            .Where(service => service.Id == id);

            if(!serviceQuery.Any())
            {
                return NotFound();
            }

            medicalService = serviceQuery.First();

            return Page();


        }
    }
}
