using Proje_model.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.Models.Concrete
{
    public class Ulke : BaseEntity
    {

        public string ulke_kodu { get; set; }
        public string ulke_adi { get; set; }

        // Nav prop
        public ICollection<FirmaSahis> FirmaSahislar { get; set; }
        public ICollection<Sehir> Sehirler { get; set; }  

    }
}
