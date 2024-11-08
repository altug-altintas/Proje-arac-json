using Proje_model.Models.Concrete;
using System;

namespace Proje_web.Areas.Admin.Models.VMs
{
    public class islemDDto
    {
       // public int Id { get; set; }
       
        public decimal MalzemeFiyat { get; set; }
        public decimal IscilikFiyat { get; set; }
        public decimal ToplamFiyat { get; set; }
        public string islemTur { get; set; }
        public string islemAciklama { get; set; }
        public int BakimKM { get; set; }
        public int FirmaSahisId { get; set; }
        public int IslemId { get; set; }
        public int AracId { get; set; }

       
    }
}
