using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ITNewsWeb.App_Start;
using ITNewsWeb.Migrations;
using ITNewsWeb.Models;

namespace ITNewsWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Инициализация БД для создания ролей
            //Database.SetInitializer<ApplicationDbContext>(new Configuration());

            AreaRegistration.RegisterAllAreas();

            AutofacConfig.ConfigureContainer();
            AutomapConfig.Configure();

            //GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
