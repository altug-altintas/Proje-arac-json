using AutoMapper;
using Proje_web.Areas.Member.Models.DTOs;
using Proje_web.Areas.Member.Models.VMs;
using Proje_Dal.Context;
using Proje_Dal.Repositories.Interfaces.Abstract;
using Proje_Dal.Repositories.Interfaces.Concrete;
using Proje_model.Models.Concrete;
using Proje_model.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace Proje_web.Areas.Member.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Area("Member")]
    public class FirmaSahisController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ProjectContext _project;
        private readonly IMapper _mapper;
        private readonly IFirmaSahisRepo _firmaSahis;
        private readonly IBaseRepo<Ulke> _ulkeRepository;
        private readonly IBaseRepo<Sehir> _sehirRepository;

        public FirmaSahisController(UserManager<AppUser> userManager, ProjectContext project, IMapper mapper, IFirmaSahisRepo firmaSahis, IBaseRepo<Ulke> ulkeRepository, IBaseRepo<Sehir> sehirRepository)
        {
            _userManager = userManager;
            _project = project;
            _mapper = mapper;
            _firmaSahis = firmaSahis;
            _ulkeRepository = ulkeRepository;
            _sehirRepository = sehirRepository;
        }

        public async Task<IActionResult> Create()
        {
            //AppUser appUser = await _userManager.GetUserAsync(User);
            var userId = User.FindFirstValue(ClaimTypes.Name);
            var appUser = await _userManager.FindByIdAsync(userId);

            var viewModel = new FirmaSahisCreateDTO
            {

                Ulkeler = _ulkeRepository.GetDefaults(x => x.Statu == Statu.Active).Select(u => new SelectListItem
                {
                    Value = u.ID.ToString(),
                    Text = u.ulke_adi
                }).ToList(),
                Sehirler = _sehirRepository.GetDefaults(x => x.Statu == Statu.Active).Select(s => new SelectListItem
                {
                    Value = s.ID.ToString(),
                    Text = s.sehir_adi
                }).ToList()
            };

            return Json(viewModel);


        }
        [HttpPost]
        public async Task<IActionResult> Create(FirmaSahisCreateDTO vM)
        {
            //  AppUser appUser = await _userManager.GetUserAsync(User);
            var userId = User.FindFirstValue(ClaimTypes.Name);
            var appUser = await _userManager.FindByIdAsync(userId);
            if (ModelState.IsValid)
            {
                var firmaSahis = _mapper.Map<FirmaSahis>(vM);
                firmaSahis.AppUserID = appUser.Id;
                _firmaSahis.Create(firmaSahis);

                return Json(new { success = true, redirectUrl = Url.Action("GetFirmaSahisList") });
            }

            ViewBag.Ulkeler = _ulkeRepository.GetDefaults(x => x.Statu == Statu.Active);
            ViewBag.Sehirler = _sehirRepository.GetDefaults(x => x.Statu == Statu.Active);

            return Json(vM);
        }


        public async Task<IActionResult> GetFirmaSahisList()
        {
            // AppUser appUser = await _userManager.GetUserAsync(User);
            var userId = User.FindFirstValue(ClaimTypes.Name);
            var appUser = await _userManager.FindByIdAsync(userId);

            var ulkeler = _ulkeRepository.GetDefaults(x => x.Statu == Statu.Active);
            var sehirler = _sehirRepository.GetDefaults(x => x.Statu == Statu.Active);

            // FirmaSahis verilerini çekin
            var firmaSahisList = _firmaSahis.GetByDefaults(
                selector: a => new
                {
                    a.ID,
                    a.Adi,
                    a.Adres,
                    a.Telefon,
                    a.Email,
                    a.VergiNo,
                    a.VergiDairesi,
                    a.TC,
                    a.PostaNo,
                    a.UlkeId,
                    a.SehirId,
                    a.Tur,
                    a.Statu
                },
                expression: a => a.Statu != Statu.Passive && a.AppUserID==appUser.Id,
                include: null,
                orderBy: query => query.OrderBy(fs => fs.Adi)
            );

            // Ulke ve Sehir bilgilerini ekleyin
            var list = firmaSahisList.Select(a => new FirmaSahisDto
            {
                ID = a.ID,
                Adi = a.Adi,
                Adres = a.Adres,
                Telefon = a.Telefon,
                Email = a.Email,
                VergiNo = a.VergiNo,
                VergiDairesi = a.VergiDairesi,
                TC = a.TC,
                PostaNo = a.PostaNo,
                UlkeId = ulkeler.FirstOrDefault(u => u.ID == a.UlkeId)?.ulke_adi ?? "Unknown",
                SehirId = sehirler.FirstOrDefault(s => s.ID == a.SehirId)?.sehir_adi ?? "Unknown",
                Tur = a.Tur,
                Statu = a.Statu
            }).ToList();

            return Json(list);
        }

        [HttpGet("countries")]
        public IActionResult GetCountries()
        {
            var countries = _ulkeRepository.GetByDefaults(
                selector: x => new
                {
                    x.ID,
                    x.ulke_adi
                },
                expression: x => x.Statu == Statu.Active,
                orderBy: q => q.OrderBy(x => x.ulke_adi)
            );

            return Ok(countries);
        }
        [HttpGet("cities")]
        public IActionResult GetCities(int ulkeId)
        {
            var cities = _sehirRepository.GetByDefaults(
                selector: x => new
                {
                    x.ID,
                    x.sehir_adi,
                    x.sehir_plaka
                },
                expression: x => x.Statu == Statu.Active && x.UlkeId == ulkeId,
                orderBy: q => q.OrderBy(x => x.sehir_adi)
            );

            return Ok(cities);
        }


        public IActionResult FirmaUpdate(int id)
        {



            FirmaSahis firmaSahis = _firmaSahis.GetDefault(a => a.ID == id);

            var updateFirmaSahis = _mapper.Map<FirmaSahisUpdateDTO>(firmaSahis);


            updateFirmaSahis.Ulkeler = _ulkeRepository.GetDefaults(x => x.Statu == Statu.Active)
             .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.ulke_adi });
            updateFirmaSahis.Sehirler = _sehirRepository.GetDefaults(x => x.Statu == Statu.Active)
             .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.sehir_adi });

            return Json(updateFirmaSahis);
        }

        [HttpPost]
        public IActionResult FirmaUpdate(FirmaSahisUpdateDTO vM)
        {


            if (ModelState.IsValid != null)
            {
                var firmaSahis = _firmaSahis.GetDefault(a => a.ID == vM.ID);

                _mapper.Map(vM, firmaSahis);

                _firmaSahis.Update(firmaSahis);
                _project.Entry(firmaSahis).State = EntityState.Detached;

                return Json(new { success = true, redirectUrl = Url.Action("GetFirmaSahisList") });
            }
            return Json(new { success = true, redirectUrl = Url.Action("GetFirmaSahisList") });
        }



        public async Task<IActionResult> FirmaSahisPasif(int id)
        {
            FirmaSahis firmaSahis = _firmaSahis.GetByIdAsync(id).Result;

            _firmaSahis.Delete(firmaSahis);

            return Json(new { success = true, redirectUrl = Url.Action("GetFirmaSahisList") });

        }

        public async Task<IActionResult> FirmaSahisAktif(int id)
        {
            FirmaSahis firmaSahis = _firmaSahis.GetByIdAsync(id).Result;

            _firmaSahis.Active(firmaSahis);

            return Json(new { success = true, redirectUrl = Url.Action("GetFirmaSahisList") });

        }



        public IActionResult Index()
        {
            return View();
        }
    }
}
