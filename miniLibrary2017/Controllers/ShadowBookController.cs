using miniLibrary2017.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace miniLibrary2017.Controllers
{
    public class ShadowBookController : Controller
    {
        DBminiLibrary db = new DBminiLibrary();
        // GET: ShadowBook
        [Authorize]
        public ActionResult ShadowBook()
        {
            var listShBook = (from item in db.tabShBook

                              select item).ToList();
            return View(listShBook);
        }




        ///////////////////////////////////////////////
        //kasowanie książki
        [HttpGet]
        [Authorize]
        public ActionResult ReturnShadowBook(int shadowid)
        {
            ShadowBook toReturn = db.tabShBook.Find(shadowid);
            if (toReturn == null)
                return new HttpNotFoundResult();
            return View(toReturn);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ReturnShadowBook(ShadowBook shadowBook)
        {
            ShadowBook toReturn = db.tabShBook.Find(shadowBook.ShadowId);
            ///
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("ShadowReturnD", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ShadowID", toReturn.ShadowId);
                    cmd.Parameters.AddWithValue("@Id", toReturn.Id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
            ////
            //db.tabBook.Remove(toDelete);
            db.SaveChanges();
            return RedirectToAction("ShadowBook");

        }




    }
}