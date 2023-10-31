using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class RollbackController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
