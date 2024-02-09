using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models.DomainModels;

namespace WebClinic.Pages.Doctors
{
    [Authorize(Roles ="admin")]
    public class ActiveModel : PageModel
    {
        readonly ClinicContext _context;

        public ActiveModel(ClinicContext context)
        {
            _context = context;
        }

        public List<Employe> Doctors { get; set; } = new();

        public IActionResult OnGet()
        {
            Doctors = _context.Employes.Where(x => x.DeleteFlag == 0)
                .Include(x => x.Specialities)
                .ToList();

            return Page();
        }

    }
}
