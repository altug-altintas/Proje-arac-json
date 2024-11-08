using Proje_model.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.Models.Concrete
{
    public class islem : BaseEntity
    {
        public int Yil { get; set; }
        public string No { get; set; }
        public int FirmaSahisId { get; set; }
        public DateTime Tarih { get; set; }

        public string AppUserID { get; set; }   // idendtiy  küpüphanesinden olduğu için  int değil string aldık
        public AppUser AppUser { get; set; }

        // Nav prop

        public FirmaSahis FirmaSahis { get; set; }
        public ICollection<islemD> IslemDetaylar { get; set; }
    }
}


