using Microsoft.AspNetCore.Mvc;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class SpecialitiesController : Controller
    {
        ClinicContext _context;

        public SpecialitiesController(ClinicContext context)
        {
            _context = context;
        }

        public IActionResult Getservices(int id)
        {
            var services = _context.MedicalServices.Where(service => service.Id == id).ToList();
            return View(services);
        }
    }
}
