using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly ClinicContext _db;
        private readonly IHttpContextAccessor _context;
        private List<string> listWithGender = new List<string>() { "Мужской", "Женский" };
        public AuthorizationController(ClinicContext db, IHttpContextAccessor context) 
        {
            _context = context;
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
            string loginSHA256 = ComputeSha256Hash(objUser.Login);
            string passSHA256 = ComputeSha256Hash(objUser.Pass);

            var objUserBd = _db.Users.FirstOrDefault (user => user.Login == loginSHA256 && user.Pass == passSHA256);

            if (objUserBd == null)
            {
                ModelState.AddModelError("Login", "Некорректно записан логин или пароль");
                ModelState.AddModelError("Pass", "Некорректно записан логин или пароль");
                return View();
            }

            _context.HttpContext.Session.SetInt32("idUser", objUserBd.Id);

            return RedirectToAction("Index", "Home");
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
            objUser.Login = ComputeSha256Hash(Login);
            objUser.Pass = ComputeSha256Hash(Pass);
            objUser.Role = "Пациент";

            try
            {
                _db.Users.Add(objUser);
                _db.SaveChanges();
            } catch 
            {
                ViewBag.Login = "Данный логин уже занят!";
                ViewBag.Pass = Pass;
                return View(objPatient);
            }

            var objUserDb = _db.Users.First(u => u.Login == ComputeSha256Hash(Login));

            objPatient.FkUsersNavigation = objUserDb;

            _db.Patients.Add(objPatient);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        private void FillViewBagWithGender()
        {
            ViewBag.listWithGender = listWithGender.Select(sex => new SelectListItem()
            {
                Text = sex,
                Value = sex
            });

        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
 
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
