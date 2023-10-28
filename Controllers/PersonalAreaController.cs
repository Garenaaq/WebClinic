using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class PersonalAreaController : Controller
    {
        private readonly ClinicContext _db;
        private readonly IHttpContextAccessor _context;

        public PersonalAreaController(ClinicContext db, IHttpContextAccessor _session)
        {
            _db = db;
            _context = _session;
        }

        [HttpGet]
        public IActionResult Index()
        {
            int idUser = (int)_context.HttpContext?.Session.GetInt32("idUser");
            var objUser = _db.Users.Include(x=>x.).FirstOrDefault(user => user.Id == idUser);
            if (objUser != null) 
            {
                return View(objUser);
            }
            return View();
        }
    }
}
