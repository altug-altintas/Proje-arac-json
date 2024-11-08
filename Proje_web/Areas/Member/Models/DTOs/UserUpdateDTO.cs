using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proje_web.Areas.Member.Models.DTOs
{
    public class UserUpdateDTO
    {
        //public int ID { get; set; }


        [Required(ErrorMessage = "Bu alanboş bırakılamaz")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Bu alanboş bırakılamaz")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Bu alanboş bırakılamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [NotMapped]
        [Required(ErrorMessage = "Lütfen bir resim seçiniz")]

        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }



        [Required(ErrorMessage = "Bu alanboş bırakılamaz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Bu alanboş bırakılamaz")]
        public string UserName { get; set; }



        [Required(ErrorMessage = "Bu alanboş bırakılamaz")]
        public string Statu { get; set; }
    }
}
