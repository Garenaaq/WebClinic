using Microsoft.AspNetCore.Mvc;

namespace WebClinic.Controllers
{
    public class DoctorPageController : Controller
    {
        //тут личный кабинет доктора
        public IActionResult Index()
        {
            return View();
        }
    }
}
