using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Security.Cryptography;
using System.Text;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class DoctorPageController : Controller
    {

        ClinicContext _db;
        readonly IHttpContextAccessor _httpContextAccessor;

        public DoctorPageController(ClinicContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        //тут личный кабинет доктора
        public IActionResult Index()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("idUser");
            ViewBag.idUser = userId;
            if (userId is null)
            {
                return StatusCode(403);
            }
            return View(_db.Employes.Include(x=>x.FkUsersNavigation).FirstOrDefault(x=>x.FkUsers == userId));
        }

        public IActionResult FreeRecords()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("idUser");
            ViewBag.idUser = userId;
            if (userId is null)
            {
                return StatusCode(403);
            }
            
            var model = _db.Records.Include(x=>x.FkEmployeeNavigation).Include(x => x.FkPatientNavigation).Include(x=>x.FkServiceNavigation).Where(x=>x.FkEmployeeNavigation == null).ToList();
            int specId = _db.Employes.Include(x=>x.FkSpecialityNavigation).FirstOrDefault(x => x.FkUsers == userId).FkSpecialityNavigation.Id;
            model = model.Where(x => x.FkServiceNavigation.FkSpeciality == specId).ToList();
            foreach (var record in model)
            {
                var user = _db.Users.Include(x=>x.Phonebooks).First(x => x.Patients.Contains(record.FkPatientNavigation));
                record.FkPatientNavigation.FkUsersNavigation = user;
            }
            return View(model);
        }

        public IActionResult ActiveRecords()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("idUser");
            ViewBag.idUser = userId;
            User user = null;
            Employe employe = _db.Employes.First(x => x.FkUsers == userId);
            if(employe != null) 
            {
                var model = _db.Records.Include(x => x.FkEmployeeNavigation).Include(x => x.FkPatientNavigation).Include(x=>x.FkServiceNavigation).Where(x => x.FkEmployee == employe.Id).ToList();
                foreach (var record in model)
                {
                    var user_phones = _db.Users.Include(x => x.Phonebooks).FirstOrDefault(x => x.Patients.Contains(record.FkPatientNavigation));
                    record.FkPatientNavigation.FkUsersNavigation = user_phones;
                }
                return View(model);
            }
            

            return StatusCode(403);
        }

        public IActionResult History()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("idUser");
            ViewBag.idUser = userId;
            User user = null;
            Employe employe = _db.Employes.FirstOrDefault(x => x.FkUsers == userId);
            if (employe != null)
            {
                var model = _db.DiseaseHistories.Include(x => x.FkEmployeeNavigation).Include(x => x.FkPatientNavigation).Where(x => x.FkEmployee == employe.Id).ToList();
                foreach (var record in model)
                {
                    var user_phones = _db.Users.Include(x => x.Phonebooks).First(x => x.Patients.Contains(record.FkPatientNavigation));
                    record.FkPatientNavigation.FkUsersNavigation = user_phones;
                }
                return View(model);
            }


            return StatusCode(403);
        }

        private List<string> listWithGender = new List<string>() { "Мужской", "Женский" };

        public IActionResult Settings()
        {
            ViewBag.listWithGender = listWithGender.Select(sex => new SelectListItem()
            {
                Text = sex,
                Value = sex
            });
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("idUser");
            ViewBag.idUser = userId;
            User user = null;
            Employe employe = _db.Employes.Include(x=>x.FkSpecialityNavigation).Include(x=>x.FkUsersNavigation).FirstOrDefault(x => x.FkUsers == userId);
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

        [HttpPost]
        public IActionResult AddPhone(int id, string phone)
        {
            Employe employe = _db.Employes.Include(x => x.FkSpecialityNavigation).Include(x => x.FkUsersNavigation).FirstOrDefault(x => x.Id == id);
            if (employe != null)
            {
                var phones = _db.Phonebooks.Where(x => x.FkUsers == employe.FkUsersNavigation.Id);
                employe.FkUsersNavigation.Phonebooks = phones.ToList();
                Phonebook phoneNumber = new Phonebook { Phone = phone};
                _db.Phonebooks.Add(phoneNumber);
                employe.FkUsersNavigation.Phonebooks.Add(phoneNumber);
                _db.Employes.Update(employe);
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

    }
}
