using HIMS.BL.Infrastructure;
using HIMS.Server.App_Start;
using HIMS.Server.utils;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using Ninject.Web.WebApi.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
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

            GlobalConfiguration.Configure(WebApiConfig.Register);

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

            NinjectModule serviceModule = new ServicesModule("DIMSDBConnection", "HimsIdentityConnection");

            var kernel = new StandardKernel(dependencesModule, serviceModule);

            kernel.Bind<DefaultModelValidatorPdinroviders>().ToConstant(new DefaultModelValidatorProviders(GlobalConfiguration.Configuration.Services.GetModelValidatorProviders()));

            var ninjectResolver = new utils.NinjectDependencyResolver(kernel);

            DependencyResolver.SetResolver(ninjectResolver);

            GlobalConfiguration.Configuration.DependencyResolver = new utils.NinjectDependencyResolver(kernel);

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
