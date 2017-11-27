using miniLibrary2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace miniLibrary2017.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(miniLibrary2017.Models.User user)
        {
            if (ModelState.IsValid)
            {
                if(IsValid(user.Login, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Login, false);
                   


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Podane danę są nieprawidłowe.");
                }
            }
            

            return View(user);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(miniLibrary2017.Models.User user)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DBminiLibrary())
                {
                    var crypto = new SimpleCrypto.PBKDF2();

                    var entryPass = crypto.Compute(user.Password);
                    User sysUser = new User();
                    //var sysUser = db.tabUser.Create();

                    sysUser.Login = user.Login;
                    sysUser.Password = entryPass;
                    sysUser.PasswordSalt = crypto.Salt;

                    db.tabUser.Add(sysUser);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");

                }
                
            }


            return View(user);
        }




        private bool IsValid(string login, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();

            bool isValid = false;

            using (var db = new DBminiLibrary())
            {
                var user = db.tabUser.FirstOrDefault(u => u.Login == login);

                if(user != null)
                {
                    if(user.Password == crypto.Compute(password, user.PasswordSalt))
                        {
                            isValid = true;
                        }
                }
            }

            return isValid;
        }
    }
}