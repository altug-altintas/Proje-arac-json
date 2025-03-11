using Proje_model.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.Models.Concrete
{
    public class isLemNew : BaseEntity
    {
        public int detayId { get; set; }    
        public decimal MalzemeFiyat { get; set; }
        public decimal iscilikFiyat { get; set; }
        public decimal ToplamFiyat { get; set; }

        public string islemAciklama { get; set; }

        public string islemTur { get; set; }

        public int BakimKM { get; set; }



        // Nav prop  
        public string AppUserID { get; set; }   
        public AppUser AppUser { get; set; }

        public int FirmaSahisId { get; set; }
        public FirmaSahis FirmaSahis { get; set; }

        public int AracId { get; set; }
        public Arac Arac { get; set; }
    }
}
