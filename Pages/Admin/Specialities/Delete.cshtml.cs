using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models;

namespace WebClinic.Pages.Admin.Specialities
{
    public class DeleteModel : PageModel
    {

        readonly ClinicContext _context;
        readonly IHttpContextAccessor _contextAccessor;

        public DeleteModel(ClinicContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public IActionResult OnPost(int? id)
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
