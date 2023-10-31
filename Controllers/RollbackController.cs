using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class RollbackController : Controller
    {
        ClinicContext _db;
        public RollbackController(ClinicContext db) 
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Rollback> objRollback = _db.Rollback.ToList();
            return View(objRollback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RollBack(string time) 
        {
            _db.Database.ExecuteSqlRaw($"CALL func_back('{time}')");
            return RedirectToAction("Index");
        }
    }
}
