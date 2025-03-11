using System;
using System.Collections.Generic;

namespace Proje_web.Areas.Member.Models.VMs
{
    public class isLemNewDTO
    {
        public int AracId { get; set; }
        public int FirmaSahisId { get; set; }
        public string AppUserID { get; set; }
        public int Yil { get; set; }
        public DateTime Tarih { get; set; }
       

        public List<islemNewDetailDTO> Detaylar { get; set; }

    }
    public class islemNewDetailDTO
    {


        public string islemAciklama { get; set; }
        public decimal iscilikFiyat { get; set; }
        public decimal MalzemeFiyat { get; set; }
        public decimal ToplamFiyat { get; set; }
        public int BakimKM { get; set; }
        public string islemTur { get; set; }
        public string AppUserID { get; set; }
        public int FirmaSahisId { get; set; }
        public int AracId { get; set; }

    }
}
