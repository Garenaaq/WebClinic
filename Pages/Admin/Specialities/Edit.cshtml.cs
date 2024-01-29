using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models;

namespace WebClinic.Pages.Admin.Specialities
{
    public class EditModel : PageModel
    {
        readonly ClinicContext _context;
        readonly IHttpContextAccessor _contextAccessor;

        public EditModel(ClinicContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        [BindProperty]
        public string name { get; set; } = default!;


        public IActionResult OnPost(int? id)
        {
            if (id is null)
            {
                return RedirectToPage("/Active");
            }

            IQueryable<Speciality> specialityQuery = _context.Specialities
                                                        .Where(x => x.Id == id);

            if (!specialityQuery.Any())
            {
                return RedirectToPage("/Active");
            }

            specialityQuery.ExecuteUpdate(speciality => speciality.SetProperty(s => s.NameSpeciality, name));

            return RedirectToPage("/Active");
        }
    }
}
