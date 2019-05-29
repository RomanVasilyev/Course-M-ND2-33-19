using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Http.News.Data.Contracts;
using Http.News.Domain.Contracts.Dtos;
using Http.News.Domain.Contracts.ViewModels;
using Http.News.Domain.Services;
using Http.News.Infrastructure;
using ITNewsWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ITNewsWeb.Controllers
{
    public class HomeController : Controller
    {
        private INewsService NewsService;

        public HomeController(INewsService newsService)
        {
            NewsService = newsService;
        }

        //[Authorize]
        //public ActionResult Index()
        //{
        //    IList<string> roles = new List<string> { "Роль не определена" };
        //    ApplicationUserManager userManager = HttpContext.GetOwinContext()
        //        .GetUserManager<ApplicationUserManager>();
        //    ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
        //    if (user != null)
        //        roles = userManager.GetRoles(user.Id);
        //    return View(roles);
        //}

        public ActionResult Index()
        {
            var viewModel = NewsService.BuildHomePageViewModel(6);
            return View(viewModel);
        }

        [Route("Home/Details/{title?}/{categoryId:int}/{itemId:int}")]
        public ActionResult Details(int categoryId, int itemId, string title = null)
        {
            var viewModel = NewsService.BuildItemDetailsViewModel(categoryId, itemId);

            return View(viewModel);
        }

        [Route("Home/Category/{name}/{id:int}")]
        public ActionResult Category(string name, int id)
        {
            var viewModel = NewsService.BuildCategoryPageViewModel(id);
            return View(viewModel);
        }

        [AcceptVerbs("post")]
        public ActionResult Rate(FormCollection form)
        {
            var rate = Convert.ToInt32(form["Score"]);
            var id = Convert.ToInt32(form["ArticleID"]);
            if (Request.Cookies["rating" + id] != null)
                return Content("false");
            Response.Cookies["rating" + id].Value = DateTime.Now.ToString();
            Response.Cookies["rating" + id].Expires = DateTime.Now.AddYears(1);
            ItemDetailsViewModel ar = NewsService.IncrementArticleRating(rate, id);
            return Json(ar);
        }

        //[Authorize(Roles = "admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}