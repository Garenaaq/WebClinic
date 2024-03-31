using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class AdminController : Controller
    {

        ClinicContext _db;
        readonly IHttpContextAccessor _httpContextAccessor;

        public AdminController(ClinicContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminAuth([Bind("Login", "Pass")] User admin)
        {
            admin.Login = ComputeHash(admin.Login);
            admin.Pass = ComputeHash(admin.Pass);

            var adminUser = _db.Users.FirstOrDefault(user => user.Login == admin.Login && user.Pass == admin.Pass && user.Role == "админ");

            if (adminUser == null)
            {
                return RedirectToAction("Index");
            }

            _httpContextAccessor.HttpContext?.Session.SetInt32("adminLoggedIn", 1);

            return RedirectToAction("AdminMenu");
        }

        public IActionResult AdminMenu()
        {
            if (_httpContextAccessor.HttpContext?.Session.GetInt32("adminLoggedIn") is null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext?.Session.Remove("adminLoggedIn");
            return RedirectToAction("Index");
        }

        private string ComputeHash(string rawData)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

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
