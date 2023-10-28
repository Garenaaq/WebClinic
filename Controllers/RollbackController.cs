using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebClinic.Controllers
{
    public class RollbackController : Controller
    {
        // GET: RollbackController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RollbackController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RollbackController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RollbackController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RollbackController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RollbackController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RollbackController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RollbackController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
