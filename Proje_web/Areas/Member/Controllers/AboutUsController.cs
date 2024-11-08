using Microsoft.AspNetCore.Mvc;

namespace Proje_web.Areas.Member
{
    [Area("Member")]
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
