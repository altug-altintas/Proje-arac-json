using Microsoft.AspNetCore.Mvc;

namespace Proje_web.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
