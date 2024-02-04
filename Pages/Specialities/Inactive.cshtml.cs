using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models.DomainModels;

namespace WebClinic.Pages.Specialities
{
    public class InactiveModel : PageModel
    {
        readonly ClinicContext _context;
        readonly IHttpContextAccessor _contextAccessor;

        public InactiveModel(ClinicContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }
        public List<Speciality> specialities { get; set; } = default!;

        public IActionResult OnGet()
        {

            specialities = _context.Specialities
                .Where(specality => specality.DeleteFlag == 1)
                .AsNoTracking()
                .ToList();

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id is null)
            {
                return Page();
            }

            IQueryable<Speciality> specialityQuery = _context.Specialities
                                                                .Where(s => s.Id == id);

            if (!specialityQuery.Any())
            {
                return Page();
            }

            specialityQuery.ExecuteUpdate(speciality => speciality.SetProperty(s => s.DeleteFlag, 0));

            return Page();
        }
    }
}
