using Microsoft.AspNetCore.Mvc;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class ServicesViewerController : Controller
    {
        ClinicContext _db;
        private readonly IHttpContextAccessor _contextSession;
        public ServicesViewerController(ClinicContext db, IHttpContextAccessor contextSession)
        {
            _db = db;
            _contextSession = contextSession;
        }

        public IActionResult Getservice(int id)
        {
            ViewBag.idUser = _contextSession.HttpContext?.Session.GetInt32("idUser");

            var service = _db.MedicalServices.FirstOrDefault(x => x.Id == id);
            //ViewBag.idUser = userId;
            return View(service);
        }

    }
}
