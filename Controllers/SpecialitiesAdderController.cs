using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class SpecialitiesAdderController : Controller
    {
        ClinicContext _db;
        readonly IHttpContextAccessor _httpContextAccessor;

        public SpecialitiesAdderController(ClinicContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public ActionResult Index()
        {
            if (_httpContextAccessor.HttpContext?.Session.GetInt32("adminLoggedIn") is null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(_db.Specialities.Where(x=> x.DeleteFlag == 0).ToList());
        }

        public ActionResult Inactive()
        {
            if (_httpContextAccessor.HttpContext?.Session.GetInt32("adminLoggedIn") is null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(_db.Specialities.Where(x => x.DeleteFlag == 1).ToList());
        }

        [HttpPost]
        public IActionResult AddSpeciality([Bind("NameSpeciality")] Speciality speciality)
        {
            _db.Specialities.Add(speciality);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteSpeciality(int? id)
        {
            if (id != null)
            {
                Speciality? speciality = _db.Specialities.FirstOrDefault(x => x.Id == id);
                if (speciality != null)
                {
                    speciality.DeleteFlag = 1;
                    var employeesList = _db.Employes.Include(x => x.FkSpecialityNavigation).ToList();

                    foreach (var employee in employeesList)
                    {
                        employee.DeleteFlag = 1;
                        _db.Employes.Update(employee);
                    }
                    _db.Specialities.Update(speciality);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult BringBack(int? id)
        {
            if (id != null)
            {
                Speciality? speciality = _db.Specialities.FirstOrDefault(x => x.Id == id);
                if (speciality != null)
                {
                    speciality.DeleteFlag = 0;
                    _db.Specialities.Update(speciality);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(int id, string name)
        {
            Speciality upd = _db.Specialities.Include(x=>x.Employes).FirstOrDefault(x => x.Id == id);
            upd.NameSpeciality = name;
            _db.Specialities.Update(upd);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
