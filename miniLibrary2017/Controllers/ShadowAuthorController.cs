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
    public class ShadowAuthorController : Controller
    {
        DBminiLibrary db = new DBminiLibrary();
        // GET: ShadowBook
        [Authorize]
        public ActionResult ShadowAuthor()
        {
            var listShAuthor = (from item in db.tabShAuthor

                              select item).ToList();
            return View(listShAuthor);
        }




        ///////////////////////////////////////////////
        //kasowanie autora
        [HttpGet]
        [Authorize]
        public ActionResult ReturnShadowAuthor(int shadowid)
        {
            ShadowAuthor toReturn = db.tabShAuthor.Find(shadowid);
            if (toReturn == null)
                return new HttpNotFoundResult();
            return View(toReturn);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ReturnShadowAuthor(ShadowAuthor shadowAuthor)
        {
            ShadowAuthor toReturn = db.tabShAuthor.Find(shadowAuthor.ShadowId);
            ///
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("ShadowReturnAuthor", con);
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
            return RedirectToAction("ShadowAuthor");

        }




    }
}