using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proje_web.Areas.Member.Models.DTOs
{
    public class FirmaSahisUpdateDTO
    {
        public int ID { get; set; }

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
        public string Tur { get; set; }
        public string PostaNo { get; set; }

        [Required]
        public int UlkeId { get; set; }

        [Required]
        public int SehirId { get; set; }


        public Proje_model.Models.Enums.Statu Statu { get; set; }

        public IEnumerable<SelectListItem> Ulkeler { get; set; }

        public IEnumerable<SelectListItem> Sehirler { get; set; }
    }
}
