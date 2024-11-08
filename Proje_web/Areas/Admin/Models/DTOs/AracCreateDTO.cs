using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Proje_web.Areas.Admin.Models.DTOs
{
    public class AracCreateDTO
    {

        public string Plaka { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Yil { get; set; }
        public string SasiNo { get; set; }
        public string YakitTur { get; set; }
        public string Renk { get; set; }
        public int BakimKM { get; set; }
        public decimal MotorHacim { get; set; }
        public decimal MotorBeygir { get; set; }
        public int Km { get; set; }
        public DateTime SonBakim { get; set; }
        public DateTime SiradakiBakim { get; set; }
        public int FirmaSahisId { get; set; }
        public List<SelectListItem> FirmaSahisler { get; set; }


    }
}
