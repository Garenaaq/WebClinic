using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Security.Cryptography;
using System.Text;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class PatientPageController : Controller
    {

        ClinicContext _db;
        readonly IHttpContextAccessor _httpContextAccessor;

        public PatientPageController(ClinicContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("idUser");
            ViewBag.idUser = userId;
            User user = null;
            Patient employe = _db.Patients.FirstOrDefault(x => x.FkUsers == userId);
            ViewBag.user = employe;
            if (employe != null)
            {
                var model = _db.DiseaseHistories.Include(x => x.FkEmployeeNavigation).Include(x => x.FkPatientNavigation).Where(x => x.FkPatient == employe.Id).ToList();
                foreach (var record in model)
                {
                    var user_phones = _db.Users.Include(x => x.Phonebooks).FirstOrDefault(x => x.Employes.Contains(record.FkEmployeeNavigation));
                    record.FkEmployeeNavigation.FkUsersNavigation = user_phones;
                }
                return View(model);
            }


            return StatusCode(403);
        }

        public IActionResult ActiveRecords()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("idUser");
            ViewBag.idUser = userId;
            User user = null;
            Patient employe = _db.Patients.FirstOrDefault(x => x.FkUsers == userId);
            if (employe != null)
            {
                var model = _db.Records.Include(x => x.FkEmployeeNavigation).Include(x => x.FkPatientNavigation).Include(x=>x.FkServiceNavigation).Where(x => x.FkPatient == employe.Id).ToList();
                foreach (var record in model)
                {
                    if (record.FkEmployeeNavigation is null) continue;
                    var user_phones = _db.Users.Include(x => x.Phonebooks).FirstOrDefault(x => x.Employes.Contains(record.FkEmployeeNavigation));
                    record.FkEmployeeNavigation.FkUsersNavigation = user_phones;
                }
                return View(model);
            }


            return StatusCode(403);
        }

        [HttpPost]
        public IActionResult AddPhone(int id, string phone)
        {
            Patient employe = _db.Patients.Include(x => x.FkUsersNavigation).FirstOrDefault(x => x.Id == id);
            if (employe != null)
            {
                var phones = _db.Phonebooks.Where(x => x.FkUsers == employe.FkUsersNavigation.Id);
                employe.FkUsersNavigation.Phonebooks = phones.ToList();
                Phonebook phoneNumber = new Phonebook { Phone = phone };
                _db.Phonebooks.Add(phoneNumber);
                employe.FkUsersNavigation.Phonebooks.Add(phoneNumber);
                _db.Patients.Update(employe);
                _db.SaveChanges();
                return RedirectToAction("Settings");
            }
            return StatusCode(403);
        }

        [HttpPost]
        public IActionResult DeletePhone(int phoneid)
        {
            var phone = _db.Phonebooks.FirstOrDefault(x => x.Id == phoneid);
            if (phone != null)
            {
                _db.Phonebooks.Remove(phone);
                _db.SaveChanges();
                return RedirectToAction("Settings");
            }
            return StatusCode(403);
        }


        private List<string> listWithGender = new List<string>() { "Мужской", "Женский" };

        public IActionResult Settings()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("idUser");
            ViewBag.idUser = userId;
            ViewBag.listWithGender = listWithGender.Select(sex => new SelectListItem()
            {
                Text = sex,
                Value = sex
            });
            User user = null;
            Patient employe = _db.Patients.Include(x => x.FkUsersNavigation).FirstOrDefault(x => x.FkUsers == userId);
            if (employe != null)
            {
                var phones = _db.Phonebooks.Where(x => x.FkUsers == employe.FkUsersNavigation.Id);
                employe.FkUsersNavigation.Phonebooks = phones.ToList();
                return View(employe);
            }


            return StatusCode(403);
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
