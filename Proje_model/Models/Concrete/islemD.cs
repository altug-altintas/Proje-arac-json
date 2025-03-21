﻿using Proje_model.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.Models.Concrete
{
    public class islemD :BaseEntity
    {

     
        public decimal MalzemeFiyat { get; set; }
        public decimal IscilikFiyat { get; set; }
        public decimal ToplamFiyat { get; set; }

        public string islemAciklama { get; set; }

        public string islemTur { get; set; }

        public int BakimKM { get; set; } 




        public string AppUserID { get; set; }   // idendtiy  küpüphanesinden olduğu için  int değil string aldık
        public AppUser AppUser { get; set; }
        // Nav prop 
        public int IslemId { get; set; }
        public islem islem { get; set; }

        public int FirmaSahisId { get; set; }
        public FirmaSahis? FirmaSahis { get; set; }

        public int AracId { get; set; }
        public Arac? Arac { get; set; }

    }
}
