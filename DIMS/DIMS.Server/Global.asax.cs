using HIMS.BL.Infrastructure;
using HIMS.Server.App_Start;
using HIMS.Server.utils;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HIMS.Server
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutomapperConfig.Initialize();

            // Remove data annotations validation provider 
            ModelValidatorProviders.Providers.Remove(
                ModelValidatorProviders.Providers.OfType<DataAnnotationsModelValidatorProvider>().First());

            DependencyInjection();
        }

        private void DependencyInjection()
        {
            // dependency injection
            NinjectModule dependencesModule = new DependencesModule();

            NinjectModule serviceModule = new ServicesModule("HIMSDbContext", "HimsIdentityConnection");

            var kernel = new StandardKernel(dependencesModule, serviceModule);

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            var userObject = new UserObject
            {
                IsAdmin = User.IsInRole("admin")
            };

            HttpContext.Current.Session.Add("__userObject", userObject);
        }
    }
}
