using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace miniLibrary2017.Models
{
    

    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Podaj tytuł książki")]
        [Display(Name ="Tytuł")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Proszę podaj rok z przedziału 1900 - 2017")]
        [DataType(DataType.Date)]
        [DateRange("1900/01/01", "2017/12/31", ErrorMessage ="Podaj datę z przedziału {1} - {2}")]
        [Display(Name ="Data publikacji")]
        public DateTime YearOfPublish { get; set; }
        [Required(ErrorMessage ="Proszę podać 10 lub 13 cyfrowy numer ISBN")]
        [Range(1000000000, 9999999999999, ErrorMessage ="Podany numer jest nieprawidlowy")]
        [Display(Name ="Numer ISBN")]
        public long ISBN { get; set; }
        [Display(Name ="Autor")]
        public int IdAuthor { get; set; }
    }
}