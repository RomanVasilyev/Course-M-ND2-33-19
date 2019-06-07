using System;
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

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new ItemDetailsDto();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(ItemDetailsDto viewModel)
        {
            try
            {
                viewModel.CreatedBy = User.Identity.Name;
                viewModel.CreatedDate = DateTime.Now;
                var id = viewModel.Id;
                var catid = viewModel.CategoryId;
                NewsService.Add(viewModel);
                //NewsService.Save(viewModel);
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