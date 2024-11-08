namespace Proje_web.Areas.Member.Models.VMs
{
    public class IslemDListVM
    {
        public int ID { get; set; }
        public decimal MalzemeFiyat { get; set; }
        public decimal IscilikFiyat { get; set; }
        public decimal ToplamFiyat { get; set; }
        public string IslemAciklama { get; set; }
        public string IslemTur { get; set; }
        public int BakimKM { get; set; }
        public int AracId { get; set; }
        public int FirmaSahisId { get; set; }
        public string AracPlaka { get; set; }
        public string FirmaAd { get; set; }

    }
}
