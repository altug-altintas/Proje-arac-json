using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje_web.Models.DTOs
{
    public class RegisterDTO
    {


        [Required(ErrorMessage ="Bu alanboş bırakılamaz")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Bu alanboş bırakılamaz")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Bu alanboş bırakılamaz")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Bu alanboş bırakılamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Bu alanboş bırakılamaz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

       
        [Required(ErrorMessage = "Bu alanboş bırakılamaz")]
        [NotMapped]    //maplama eşleştirme yapma
        public IFormFile Image { get; set; }
       



    }
}
