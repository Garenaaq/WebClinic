using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class AuthorizationController : Controller
    {
        private ClinicContext _db {  get; set; }
        private List<string> listWithGender = new List<string>() { "Мужской", "Женский" };
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

        public IActionResult RegistrationPatient() 
        {
            FillViewBagWithGender();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegistrationPatient(Patient objPatient, string? Login, string? Pass)
        {
            FillViewBagWithGender();
            if (!ModelState.IsValid)
            {
                ViewBag.Login = Login;
                ViewBag.Pass = Pass;
                return View(objPatient);
            }
            User objUser = new User();
            objUser.Login = Login;
            objUser.Pass = Pass;
            objUser.Role = "Пациент";
            Console.WriteLine(objUser.Role);
            Console.WriteLine(objUser.Id.ToString());
            _db.Users.Add(objUser);
            _db.SaveChanges();

            return View();
        }

        private void FillViewBagWithGender()
        {
            ViewBag.listWithGender = listWithGender.Select(sex => new SelectListItem()
            {
                Text = sex,
                Value = sex
            });

        }
    }
}
