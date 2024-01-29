using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models;

namespace WebClinic.Pages.Admin.Specialities
{
    public class ActiveModel : PageModel
    {
        readonly ClinicContext _context;
        readonly IHttpContextAccessor _contextAccessor;

        public ActiveModel(ClinicContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public List<Speciality> specialities { get; set; } = default!;

        [BindProperty]
        public Speciality speciality { get; set; } = new Speciality();

        [BindProperty]
        public string? name { get; set; }

        public IActionResult OnGet()
        {
            specialities = _context.Specialities
                .Where(x => x.DeleteFlag == 0)
                .AsNoTracking()
                .ToList();


            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || speciality is null)
            {
                return Page();
            }

            _context.Specialities.Add(speciality);
            _context.SaveChanges();

            return Page();
        }
    }
}
