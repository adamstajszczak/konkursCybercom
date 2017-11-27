using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace miniLibrary2017.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Podaj imię autora")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Nazwisko autora")]
        public string LastName { get; set; }
    }
}