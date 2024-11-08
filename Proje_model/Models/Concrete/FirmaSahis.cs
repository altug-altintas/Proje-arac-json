using Proje_model.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.Models.Concrete
{
    public class FirmaSahis :BaseEntity
    {      
        public string Adi { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string VergiNo { get; set; }
        public string VergiDairesi { get; set; }
        public string TC { get; set; }
        public string PostaNo { get; set; }
        public string Tur { get; set; }

        public string AppUserID { get; set; }   // idendtiy  küpüphanesinden olduğu için  int değil string aldık
        public AppUser AppUser { get; set; }

        // Foreign Keys
        public int UlkeId { get; set; }
        public int SehirId { get; set; }

        // Navigation Properties
        public  Ulke Ulke  { get; set; }
        public  Sehir Sehir { get; set; }

        public ICollection<Arac> Araclar { get; set; }

       public ICollection<islem> islems { get; set; }

        public ICollection<islemD> IslemDetaylar { get; set; }

    }
}
