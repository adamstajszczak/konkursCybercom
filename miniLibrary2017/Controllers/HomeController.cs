using miniLibrary2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace miniLibrary2017.Controllers
{
    public class HomeController : Controller
    {
        DBminiLibrary db = new DBminiLibrary();
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            //db.TestDate();
            var dane = db.tabBook.FirstOrDefault();
            return View(dane);
            //return View();
          
        }
    }
}