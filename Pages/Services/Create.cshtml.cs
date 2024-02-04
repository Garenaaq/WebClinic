using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClinic.Data;
using WebClinic.Models.DomainModels;

namespace WebClinic.Pages.Services
{
    public class CreateModel : PageModel
    {
        readonly ClinicContext _context;
        readonly IHttpContextAccessor _contextAccessor;

        public CreateModel(ClinicContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        [BindProperty]
        public MedicalService service { get; set; } = default!;

        public SelectList specialities { get; set; } = default!;

        public IActionResult OnGet()
        {
            specialities = new SelectList(_context.Specialities.Where(x => x.DeleteFlag == 0).ToList(), "Id", "NameSpeciality");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MedicalServices.Add(service);
            _context.SaveChanges();

            return RedirectToPage("/Active");

        }
    }
}
