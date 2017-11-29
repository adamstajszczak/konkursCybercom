using miniLibrary2017.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Data.Entity;

namespace miniLibrary2017.Controllers
{
    public class EditBookController : Controller
    {
        
        DBminiLibrary db = new DBminiLibrary();
        // GET: EditBook
        public ActionResult Index()
        {

            var viewModel =(from bk in db.tabBook
            join ar in db.tabAuthor on bk.IdAuthor equals ar.Id        
            orderby bk.Title
            select new ViewBook { book = bk, author = ar }).ToList();
            return View(viewModel);


        }
        [Authorize]
        [HttpGet]
        public ActionResult EditBook(int? id)
        {
            Book toEdit;
            if (id != null)
            {
                toEdit = db.tabBook.Find(id);
                if (toEdit == null)
                    return new HttpNotFoundResult();
            }
            else
            {
                toEdit = new Models.Book();
                toEdit.YearOfPublish = DateTime.Now;
            }

            List<SelectListItem> listaZPoz0 = new List<SelectListItem>();
            listaZPoz0.Add(new SelectListItem { Text = "wybierz autora", Value = "" });
            List<SelectListItem> listaAutorow = (from item in db.tabAuthor
                                                 orderby item.LastName
                                                 select new SelectListItem { Value = item.Id.ToString(), Text = item.LastName +" "+ item.FirstName }).ToList();
            listaZPoz0.AddRange(listaAutorow);

            ViewBag.Autorzy = listaZPoz0;
            return View(toEdit);
        }

        //zapis do zmianie
        [Authorize]
        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            if (ModelState.IsValid)
            {
                Book toEdit;
                if (book.Id == 0)
                {
                    toEdit = new Models.Book();
                    
                    db.tabBook.Add(book);
                    
                }
                else
                {
                    toEdit = db.tabBook.Find(book.Id);
                    if (toEdit != null)
                    {
                        try
                        {
                            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(CS))
                            {
                                SqlCommand cmd = new SqlCommand("BookEdit", con);
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@Id", toEdit.Id);
                                cmd.Parameters.AddWithValue("@Title", toEdit.Title);
                                cmd.Parameters.AddWithValue("@YearOfPublish", toEdit.YearOfPublish);
                                cmd.Parameters.AddWithValue("@ISBN", toEdit.ISBN);
                                cmd.Parameters.AddWithValue("@IdAuthor", toEdit.IdAuthor);

                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (Exception e)
                        {
                            Response.Write(e.Message);
                        }
                        toEdit.IdAuthor = book.IdAuthor;
                        toEdit.Title = book.Title;
                        toEdit.ISBN = book.ISBN;
                        toEdit.YearOfPublish = book.YearOfPublish;
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> listaZPoz0 = new List<SelectListItem>();
            listaZPoz0.Add(new SelectListItem { Text = "wybierz autora", Value = "" });
            List<SelectListItem> listaAutorow = (from item in db.tabAuthor
                                                 orderby item.LastName
                                                 select new SelectListItem { Value = item.Id.ToString(), Text = item.LastName +" "+ item.FirstName}).ToList();
            listaZPoz0.AddRange(listaAutorow);

            ViewBag.Autorzy = listaZPoz0;
            return View();
        }

        //kasowanie książki
        [Authorize]
        [HttpGet]
        public ActionResult DeleteBook(int id)
        {
            Book toDelete = db.tabBook.Find(id);
            if (toDelete == null)
                return new HttpNotFoundResult();
            return View(toDelete);
        }
        [Authorize]
        [HttpPost]
        public ActionResult DeleteBook(Book book)
        {
            Book toDelete = db.tabBook.Find(book.Id);
            ///
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("BookDelete", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", toDelete.Id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            } catch (Exception e)
            {
                Response.Write(e.Message);
            }
            ////
            //db.tabBook.Remove(toDelete);
            db.SaveChanges();
             return RedirectToAction("Index");
            
        }
    }
}