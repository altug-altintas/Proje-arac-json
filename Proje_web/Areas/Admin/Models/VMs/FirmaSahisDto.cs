using Proje_model.Models.Concrete;
using Proje_model.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Proje_web.Areas.Admin.Models.VMs
{
    public class FirmaSahisDto
    {

        public int ID { get; set; }
        public string Adi { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string VergiNo { get; set; }
        public string VergiDairesi { get; set; }
        public string TC { get; set; }
        public string PostaNo { get; set; }
        public string UlkeId { get; set; }
        public string SehirId { get; set; }
        public string Tur { get; set; }

        public Statu Statu  { get; set; }
        public ICollection<AracDto> Araclar { get; set; }
        public ICollection<islemDto> Islemler { get; set; }

        public IEnumerable<SelectListItem> Ulkeler { get; set; }
        public IEnumerable<SelectListItem> Sehirler { get; set; }

    }
}
