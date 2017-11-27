using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;

namespace miniLibrary2017.Models
{
    public class DBminiLibrary : DbContext
    {
        DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
        public DbSet<Book> tabBook { get; set; }
        public DbSet<Author> tabAuthor { get; set; }

        public DbSet<User> tabUser { get; set; }
        public DbSet<ShadowBook> tabShBook { get; set; }
        public DbSet<ShadowAuthor> tabShAuthor { get; set; }

        public void TestDate()
        {
            this.Database.Delete();
            this.tabBook.Add(new Models.Book() { Title = "Dziewczęta z Szanghaju", YearOfPublish = Convert.ToDateTime("12/01/2016", usDtfi), IdAuthor = 1, ISBN = 7586059685739 });

            this.tabBook.Add(new Models.Book() { Title = "Lśnienie", YearOfPublish = Convert.ToDateTime("08/22/1990", usDtfi), IdAuthor = 2, ISBN = 2839405768549 });
            this.tabBook.Add(new Models.Book() { Title = "Kostnica", YearOfPublish = Convert.ToDateTime("12/16/1994", usDtfi), IdAuthor = 3, ISBN = 9405769402134 });

            this.tabBook.Add(new Models.Book() { Title = "Ania z Zielonego Wzgórza", YearOfPublish = Convert.ToDateTime("12/14/1900", usDtfi), IdAuthor = 4, ISBN = 28594037485 });
            this.tabBook.Add(new Models.Book() { Title = "Mitologia Nordycka", YearOfPublish = Convert.ToDateTime("11/19/2015", usDtfi), IdAuthor = 5, ISBN = 38495047361 });
            this.tabBook.Add(new Models.Book() { Title = "Chińskie Lalki", YearOfPublish = Convert.ToDateTime("1/11/2016", usDtfi), IdAuthor = 1, ISBN = 4758403758 });
            this.tabBook.Add(new Models.Book() { Title = "Drapieżcy", YearOfPublish = Convert.ToDateTime("05/25/1998", usDtfi), IdAuthor = 3, ISBN = 9106902134 });
            this.tabBook.Add(new Models.Book() { Title = "Cmentarz dla Zwierząt", YearOfPublish = Convert.ToDateTime("12/02/1989", usDtfi), IdAuthor = 2, ISBN = 4758694021 });
            this.tabBook.Add(new Models.Book() { Title = "Miasteczko Salem", YearOfPublish = Convert.ToDateTime("1/10/1985", usDtfi), IdAuthor = 2, ISBN = 9405769402134 });
            this.tabBook.Add(new Models.Book() { Title = "Podróżnik WC", YearOfPublish = Convert.ToDateTime("7/09/2015", usDtfi), IdAuthor = 6, ISBN = 1405740213 });
            


            this.tabAuthor.Add(new Models.Author { FirstName = "Lisa", LastName= "See" });
            this.tabAuthor.Add(new Models.Author { FirstName = "Stephen", LastName = "King" });
            this.tabAuthor.Add(new Models.Author { FirstName = "Graham", LastName = "Masterton" });
            this.tabAuthor.Add(new Models.Author { FirstName = "Lucy", LastName = "Mound Montgomery" });
            this.tabAuthor.Add(new Models.Author { FirstName = "Neil", LastName = "Gaiman" });
            this.tabAuthor.Add(new Models.Author { FirstName = "Wojciech", LastName = "Cejrowski" });

            //this.tabUser.Add(new Models.User { Login = "admin", Password = "CLp3LOYSL20V1uqAjIyYEqyOKf1woRKR4NV3V59eLFnjJqv/G+sgvCeKB4roldeYfwFqRD+EqIQEaA+/FeLUfg==", PasswordSalt= "100000.OByZjt1t/1xsiY0j4IQMT5eVbTE2wYOgrzBxesrfJ0nIEw==" });
            this.tabShBook.Add(new Models.ShadowBook { ShadowTimestamp = Convert.ToDateTime("12/01/2011", usDtfi), ShadowOption="d", ShadowId=999, YearOfPublish= Convert.ToDateTime("12/01/2011", usDtfi), ISBN=475860473812, IdAuthor=1, Title="TestShadow" });

            this.tabShAuthor.Add(new Models.ShadowAuthor { ShadowTimestamp = Convert.ToDateTime("12/01/2011", usDtfi), ShadowOption = "d", ShadowId = 999, FirstName="Dominika", LastName="Żywica" });

            this.SaveChanges();
        }
    }
}