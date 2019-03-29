using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksWebApplication.Controllers
{
    public class GetController : Controller
    {
        //public BookRepository Books { get; set; }
        // GET: Get
        public ActionResult Index()
        {
            var books = new BookRepository();
            ViewBag.Books = books.Books;
            return View(books.Books);
        }

        // GET: Get/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Get/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Get/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Get/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Get/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Get/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Get/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
