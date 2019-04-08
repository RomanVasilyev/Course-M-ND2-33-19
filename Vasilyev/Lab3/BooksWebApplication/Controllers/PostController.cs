﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Http.BooksLibrary.Data.Contracts.Entities;
using Http.BooksLibrary.Domain.Contracts;
using Http.BooksLibrary.Domain.Contracts.ViewModels;

namespace BooksWebApplication.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        // GET: Get
        public ActionResult Index()
        {
            var books = postService.GetAll();
            return View(books);
        }

        // GET: Get/Details/5
        public ActionResult Details(int id)
        {
            var viewModel = postService.Get(id);
            if (viewModel == null) return new HttpNotFoundResult();
            PrepareView(viewModel);
            return View(viewModel);
        }

        // GET: Get/Create
        public ActionResult Create()
        {
            BookViewModel viewModel = new BookViewModel();
            if (viewModel == null) return new HttpNotFoundResult();
            PrepareView(viewModel);
            return View(viewModel);
        }

        // POST: Get/Create
        [HttpPost]
        public ActionResult Create(BookViewModel viewModel)
        {
            try
            {
                postService.Save(viewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Edit/5
        //[HttpPost]
        public ActionResult Edit(int id)
        {
            var viewModel = postService.Get(id);
            if (viewModel == null) return new HttpNotFoundResult();
            PrepareView(viewModel);
            ViewData["PageTitle"] = viewModel.Title;
            ViewBag.PageTitleLower = viewModel.Title.ToLower();
            Session["Created"] = DateTime.Now;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel viewModel)
        {
            postService.Save(viewModel);
            var title = ViewData["BookTitle"];
            var author = TempData["Author"];
            var created = Session["Created"];
            return RedirectToAction("Edit", new { id = viewModel.Id });
        }

        // GET: Get/Delete/5
        public ActionResult Delete(int id)
        {
            postService.Delete(id);
            return RedirectToAction("Index");
        }

        private void PrepareView(BookViewModel viewModel)
        {
            var genres = new List<SelectListItem>();
            var names = Enum.GetNames(typeof(Genre));
            for (int i = 0; i < names.Length; i++)
            {
                genres.Add(new SelectListItem { Value = i.ToString(), Text = names[i] });
            }
            var langs = new List<SelectListItem>();
            var nlangs = Enum.GetNames(typeof(Langs));
            for (int i = 0; i < nlangs.Length; i++)
            {
                langs.Add(new SelectListItem { Value = i.ToString(), Text = nlangs[i] });
            }
            viewModel.AvailableLanguages = langs;
            viewModel.AvailableGenres = genres;
        }

    }
}
