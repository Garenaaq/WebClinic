using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Security.Cryptography;
using System.Text;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class DoctorAdderController : Controller
    {
        ClinicContext _db;
        readonly IHttpContextAccessor _httpContextAccessor;
        private List<string> listWithGender = new List<string>() { "Мужской", "Женский" };

        public DoctorAdderController(ClinicContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            if (_httpContextAccessor.HttpContext?.Session.GetInt32("adminLoggedIn") is null)
            {
                return RedirectToAction("Index", "Admin");
            }
            FillViewBagWithGender();
            ViewBag.specialities = _db.Specialities.Select(speciality => new SelectListItem()
            {
                Text = speciality.NameSpeciality,
                Value = speciality.NameSpeciality
            });
            return View();
        }

        public IActionResult EditDoctor(int id)
        {
            if (_httpContextAccessor.HttpContext?.Session.GetInt32("adminLoggedIn") is null)
            {
                return RedirectToAction("Index", "Admin");
            }
            FillViewBagWithGender();
            ViewBag.specialities = _db.Specialities.Select(speciality => new SelectListItem()
            {
                Text = speciality.NameSpeciality,
                Value = speciality.NameSpeciality
            });

            var doctor = _db.Employes.Include(x=>x.FkUsersNavigation).AsNoTracking().FirstOrDefault(x=>x.Id == id);
            return View(doctor);
        }

        [HttpPost]
        public IActionResult EditDoctor(Employe doctor, int flag, string speciality)
        {
            var fkUser = _db.Employes
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == doctor.Id)!.FkUsers;
            

            var objSpecialityDB = _db.Specialities.FirstOrDefault(u => u.NameSpeciality == speciality);

            doctor.FkSpecialityNavigation = objSpecialityDB;

            doctor.FkUsers = fkUser;

            doctor.DeleteFlag = flag;

            _db.Employes.Update(doctor);
            _db.SaveChanges();

            return RedirectToAction("ShowDoctors");
        }

        [HttpPost]
        public IActionResult MakeInactive(int id)
        {
            var doctor = _db.Employes.Include(x=>x.Records).First(x => x.Id == id);
            doctor.DeleteFlag = 1;
            _db.Employes.Update(doctor);

            var activeDoctors = _db.Employes.Include(x=>x.Records).Where(x => x.DeleteFlag == 0 && x.FkSpeciality == doctor.FkSpeciality);

            if (activeDoctors.Count() != 0)
            {
                var freeDoctor = activeDoctors.OrderBy(x => x.Records.Count).First();

                foreach (var record in doctor.Records)
                {
                    record.FkEmployeeNavigation = freeDoctor;
                }
            }

            _db.SaveChanges();
            return RedirectToAction("ShowDoctors");
        }

        [HttpPost]
        public IActionResult MakeActive(int id)
        {
            var doctor = _db.Employes.First(x => x.Id == id);
            doctor.DeleteFlag = 0;
            _db.Employes.Update(doctor);
            _db.SaveChanges();
            return RedirectToAction("ShowDoctors");
        }

        public IActionResult ShowDoctors()
        {
            if (_httpContextAccessor.HttpContext?.Session.GetInt32("adminLoggedIn") is null)
            {
                return RedirectToAction("Index", "Admin");
            }
            var model = _db.Employes.Include(x => x.FkSpecialityNavigation).ToList();
            
            return View(model);
        }

        public IActionResult ShowInactive()
        {
            if (_httpContextAccessor.HttpContext?.Session.GetInt32("adminLoggedIn") is null)
            {
                return RedirectToAction("Index", "Admin");
            }
            var model = _db.Employes.Include(x => x.FkSpecialityNavigation).ToList();
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Employe objPatient, string? Login, string? Pass, string? speciality, string phone)
        {
            Console.WriteLine(Login);
            if (!ModelState.IsValid)
            {
                ViewBag.Login = Login;
                ViewBag.Pass = Pass;
                FillViewBagWithGender();
                ViewBag.specialities = _db.Specialities.Select(speciality => new SelectListItem()
                {
                    Text = speciality.NameSpeciality,
                    Value = speciality.NameSpeciality
                });
                return View(objPatient);
            }

            User objUser = new User();
            objUser.Login = ComputeHash(Login);
            objUser.Pass = ComputeHash(Pass);
            objUser.Role = "Сотрудник";

            var phoneNumber = new Phonebook { Phone = phone };

            _db.Phonebooks.Add(phoneNumber);

            objUser.Phonebooks.Add(phoneNumber);

            try
            {
                _db.Users.Add(objUser);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.Login = "Данный логин уже занят!";
                ViewBag.Pass = Pass;
                return RedirectToAction("Index", new { objUser = objUser});
            }

            var objUserDb = _db.Users.FirstOrDefault(u => u.Login == ComputeHash(Login));
            var objSpecialityDB = _db.Specialities.FirstOrDefault(u => u.NameSpeciality == speciality);


            objPatient.FkUsersNavigation = objUserDb;

            objPatient.FkSpecialityNavigation = objSpecialityDB;

            _db.Employes.Add(objPatient);
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Врач успешно зарегистрирован";

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
