using System.ComponentModel.DataAnnotations;

namespace Proje_web.Models.DTOs
{
    public class LoginDTO
    {



        [Required(ErrorMessage ="Bu alan boş bırakılamaz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [DataType(DataType.Password)]       
        public string Password { get; set; }

        
        public string ReturnUrl { get; set; }  // bu alan gette gitmek istediği bir yer varsa doldurup göndereceğiz ki posttaki işlemler doğruysa buradak Url üzerinden doğrudan gitmek istediği yere yönlendirelim

                                              //  Aynı kişiyi updata ederken ID bilgisini postta kullanmaküzere gömmemiz gibi
       
    }
}
