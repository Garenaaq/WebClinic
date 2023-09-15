using Microsoft.AspNetCore.Mvc;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class ServicesController : Controller
    {
        ClinicContext _context;

        public ServicesController(ClinicContext context)
        {
            _context = context;
        }

        public IActionResult Getservice(int id)
        {
            var service = _context.MedicalServices.FirstOrDefault(x => x.Id == id);
            return View(service);
        }

        //public IActionResult

    }
}
