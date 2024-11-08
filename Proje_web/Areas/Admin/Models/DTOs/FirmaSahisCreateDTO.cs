using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proje_web.Areas.Admin.Models.DTOs
{
    [Area("admin")]
    public class FirmaSahisCreateDTO
    {
        [Required]
        public string Adi { get; set; }

        public string Adres { get; set; }

        [Required]
        public string Telefon { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string VergiNo { get; set; }

        public string VergiDairesi { get; set; }

        public string TC { get; set; }

        public string PostaNo { get; set; }

        [Required]
        public int UlkeId { get; set; }

        [Required]
        public int SehirId { get; set; }
        public string Tur { get; set; }

        public IEnumerable<SelectListItem> Ulkeler { get; set; }
        public IEnumerable<SelectListItem> Sehirler { get; set; }
    }
}
