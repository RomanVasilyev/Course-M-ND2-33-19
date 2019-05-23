using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using AutoMapper;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace ITNewsWeb.App_Start
{
    public class AutomapConfig
    {
        //Initialize
        public static void ConfigureContainer(Assembly webAssembly, IEnumerable<Assembly> relatedAssemblies)
        {
            //var builder = new ContainerBuilder();

            //// register all of autofac modules
            //builder.RegisterAssemblyModules(relatedAssemblies.ToArray());

            //// register all controllers
            //builder.RegisterControllers(webAssembly);

            //// register all web api controllers
            //builder.RegisterApiControllers(webAssembly);

            //// register all filters
            //builder.RegisterFilterProvider();

            //// build up the container
            //var container = builder.Build();

            //// register all AutoMapper profiles
            //Mapper.AddProfile(new CategoryProfile());

            //// register it to ASP.NET MVC
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //// Create the dependency resolver.
            //var resolver = new AutofacWebApiDependencyResolver(container);

            //// Configure Web API with the dependency resolver.
            //GlobalConfiguration.Configuration.DependencyResolver = resolver;

            //// получаем экземпляр контейнера
            //var builder = new ContainerBuilder();

            //// регистрируем контроллер в текущей сборке
            //builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //// регистрируем споставление типов
            //builder.AddDataDependencies();
            //builder.AddDomainDependencies();

            //// создаем новый контейнер с теми зависимостями, которые определены выше
            //var container = builder.Build();

            //// установка сопоставителя зависимостей
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //var csl = new AutofacServiceLocator(container);
            //ServiceLocator.SetLocatorProvider(() => csl);
        }
    }
}