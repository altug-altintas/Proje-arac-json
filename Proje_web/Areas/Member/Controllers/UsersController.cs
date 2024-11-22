using AutoMapper;
using Proje_web.Areas.Member.Models.VMs;
using Proje_Dal.Context;
using Proje_Dal.Repositories.Interfaces.Concrete;
using Proje_model.Models.Concrete;
using Proje_model.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace Proje_web.Areas.Member.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Area("Member")]
    public class UsersController : Controller
    {


        private readonly ProjectContext _project;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(  UserManager<AppUser> userManager, IMapper mapper, ProjectContext project)
        {
            
            _project = project;
            _userManager = userManager;
        }


       
    }
}
