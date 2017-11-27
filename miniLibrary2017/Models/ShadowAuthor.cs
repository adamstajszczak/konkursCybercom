using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace miniLibrary2017.Models
{
    public class ShadowAuthor
    {
        [Display(Name = "Oryginalne ID")]
        public int ShadowId { get; set; }
        [Display(Name = "Data wstawienia")]
        public DateTime ShadowTimestamp { get; set; }
        [StringLength(1)]
        [Display(Name = "Metoda wstawienia")]
        public string ShadowOption { get; set; }
        public int Id { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
    }
}