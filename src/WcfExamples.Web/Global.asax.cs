using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using StructureMap;
using WcfExamples.Contracts;
using WcfExamples.Web;
using WcfExamples.Web.Code;
using WcfExamples.Web.DependencyResolution;

namespace WcfExamples.Web
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();

            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Web Forms default
            RouteTable.Routes.MapPageRoute(
              "WebFormDefault",
              "",
              "~/default.aspx");

            RouteTable.Routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );

            ObjectFactory.Configure(cfg => cfg.For<IExampleService>().Use(x => new WcfModule().CreateProxy<IExampleService>()));

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
        }
    }
}
