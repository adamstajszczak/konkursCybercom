using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace miniLibrary2017.Controllers
{
    public class RootController : Controller
    {
        // GET: Root
        public ActionResult AdminError()
        {
            return View();
        }
        public ActionResult OnlyAdmin()
        {
            return View();
        }
    }
}