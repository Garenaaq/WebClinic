using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models.DomainModels;

namespace WebClinic.Pages.Services
{
    public class ActiveModel : PageModel
    {
        readonly ClinicContext _context;
        readonly IHttpContextAccessor _httpContextAccessor;

        public ActiveModel(ClinicContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<MedicalService> services { get; set; } = default!;

        public IActionResult OnGet()
        {
            services = _context.MedicalServices
                .Where(service => service.DeleteFlag == 0)
                .AsNoTracking()
                .ToList();

            return Page();
        }
    }
}
