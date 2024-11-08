using AutoMapper;
using Proje_web.Areas.Admin.Models.VMs;
using Proje_Dal.Context;
using Proje_Dal.Repositories.Concrete;
using Proje_Dal.Repositories.Interfaces.Abstract;
using Proje_Dal.Repositories.Interfaces.Concrete;
using Proje_model.Models.Concrete;
using Proje_model.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Proje_web.Areas.Admin.Controllers
{
    [Area("Admin")]
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


        public IslemController(UserManager<AppUser> userManager,IAracRepo aracRepo , IIslemRepo islemRepo, ProjectContext project, IMapper mapper, IFirmaSahisRepo firmaSahis, IBaseRepo<Ulke> ulkeRepository, IBaseRepo<Sehir> sehirRepository, IIslemDRepo islemDRepo)
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


        public IActionResult CreateIslem()
        {
            var firmaSahisler = _firmaSahis.GetDefaults(x => x.Statu != Statu.Passive);
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
        public IActionResult CreateIslem([FromBody]CreateislemDto dto)
        {
            if (ModelState.IsValid)
            {
                islem islem = _mapper.Map<islem>(dto);
                _islemRepo.Create(islem);
                dto.Id = islem.ID;              
                return Json(new { success = true, id = islem.ID });
            }

            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
        }


        public IActionResult GetAraclarByFirmaSahis(int? firmaSahisId)  //sağlıklı çalışıyor
        {
            var araclar = firmaSahisId.HasValue
                ? _aracRepo.GetDefaults(x => x.FirmaSahisId == firmaSahisId.Value && x.Statu != Statu.Passive)
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



        [HttpPost]
        public IActionResult SaveIslemD([FromBody] islemDDto dto)
        {
            if (ModelState.IsValid)
            {
                var islemD = _mapper.Map<islemD>(dto);
                _islemDRepo.Create(islemD);
                return Json(new { success = true,data= islemD });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage)
                                           .ToList();

            return Json(new { success = false, errors });
        }

        public async Task<IActionResult> GetisLemler(int islemId)
        {
            AppUser appUser = await _userManager.GetUserAsync(User);

            var list =  _islemDRepo.GetDefaults(a => a.Statu == Statu.Active && a.IslemId == islemId );

            return View(list);
        }



        [HttpPost]
        public IActionResult UpdateIslemD([FromBody] UpdateIslemDto dTO)
        {
            if (ModelState.IsValid)
            {
                islemD islemD = _islemDRepo.GetDefault(a => a.ID == dTO.ID);
                _mapper.Map(dTO, islemD);
                _islemDRepo.Update(islemD);
                return Json(new { success = true});
            }
            return Json(new { success = false });

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
