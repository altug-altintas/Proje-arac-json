using AutoMapper;
using Proje_Dal.Context;
using Proje_Dal.Repositories.Interfaces.Abstract;
using Proje_Dal.Repositories.Interfaces.Concrete;
using Proje_model.Models.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Proje_model.Models.Enums;
using Proje_web.Areas.Member.Models.VMs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Proje_web.Areas.Member.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Area("Member")]
    public class IslemController : Controller
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


        public IslemController(UserManager<AppUser> userManager, IAracRepo aracRepo, IIslemRepo islemRepo, ProjectContext project, IMapper mapper, IFirmaSahisRepo firmaSahis, IBaseRepo<Ulke> ulkeRepository, IBaseRepo<Sehir> sehirRepository, IIslemDRepo islemDRepo)
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


        public async Task<IActionResult> CreateIslem()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            var firmaSahisler = _firmaSahis.GetDefaults(x => x.Statu != Statu.Passive && x.AppUserID == appUser.Id);
            ViewBag.FirmaSahisler = firmaSahisler ?? new List<FirmaSahis>();

            var model = new CreateislemDto
            {
                Tarih = DateTime.Now,
                Yil = DateTime.Now.Year,
                No = _islemRepo.GetNextFisNo(),

            };
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIslem([FromBody] CreateislemDto dto)
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                islem islem = _mapper.Map<islem>(dto);
                islem.AppUserID = appUser.Id;
                _islemRepo.Create(islem);
                dto.Id = islem.ID;
                return Json(new { success = true, id = islem.ID });
            }

            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
        }


        public async Task<IActionResult> GetAraclarByFirmaSahis(int? firmaSahisId)  //sağlıklı çalışıyor
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            var araclar = firmaSahisId.HasValue
                ? _aracRepo.GetDefaults(x => x.FirmaSahisId == firmaSahisId.Value && x.Statu != Statu.Passive && x.AppUserID == appUser.Id)
                : _aracRepo.GetDefaults(x => x.Statu != Statu.Passive);

            var result = araclar.Select(a => new
            {
                a.ID,
                a.Plaka
            }).ToList();

            Console.WriteLine("Dönen Araçlar: " + JsonConvert.SerializeObject(result));
            return Json(result);
        }



        public IActionResult GetIslemDetailsById(int islemId)
        {
            var islem = _project.IslemDetaylar
                .Where(i => i.ID == islemId)
                .Select(i => new
                {
                    i.MalzemeFiyat,
                    i.IscilikFiyat,
                    i.ToplamFiyat,
                    i.islemAciklama,
                    i.islemTur,
                    i.BakimKM,
                    Plaka = i.Arac.Plaka,
                    AracId = i.Arac.ID
                })
                .FirstOrDefault();

            return Ok(islem);
        }



        //[HttpPost]
        //public async Task<IActionResult> SaveIslemD([FromBody] islemDDto dto)
        //{
        //    AppUser appUser = await _userManager.GetUserAsync(User);
        //    if (ModelState.IsValid)
        //    {
        //        var islemD = _mapper.Map<islemD>(dto);
        //        islemD.AppUserID = appUser.Id;
        //        _islemDRepo.Create(islemD);
        //        return Json(new { success = true, data = islemD });
        //    }


        //    return Json(new { success = false });
        //}

        [HttpPost]
        public async Task<IActionResult> SaveIslemD([FromBody] islemDDto dto)
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var islemD = _mapper.Map<islemD>(dto);
                islemD.AppUserID = appUser.Id;
                _islemDRepo.Create(islemD);
                return Json(new
                {
                    success = true,
                    data = new
                    {
                        ID = islemD.ID,
                        islemTur = islemD.islemTur,
                        BakimKM = islemD.BakimKM,
                        MalzemeFiyat = islemD.MalzemeFiyat,
                        IscilikFiyat = islemD.IscilikFiyat,
                        ToplamFiyat = islemD.ToplamFiyat,
                        islemAciklama = islemD.islemAciklama,
                        AracId = islemD.AracId                       
                    }
                });
            }

            return Json(new { success = false });
        }


        [HttpPost]
        public IActionResult DeleteIslemD([FromBody] int ID)
        {          

            var islemD = _islemDRepo.GetDefault(a => a.ID == ID);
            if (islemD == null)
            {
                return Json(new { success = false, message = "İşlem bulunamadı." });
            }

            _islemDRepo.Delete(islemD);
            return Json(new { success = true });
        }




        [HttpPost]
        public async Task<IActionResult> UpdateIslemD([FromBody] UpdateIslemDto dTO)
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                islemD islemD = _islemDRepo.GetDefault(a => a.ID == dTO.ID);
                _mapper.Map(dTO, islemD);
                islemD.AppUserID = appUser.Id;
                _islemDRepo.Update(islemD);

                return Json(new
                {
                    success = true

                });
            }

            return Json(new { success = false });
        }

        public IActionResult ListIslemD()
        {
            AppUser appUser = _userManager.GetUserAsync(User).Result;

            // Modeli doldurma
            var islemDList = _islemDRepo.GetByDefaults(
                selector: x => new IslemDListVM
                {
                    ID = x.ID,
                    MalzemeFiyat = x.MalzemeFiyat,
                    IscilikFiyat = x.IscilikFiyat,
                    ToplamFiyat = x.ToplamFiyat,
                    IslemAciklama = x.islemAciklama,
                    IslemTur = x.islemTur,
                    BakimKM = x.BakimKM,
                    AracId = x.AracId,
                    FirmaSahisId = x.FirmaSahisId,
                    AracPlaka = x.Arac.Plaka,
                    FirmaAd = x.FirmaSahis.Adi
                },
                expression: x => x.Statu == Statu.Active && x.AppUserID == appUser.Id,
                include: q => q.Include(x => x.Arac)
                                .Include(x => x.FirmaSahis)
            );

            return Json(islemDList);
        }





        //public async Task<IActionResult> ListIslemD()
        //{
        //    AppUser appUser = await _userManager.GetUserAsync(User);
        //    var islemDList = _islemDRepo.GetByDefaults(
        //        selector: x => new
        //        {
        //            x.ID,
        //            x.MalzemeFiyat,
        //            x.IscilikFiyat,
        //            x.ToplamFiyat,
        //            x.islemAciklama,
        //            x.islemTur,
        //            x.BakimKM,
        //            AracId = x.AracId,
        //            FirmaSahisId = x.FirmaSahisId,
        //            AracPlaka = x.Arac.Plaka,
        //            FirmaAd = x.FirmaSahis.Adi 
        //        },
        //        expression: x => x.Statu == Statu.Active && x.AppUserID == appUser.Id,
        //        include: q => q.Include(x => x.Arac)
        //                     .Include(x => x.FirmaSahis) 
        //    );

        //    return View(islemDList);
        //}



        public async Task<IActionResult> GetisLemler(int islemId)
        {
            AppUser appUser = await _userManager.GetUserAsync(User);

            var list = _islemDRepo.GetDefaults(a => a.Statu == Statu.Active && a.IslemId == islemId && a.AppUserID == appUser.Id);

            return Json(list);
        }





        public IActionResult Index()
        {
            return View();
        }
    }
}
