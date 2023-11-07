using Microsoft.AspNetCore.Mvc;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class SpecialitiesViewerController : Controller
    {
        ClinicContext _db;

        public SpecialitiesViewerController(ClinicContext db)
        {
            _db = db;
        }

        public IActionResult Getservices(int id)
        {
            var services = _db.MedicalServices.Where(service => service.Id == id).ToList();
            //ViewBag.idUser = userId;
            return View(services);
        }
    }
}
