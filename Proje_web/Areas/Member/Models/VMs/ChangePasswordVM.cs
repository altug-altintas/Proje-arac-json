using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Proje_web.Areas.Member.Models.VMs
{
    public class ChangePasswordVM
    {

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Eski Şifre")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre")]
        public string NewPassword { get; set; }
        
    }
}

