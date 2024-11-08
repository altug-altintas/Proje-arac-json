using System;

namespace Proje_web.Areas.Member.Models.VMs
{
    public class islemDto
    {
        public int Id { get; set; }

        public int Yil { get; set; }

        public string No { get; set; }


        public string FirmaSahisId { get; set; }

        public DateTime Tarih { get; set; }
    }
}
