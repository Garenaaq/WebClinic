using Microsoft.AspNetCore.Mvc;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class ServicesViewerController : Controller
    {
        ClinicContext _db;

        public ServicesViewerController(ClinicContext db)
        {
            _db = db;
        }

        public IActionResult Getservice(int id)
        {
            var service = _db.MedicalServices.FirstOrDefault(x => x.Id == id);
            return View(service);
        }

    }
}
