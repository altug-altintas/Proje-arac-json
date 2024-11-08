using Proje_model.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.Models.Concrete
{
    public class Sehir:BaseEntity
    {
        public int sehir_plaka { get; set; }
        public string sehir_adi { get; set; }
        public int sehir_posta_kod { get; set; }



        public int UlkeId { get; set; }

        // Nav prop
        public Ulke Ulke { get; set; }
        public ICollection<FirmaSahis> FirmaSahislar { get; set; }

    }
}
