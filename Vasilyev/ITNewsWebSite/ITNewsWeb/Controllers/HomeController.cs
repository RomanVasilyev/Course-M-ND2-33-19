using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Http.News.Domain.Contracts.Dtos;
using Http.News.Domain.Services;

namespace ITNewsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsService NewsService;

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

        [Authorize]
        [HttpPost]
        public ActionResult SetRating(int itemId, double ratingValue)
        {
            return Json(new { Result = ratingValue }); 
        }

        [Authorize]
        [HttpPost]
        public ActionResult Details(ItemDetailsDto viewModel, double score)
        {
            var id = viewModel.Id;
            var catid = viewModel.CategoryId;
            if (Request.Cookies["rating" + id] != null)
            {
                ViewBag.Message = "You have already rated this article";
                return View(NewsService.BuildItemDetailsViewModel(catid, id));
            }

            Response.Cookies["rating" + +id].Value = DateTime.Now.ToString();
            Response.Cookies["rating" + id].Expires = DateTime.Now.AddYears(1);
            viewModel = NewsService.IncrementArticleRating(score, id, catid);
            return View(NewsService.BuildItemDetailsViewModel(catid, id));
        }

        [Authorize(Roles = "admin, writer")]
        public ActionResult Create()
        {
            var viewModel = new ItemDetailsDto();
            PrepareView(viewModel);
            return View(viewModel);
        }

        private void PrepareView(ItemDetailsDto viewModel)
        {
            viewModel.CatList = new List<SelectListItem>();
            var cat = NewsService.GetCategoryForMenu();
            for (int i = 0; i < cat.Count; i++)
            {
                viewModel.CatList.Add(new SelectListItem { Value = ( i + 1 ).ToString(), Text = cat.ElementAt(i).Name });
            }
        }

        [Authorize(Roles = "admin, writer")]
        [HttpPost]
        public ActionResult Create(ItemDetailsDto viewModel)
        {
            try
            {
                viewModel.CreatedBy = User.Identity.Name;
                viewModel.CreatedDate = DateTime.Now;
                var catid = viewModel.CategoryId;
                string filename = Path.GetFileNameWithoutExtension(viewModel.ImageFile.FileName);
                string extention = Path.GetExtension(viewModel.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extention;
                viewModel.SmallImageUrl = viewModel.BigImageUrl = viewModel.MediumImageUrl = "/Upload/img/" + filename;
                NewsService.Add(viewModel);
                filename = Path.Combine(Server.MapPath("~/Upload/img/"), filename);
                viewModel.ImageFile.SaveAs(filename);
                //NewsService.Save(viewModel);
                var id = NewsService.GetItemsByCategoryId(catid).OrderBy(x => x.ItemId).Last().ItemId;
                return RedirectToAction("Details", "Home", new {categoryId = catid, itemId = id});
            }
            catch (Exception e)
            {
                return View();
            }
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