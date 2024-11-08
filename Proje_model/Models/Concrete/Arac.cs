using Proje_model.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.Models.Concrete
{
    public class Arac :BaseEntity
    {
        public string Plaka { get; set; } // Plaka, birincil anahtar olacak
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

        public string AppUserID { get; set; }   // idendtiy  küpüphanesinden olduğu için  int değil string aldık
        public AppUser AppUser { get; set; }


        public int FirmaSahisId { get; set; } 

        // Nav prop
        public FirmaSahis FirmaSahis { get; set; }
        public ICollection<islemD> IslemDetaylar { get; set; }



    }
}
