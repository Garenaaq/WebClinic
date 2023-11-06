using Microsoft.AspNetCore.Mvc;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class SpecialitiesViewerController : Controller
    {
        ClinicContext _db;
        private readonly IHttpContextAccessor _contextSession;
        public SpecialitiesViewerController(ClinicContext db, IHttpContextAccessor contextSession)
        {
            _db = db;
            _contextSession = contextSession;
        }

        public IActionResult Getservices(int id)
        {
            ViewBag.idUser = _contextSession.HttpContext?.Session.GetInt32("idUser");

            var services = _db.MedicalServices.Where(service => service.Id == id).ToList();
            return View(services);
        }
    }
}
