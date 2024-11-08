namespace Proje_web.Areas.Member.Models.VMs
{
    public class UpdateIslemDto
    {
        public int ID { get; set; }
        public decimal MalzemeFiyat { get; set; }
        public decimal IscilikFiyat { get; set; }
        public decimal ToplamFiyat { get; set; }
        public string islemAciklama { get; set; }
        public int BakimKM { get; set; }
        public string islemTur { get; set; }
        public int AracId { get; set; }

    }
}
