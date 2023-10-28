using Microsoft.AspNetCore.Mvc;

namespace WebClinic.Controllers
{
    public class PatientPageController : Controller
    {
        //тут лк пациента
        public IActionResult Index()
        {
            return View();
        }
    }
}
