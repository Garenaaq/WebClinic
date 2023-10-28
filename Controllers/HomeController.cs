using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class HomeController : Controller
    {
        ClinicContext _context;
        private readonly IHttpContextAccessor _contextSession;
        public HomeController(ClinicContext clinicContext, IHttpContextAccessor context)
        {
            _context = clinicContext;
            this._contextSession = context;
        }

        public IActionResult Index()
        {
            ViewBag.idUser = _contextSession.HttpContext?.Session.GetInt32("idUser");

            var specialities = _context.Specialities.ToList();
            return View(specialities);
        }
    }
}
