using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models.DomainModels;

namespace WebClinic.Pages.Doctors
{
    [Authorize(Roles = "admin")]
    public class InactiveModel : PageModel
    {
        readonly ClinicContext _context;

        public InactiveModel(ClinicContext context)
        {
            _context = context;
        }

        public List<Employe> Doctors { get; set; } = new();

        public IActionResult OnGet()
        {
            Doctors = _context.Employes.Where(x => x.DeleteFlag == 1)
                .Include(x => x.Specialities)
                .ToList();

            return Page();
        }

    }
}
