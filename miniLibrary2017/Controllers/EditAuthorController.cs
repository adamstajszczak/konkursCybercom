using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using miniLibrary2017.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace miniLibrary2017.Controllers
{
    public class EditAuthorController : Controller
    {
        DBminiLibrary db = new DBminiLibrary();
        // GET: EditAuthor
        public ActionResult Index()
        {
            var listAuthor = (from item in db.tabAuthor
                              orderby item.LastName
                              select item).ToList();
            return View(listAuthor);
        }

        // edycja i dodaj nowy
        [HttpGet]

        public ActionResult EditAuthor(int? id)
        {
            Author toEdit;
            if( id!= null) //sprawdzanie czy jest
            {
                toEdit = db.tabAuthor.Find(id);
                if (toEdit == null)
                    return new HttpNotFoundResult();
            }
            else //dodanie Autora
            {
                toEdit = new Author();
            }
            return View(toEdit);
        }
        //zwracanie po zmianie
        [HttpPost]
        public ActionResult EditAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                Author toEdit;
                if (author.Id == 0)
                {
                    toEdit = new Author();
                    db.tabAuthor.Add(author);
                }
                else
                {
                    toEdit = db.tabAuthor.Find(author.Id);
                    if(toEdit != null)
                    {
                        try
                        {
                            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(CS))
                            {
                                SqlCommand cmd = new SqlCommand("AuthorEdit", con);
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@Id", toEdit.Id);
                                cmd.Parameters.AddWithValue("@Fname", toEdit.FirstName);
                                cmd.Parameters.AddWithValue("@Lname", toEdit.LastName);
                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (Exception e)
                        {
                            Response.Write(e.Message);
                        }


                        toEdit.LastName = author.LastName;
                        toEdit.FirstName = author.FirstName;
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //kasowanie Autora
        [HttpGet]
        public ActionResult DeleteAuthor(int id)
        {
            Author toDelete = db.tabAuthor.Find(id);
            if (toDelete == null)
                return new HttpNotFoundResult();
            return View(toDelete);
        }

        //zapis zakascji
        [HttpPost]
        public ActionResult DeleteAuthor(Author author)
        {
            Author toDelete = db.tabAuthor.Find(author.Id);
            if (toDelete != null)
            {
                try
                {
                    string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        SqlCommand cmd = new SqlCommand("AuthorDelete", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", toDelete.Id);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Response.Write(e.Message);
                }

                //db.tabAuthor.Remove(toDelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}