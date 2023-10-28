using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (userId is null)
            {
                return StatusCode(403);
            }
            return View();
        }

        public IActionResult FreeRecords()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("idUser");
            if (userId is null)
            {
                return StatusCode(403);
            }
            
            var model = _db.Records.Include(x=>x.FkEmployeeNavigation).Include(x => x.FkPatientNavigation).Include(x=>x.FkServiceNavigation).Where(x=>x.FkEmployeeNavigation == null).ToList();
            var specId = _db.Employes.Include(x=>x.FkSpecialityNavigation).First(x => x.FkUsers == userId).FkSpecialityNavigation.Id;
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
            User user = null;
            Employe employe = _db.Employes.First(x => x.FkUsers == userId);
            if(employe != null) 
            {
                var model = _db.Records.Include(x => x.FkEmployeeNavigation).Include(x => x.FkPatientNavigation).Include(x=>x.FkServiceNavigation).Where(x => x.FkEmployee == employe.Id).ToList();
                foreach (var record in model)
                {
                    var user_phones = _db.Users.Include(x => x.Phonebooks).First(x => x.Patients.Contains(record.FkPatientNavigation));
                    record.FkPatientNavigation.FkUsersNavigation = user_phones;
                }
                return View(model);
            }
            

            return StatusCode(403);
        }

        public IActionResult History()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("idUser");
            User user = null;
            Employe employe = _db.Employes.First(x => x.FkUsers == userId);
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

    }
}
