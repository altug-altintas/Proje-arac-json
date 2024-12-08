using AutoMapper;
using Proje_web.Areas.Member.Models.DTOs;
using Proje_Dal.Context;
using Proje_Dal.Repositories.Interfaces.Abstract;
using Proje_Dal.Repositories.Interfaces.Concrete;
using Proje_model.Models.Concrete;
using Proje_model.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace Proje_web.Areas.Member.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Area("Member")]
    public class AracController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IIslemRepo islemRepo;
        private readonly ProjectContext _project;
        private readonly IMapper _mapper;
        private readonly IFirmaSahisRepo _firmaSahis;
        private readonly IBaseRepo<Ulke> _ulkeRepository;
        private readonly IBaseRepo<Sehir> _sehirRepository;
        private readonly IIslemRepo _islemRepo;
        private readonly IIslemDRepo _islemDRepo;
        private readonly IAracRepo _aracRepo;


        public AracController(UserManager<AppUser> userManager, IAracRepo aracRepo, IIslemRepo islemRepo, ProjectContext project, IMapper mapper, IFirmaSahisRepo firmaSahis, IBaseRepo<Ulke> ulkeRepository, IBaseRepo<Sehir> sehirRepository, IIslemDRepo islemDRepo)
        {
            _userManager = userManager;
            _aracRepo = aracRepo;
            _project = project;
            _mapper = mapper;
            _firmaSahis = firmaSahis;
            _ulkeRepository = ulkeRepository;
            _sehirRepository = sehirRepository;
            _islemRepo = islemRepo;
            _islemDRepo = islemDRepo;
        }

        public async Task<IActionResult> CreateArac()
        {
            //AppUser appUser = await _userManager.GetUserAsync(User);
            var userId = User.FindFirstValue(ClaimTypes.Name);
            var appUser = await _userManager.FindByIdAsync(userId);



            var viewModel = new AracCreateDTO
            {
                FirmaSahisler = (_firmaSahis.GetDefaults(x => x.Statu != Statu.Passive && x.AppUserID == appUser.Id))
            .Select(fs => new SelectListItem
            {
                Value = fs.ID.ToString(),
                Text = fs.Adi
            }).ToList(),

            };

            return Json(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArac(AracCreateDTO dto)
        {
            //AppUser appUser = await _userManager.GetUserAsync(User);
            var userId = User.FindFirstValue(ClaimTypes.Name);
            var appUser = await _userManager.FindByIdAsync(userId);
            if (ModelState.IsValid)
            {

                var arac = _mapper.Map<Arac>(dto);
                arac.AppUserID = appUser.Id;
                _aracRepo.Create(arac);

               // return RedirectToAction("AracList");
                return Json(new { success = true, redirectUrl = Url.Action("AracList") });

            }

            ViewBag.FirmaSahisler = _firmaSahis.GetDefaults(x => x.Statu != Statu.Passive && x.AppUserID == appUser.Id);
            return Json(dto);
        }


        public async Task<IActionResult> AracList(int? firmaSahisId)
        {
            // AppUser appUser = await _userManager.GetUserAsync(User);
            var userId = User.FindFirstValue(ClaimTypes.Name);
            var appUser = await _userManager.FindByIdAsync(userId);

            // Kullanıcının sahip olduğu FirmaSahis listesini al
            ViewBag.FirmaSahisler = (_firmaSahis.GetDefaults(x => x.Statu != Statu.Passive && x.AppUserID == appUser.Id))
                .Select(fs => new SelectListItem
                {
                    Value = fs.ID.ToString(),
                    Text = fs.Adi
                }).ToList();

            // Araçları filtrele
            var list = firmaSahisId.HasValue
                ? _aracRepo.GetDefaults(x => x.FirmaSahisId == firmaSahisId.Value || x.Statu != Statu.Passive && x.AppUserID == appUser.Id)
                : _aracRepo.GetDefaults(x => x.AppUserID == appUser.Id && x.Statu != Statu.Passive);

            return Json(list);
        }



        [HttpPost]
        public IActionResult AracPasif([FromBody] int ID)
        {          

            var arac = _aracRepo.GetDefault(a => a.ID == ID);
            if (arac == null)
            {
                return Json(new { success = false, message = "İşlem bulunamadı." });
            }

            _aracRepo.Delete(arac);
            return Json(new { success = true });
        }   
         [HttpPost]
        public IActionResult AracAktif([FromBody] int ID)
        {          

            var arac = _aracRepo.GetDefault(a => a.ID == ID);
            if (arac == null)
            {
                return Json(new { success = false, message = "İşlem bulunamadı." });
            }

            _aracRepo.Active(arac);
            return Json(new { success = true });
        }  
    }
}
