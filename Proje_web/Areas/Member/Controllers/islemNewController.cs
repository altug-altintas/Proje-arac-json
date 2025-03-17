using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje_Dal.Context;
using Proje_Dal.Repositories.Interfaces.Abstract;
using Proje_Dal.Repositories.Interfaces.Concrete;
using Proje_model.Models.Concrete;
using Proje_model.Models.Enums;
using Proje_web.Areas.Member.Models.VMs;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Proje_web.Areas.Member.Controllers
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Area("Member")]
    public class islemNewController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IIslemRepo islemRepo;
        private readonly ProjectContext _project;
        private readonly IMapper _mapper;
        private readonly IFirmaSahisRepo _firmaSahis;
        private readonly IBaseRepo<Ulke> _ulkeRepository;
        private readonly IBaseRepo<Sehir> _sehirRepository;
        private readonly IAracRepo _aracRepo;
        private readonly IisLemNewRepo _isLemNewRepo;


        public islemNewController(UserManager<AppUser> userManager, IisLemNewRepo isLemNew, IAracRepo aracRepo, ProjectContext project, IMapper mapper, IFirmaSahisRepo firmaSahis, IBaseRepo<Ulke> ulkeRepository, IBaseRepo<Sehir> sehirRepository)
        {
            _userManager = userManager;
            _aracRepo = aracRepo;
            _project = project;
            _mapper = mapper;
            _firmaSahis = firmaSahis;
            _ulkeRepository = ulkeRepository;
            _sehirRepository = sehirRepository;
            _isLemNewRepo = isLemNew;

        }

        [HttpPost]
        public async Task<IActionResult> islemKayit([FromBody]isLemNewDTO _dto)
        {
            if (ModelState.IsValid)
            {
                islem islem = new islem();
                islem.FirmaSahisId = _dto.FirmaSahisId;
                islem.Yil = _dto.Yil;
                islem.Tarih = _dto.Tarih;
                islem.AppUserID = _dto.AppUserID;
                islem.No = _isLemNewRepo.GetNextFisNo();
                islem.Statu = Statu.Active;

                List<islemD> islemDList = new List<islemD>();
                foreach (islemNewDetailDTO item in _dto.Detaylar)
                {
                    islemD islemDNew = new islemD();
                    islemDNew.islemTur = item.islemTur;
                    islemDNew.islemAciklama = item.islemAciklama;
                    islemDNew.MalzemeFiyat = item.MalzemeFiyat;
                    islemDNew.IscilikFiyat = item.iscilikFiyat;
                    islemDNew.ToplamFiyat = item.ToplamFiyat;
                    islemDNew.AppUserID = item.AppUserID;
                    islemDNew.IslemId = islem.ID;
                    islemDNew.BakimKM = item.BakimKM;
                    islemDNew.FirmaSahisId = item.FirmaSahisId;
                    islemDNew.AracId = item.AracId;

                    islemDList.Add(islemDNew);
                }
                _isLemNewRepo.islemSaveWithDetails(islem, islemDList);
            
                return Json("ok");
            }
            else
            {
                return BadRequest(ModelState);
            }
           
        }



    }
}
