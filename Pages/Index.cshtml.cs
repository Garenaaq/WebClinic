using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models;

namespace WebClinic.Pages.Home
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

        public List<Speciality> specialities { get; set; } = default!;


        public IActionResult OnGet()
        {
            ViewData["idUser"] = _contextAccessor.HttpContext?.Session.GetInt32("idUser");

            specialities = _context.Specialities
                .AsNoTracking()                    
                .ToList();

            return Page();
        }
    }
}
