using Microsoft.AspNetCore.Mvc;

namespace WebClinic.Controllers
{
    public class RecordController : Controller
    {
        //Здесь будут данные по записям
        public IActionResult Index()
        {
            return View();
        }
    }
}
