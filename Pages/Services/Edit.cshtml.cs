using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClinic.Data;
using WebClinic.Models.DomainModels;

namespace WebClinic.Pages.Services
{
    public class EditModel : PageModel
    {
        readonly ClinicContext _context;
        readonly IHttpContextAccessor _httpContextAccessor;

        public EditModel(ClinicContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty(SupportsGet = true)]
        public MedicalService? service { get; set; }

        public SelectList specialities { get; set; } = default!;


        public IActionResult OnGet(int? id)
        {
            if (id is null)
            {
                return RedirectToPage("/Active");
            }

            specialities = new SelectList(_context.Specialities.Where(x => x.DeleteFlag == 0).ToList(), "Id", "NameSpeciality");

            service = _context.MedicalServices.FirstOrDefault(s => s.Id == id);

            if (service is null)
            {
                return RedirectToPage("/Active");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || service is not null)
            {
                return Page();
            }

            _context.MedicalServices.Update(service);
            _context.SaveChanges();

            return RedirectToPage("/Active");
        }

    }
}
