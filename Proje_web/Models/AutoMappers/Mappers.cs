using AutoMapper;
using Proje_web.Areas.Admin.Models.DTOs;
using Proje_web.Areas.Admin.Models.VMs;
using Proje_web.Areas.Member.Models.DTOs;
using Proje_web.Areas.Member.Models.VMs;
using Proje_web.Models.DTOs;
using Proje_model.Models.Concrete;

namespace Proje_web.Models.AutoMappers
{
    public class Mappers : Profile
    {

        public Mappers()
        {
            // maplemeler
            CreateMap<RegisterDTO, AppUser>();    // registerDTO --- > appUser nesnesi teslim etmeslisin


            CreateMap<UserUpdateDTO, AppUser>().ReverseMap();

            CreateMap<Areas.Admin.Models.VMs.UlkeDto, Ulke>().ReverseMap();
            CreateMap<Areas.Member.Models.VMs.UlkeDto, Ulke>().ReverseMap();

            CreateMap<Areas.Admin.Models.VMs.SehirDto, Sehir>().ReverseMap();
            CreateMap<Areas.Member.Models.VMs.SehirDto, Sehir>().ReverseMap();


            CreateMap<Areas.Admin.Models.VMs.FirmaSahisDto, FirmaSahis>().ReverseMap();
            CreateMap<Areas.Member.Models.VMs.FirmaSahisDto, FirmaSahis>().ReverseMap();

            CreateMap<Areas.Admin.Models.DTOs.FirmaSahisCreateDTO, FirmaSahis>().ReverseMap();
            CreateMap<Areas.Member.Models.DTOs.FirmaSahisCreateDTO, FirmaSahis>().ReverseMap();

         CreateMap<Areas.Member.Models.VMs.isLemNewDTO, islemD>().ReverseMap();


            CreateMap<Areas.Admin.Models.DTOs.FirmaSahisUpdateDTO, FirmaSahis>().ReverseMap();
            CreateMap<Areas.Member.Models.DTOs.FirmaSahisUpdateDTO, FirmaSahis>().ReverseMap();


            CreateMap<Areas.Admin.Models.VMs.islemDto, islem>().ReverseMap();
            CreateMap<Areas.Member.Models.VMs.islemDto, islem>().ReverseMap();

            CreateMap<Areas.Admin.Models.VMs.islemDDto, islemD>().ReverseMap();
            CreateMap<Areas.Member.Models.VMs.islemDDto, islemD>().ReverseMap();

            CreateMap<Arac, Areas.Admin.Models.DTOs.AracCreateDTO>().ReverseMap();
            CreateMap<Arac, Areas.Member.Models.DTOs.AracCreateDTO>().ReverseMap();

          
            CreateMap<Arac, Areas.Admin.Models.VMs.AracDto>().ReverseMap(); 
            CreateMap<Arac, Areas.Member.Models.VMs.AracDto>().ReverseMap(); 

            CreateMap<Areas.Admin.Models.VMs.CreateislemDto, islem>().ReverseMap();
            CreateMap<Areas.Member.Models.VMs.CreateislemDto, islem>().ReverseMap();

            CreateMap<Areas.Admin.Models.VMs.UpdateIslemDto, islemD>().ReverseMap(); 
            CreateMap<Areas.Member.Models.VMs.UpdateIslemDto, islemD>().ReverseMap(); 


        }
    }
}
