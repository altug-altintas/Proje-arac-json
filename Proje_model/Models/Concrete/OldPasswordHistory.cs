using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proje_model.Models.Concrete
{
    public class OldPasswordHistory
    {

        [Key]
        public int ID { get; set; }
        public string UserId { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
