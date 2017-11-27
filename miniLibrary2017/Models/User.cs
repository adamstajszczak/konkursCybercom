using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace miniLibrary2017.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name="Login: ")]
        public string Login { get; set; }
        [Display(Name ="Hasło: ")]
        [DataType(DataType.Password)]
        //[StringLength(-1, MinimumLength =5)]
        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public bool RoleId { get; set; }
    }
}