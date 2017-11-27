using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace miniLibrary2017.Models
{
    public class ShadowBook
    {
        [Display(Name = "Oryginalne Id")]
        public int ShadowId { get; set; }
        [Display(Name = "Data wstawienia")]
        public DateTime ShadowTimestamp { get; set; }
        [StringLength(1)]
        [Display(Name = "Metoda wstawienia")]
        public string ShadowOption { get; set; }
        public int Id { get; set; }
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Rok publiakcji")]
        public DateTime YearOfPublish { get; set; }
        public long ISBN { get; set; }
        [Display(Name = "Author")]
        public int IdAuthor { get; set; }
    }
}