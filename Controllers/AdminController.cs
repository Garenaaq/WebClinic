using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class AdminController : Controller
    {

        ClinicContext _context;

        public AdminController(ClinicContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddService()
        {
            ViewBag.Specialities = new SelectList(_context.Specialities.ToList(), "Id", "NameSpeciality");
            return View();
        }

        public IActionResult AddSpeciality()
        {
            return View();
        }

        public IActionResult GetSpecialities() 
        {
            var specialities = _context.Specialities.ToList();
            return PartialView(specialities);
        }

        [HttpPost]
        public IActionResult AddServiceAction(MedicalService service)
        {
            _context.MedicalServices.Add(service);
            _context.SaveChanges();
            return RedirectToAction("AddService");
        }

        [HttpPost]
        public IActionResult AddSpecialityAction(Speciality speciality)
        {
            _context.Specialities.Add(speciality);
            _context.SaveChanges();
            return RedirectToAction("AddSpeciality");
        }
    }
}
