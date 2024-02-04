using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models.DomainModels;

namespace WebClinic.Pages.Home
{
    public class IndexModel : PageModel
    {
        readonly ClinicContext _context;

        public IndexModel(ClinicContext context)
        {
            _context = context;
        }

        public List<Speciality> specialities { get; set; } = default!;


        public IActionResult OnGet()
        {
            specialities = _context.Specialities
                .AsNoTracking()                    
                .ToList();

            return Page();
        }
    }
}
