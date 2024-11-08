using AutoMapper;
using Proje_Dal.Context;
using Proje_Dal.Repositories.Interfaces.Concrete;
using Proje_model.Models.Concrete;
using Proje_model.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Proje_web.Controllers
{
    public class UsersController : Controller
    {


        private readonly ProjectContext _project;
        private readonly UserManager<AppUser> _userManager;

        public UsersController( UserManager<AppUser> userManager, IMapper mapper, ProjectContext project)
        {
           
            _project = project;
            _userManager = userManager;
        }


       
    }
}
