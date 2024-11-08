using System.Collections.Generic;

namespace Proje_web.Areas.Member.Models.VMs
{
    public class SehirDto
    {
        public int SehirPlaka { get; set; }
        public string SehirAdi { get; set; }
        public int SehirPostaKod { get; set; }
        public string UlkeId { get; set; }
        public UlkeDto Ulke { get; set; }
        public ICollection<FirmaSahisDto> FirmaSahislar { get; set; }
    }
}
