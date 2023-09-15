using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class AuthorizationController : Controller
    {
        private ClinicContext _db {  get; set; }
        public AuthorizationController(ClinicContext db) 
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User objUser)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Add(objUser);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(objUser);
        }

        [HttpGet]
        public IActionResult RegistrationPatient() 
        {
            return View();
        }
    }
}
