using Proje_web.Areas.Admin.Models.VMs;
using System.Collections.Generic;
using System;

namespace Proje_web.Areas.Member.Models.VMs
{
    public class AracDto
    {
        public string Plaka { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Yil { get; set; }
        public string SasiNo { get; set; }
        public string YakitTur { get; set; }
        public string Renk { get; set; }
        public decimal MotorHacim { get; set; }
        public decimal MotorBeygir { get; set; }
        public string FirmaSahisId { get; set; }
        public int BakimKM { get; set; }
        public int Km { get; set; }
        public DateTime SonBakim { get; set; }
        public DateTime SiradakiBakim { get; set; }
        public decimal MalzemeFiyat { get; set; }
        public decimal IscilikFiyat { get; set; }
        public decimal ToplamFiyat { get; set; }

        //nav prop
        public FirmaSahisDto FirmaSahis { get; set; }
        public ICollection<islemDDto> IslemDetaylar { get; set; }
    }
}
