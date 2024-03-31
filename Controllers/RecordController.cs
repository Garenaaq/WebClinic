using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class RecordController : Controller
    {
        ClinicContext _db;
        readonly IHttpContextAccessor _httpContextAccessor;

        public RecordController(ClinicContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public IActionResult MakeOrder(int id)
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("idUser");
            User user = null;
            if (userId is not null)
            {
                user = _db.Users.First(x => x.Id == userId);
            }
            if (user is not null && user.Role == "Пациент")
            {
                Patient patient = _db.Patients.First(x => x.FkUsersNavigation.Id == user.Id);
                Record record = new Record { DateRecords = null, FkPatientNavigation = patient, FkServiceNavigation = _db.MedicalServices.FirstOrDefault(x=>x.Id == id) };
                _db.Records.Add(record);
                _db.SaveChanges();
                return RedirectToAction("ActiveRecords", "PatientPage");
            }
            return RedirectToAction("Getservice", "ServicesViewer", new {id = id});
        }

        [HttpPost]
        public IActionResult TakeOrder(int id)
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("idUser");
            User user = null;
            if (userId is not null)
            {
                user = _db.Users.First(x => x.Id == userId);
            }
            if (user is not null && user.Role == "Сотрудник")
            {
                Employe doctor = _db.Employes.First(x => x.FkUsersNavigation.Id == user.Id);
                Record record = _db.Records.Include(x=>x.FkEmployeeNavigation).First(x=>x.Id == id);
                record.FkEmployeeNavigation = doctor;
                _db.Records.Update(record);
                _db.SaveChanges();
                return RedirectToAction("ActiveRecords", "DoctorPage");
            }
            return RedirectToAction("Getservice", "ServicesViewer", new { id = id });
        }

        [HttpPost]
        public IActionResult EditOrder(int id, DateTime Date, DateTime Time)
        {
            var record = _db.Records.First(x => x.Id == id);

            record.DateRecords = Date.Add(Time.TimeOfDay);

            _db.Records.Update(record);
            _db.SaveChanges();
            return RedirectToAction("ActiveRecords", "DoctorPage");
        }

        [HttpPost]
        public IActionResult FinishOrder(int id)
        {
            var record = _db.Records.Include(x=>x.FkPatientNavigation).Include(x=>x.FkServiceNavigation).Include(x=>x.FkEmployeeNavigation).First(x => x.Id == id);
           
            return View(record);
        }

        [HttpPost]
        public IActionResult Finish(string diagnosis, string therapy, bool save, int id)
        {
            Record record = _db.Records.Include(x => x.FkPatientNavigation).Include(x => x.FkServiceNavigation).Include(x => x.FkEmployeeNavigation).First(x => x.Id == id);
            if (save)
            {
                DiseaseHistory history = new DiseaseHistory
                {
                    DateRecord = record.DateRecords,
                    FkEmployeeNavigation = record.FkEmployeeNavigation,
                    FkPatientNavigation = record.FkPatientNavigation,
                    Diagnosis = diagnosis,
                    Therapy = therapy
                };

                _db.DiseaseHistories.Add(history);
            }
            _db.Records.Remove(record);
            _db.SaveChanges();
            return RedirectToAction("ActiveRecords", "DoctorPage");
        }
    }
}
