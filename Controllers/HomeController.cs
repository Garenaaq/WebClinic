using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class HomeController : Controller
    {
        ClinicContext _context;

        public HomeController(ClinicContext clinicContext)
        {
            _context = clinicContext;
        }

        public IActionResult Index()
        {
            var specialities = _context.Specialities.ToList();
            return View(specialities);
        }
    }
}
