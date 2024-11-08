using System.ComponentModel.DataAnnotations;
using System;

namespace Proje_web.Areas.Admin.Models.VMs
{
    public class CreateislemDto
    {
        public int Id { get; set; }

        public int Yil { get; set; }

        public string No { get; set; }

        [Required(ErrorMessage = "Firma Sahis seçilmelidir.")]
        public string FirmaSahisId { get; set; }



        [Required(ErrorMessage = "Tarih girilmelidir.")]
        public DateTime Tarih { get; set; }


    }
}
