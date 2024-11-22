using Proje_model.Models.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Proje_web.Areas.Member.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Area("Member")]
    public class AppUserController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> LogOut()
        {

            await _signInManager.SignOutAsync();

            // return Redirect("~/");   // redirectionAction("index","home"); yerine 
            return Json(new { success = true, redirectUrl = "/" });

        }
    }
}
