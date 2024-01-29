using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class ServicesAdderController : Controller
    {

        ClinicContext _db;
        readonly IHttpContextAccessor _httpContextAccessor;

        public ServicesAdderController(ClinicContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public ActionResult Index()
        {

            if(_httpContextAccessor.HttpContext?.Session.GetInt32("adminLoggedIn") is null)
            {
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Specialities = new SelectList(_db.Specialities.Where(x=>x.DeleteFlag == 0).ToList(), "Id", "NameSpeciality");
            var model = _db.MedicalServices.Include(x=>x.FkSpecialityNavigation).ToList();
            return View(model.Where(x => x.DeleteFlag == 0).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddService([Bind("NameService", "Description", "FkSpeciality", "Price")] MedicalService service)
        {
            _db.MedicalServices.Add(service);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditService(int id)
        {

            if (_httpContextAccessor.HttpContext?.Session.GetInt32("adminLoggedIn") is null)
            {
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Specialities = new SelectList(_db.Specialities.Where(x => x.DeleteFlag == 0).ToList(), "Id", "NameSpeciality");
            return View(_db.MedicalServices.First(x => x.Id == id));
        }

        public ActionResult Inactive()
        {
            if (_httpContextAccessor.HttpContext?.Session.GetInt32("adminLoggedIn") is null)
            {
                return RedirectToAction("Index", "Admin");
            }
            var model = _db.MedicalServices.Include(x => x.FkSpecialityNavigation).ToList();
            return View(model.Where(x=>x.DeleteFlag == 1).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditService(MedicalService service, int flag)
        {
            service.DeleteFlag = flag;
            _db.MedicalServices.Update(service);
            _db.SaveChanges();
            return RedirectToAction("ActiveServices");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteService(int id)
        {

            var deleted = _db.MedicalServices.FirstOrDefault(x => x.Id == id);
                
            if (deleted != null)
            {
                deleted.DeleteFlag = 1;
                _db.MedicalServices.Update(deleted);
                _db.SaveChanges();
            }
            return RedirectToAction("ActiveServices");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BringbackService(int id)
        {

            var deleted = _db.MedicalServices.FirstOrDefault(x => x.Id == id);

            if (deleted != null)
            {
                deleted.DeleteFlag = 0;
                _db.MedicalServices.Update(deleted);
                _db.SaveChanges();
            }
            return RedirectToAction("Inactive");
        }

        [HttpGet]
        public ActionResult ActiveServices()
        {
            if (_httpContextAccessor.HttpContext?.Session.GetInt32("adminLoggedIn") is null)
            {
                return RedirectToAction("Index", "Admin");
            }
            List<MedicalService> model = _db.MedicalServices.ToList();

            return View(model);
        }
    }
}
