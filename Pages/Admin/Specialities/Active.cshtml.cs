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
            if (_contextAccessor.HttpContext?.Session.GetInt32("adminLoggedIn") is null)
            {
                return RedirectToPage("/Admin/Index");
            }

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

        public IActionResult OnPostUpdate(int? id)
        {
            if (id is null)
            {
                return Page();
            }

            IQueryable<Speciality> specialityQuery = _context.Specialities
                                                        .Where(x => x.Id == id);

            if (!specialityQuery.Any())
            {
                return Page();
            }

            specialityQuery.ExecuteUpdate(speciality => speciality.SetProperty(s => s.NameSpeciality, name));

            return Page();

        }

        public IActionResult OnPostDelete(int? id)
        {
            if (id is null)
            {
                return Page();
            }

            IQueryable<Speciality> specialityQuery = _context.Specialities
                                                        .Where(x => x.Id == id);

            if (!specialityQuery.Any())
            {
                return Page();
            }

            specialityQuery.ExecuteUpdate(speciality => speciality.SetProperty(s => s.DeleteFlag, 1));

            return Page();
        }

    }
}
