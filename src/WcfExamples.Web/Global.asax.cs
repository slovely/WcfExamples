using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StructureMap;
using WcfExamples.Contracts;
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

            SetupRoutes();

            SetupIoC();
        }

        private static void SetupRoutes()
        {
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
        }

        private static void SetupIoC()
        {
            ObjectFactory.Initialize(x =>
                {
                    x.Configure(
                        cfg => cfg.Scan(scan =>
                            {
                                scan.TheCallingAssembly();
                                scan.AssemblyContainingType<IRequestResponseService>();
                                scan.Convention<StructureMapWcfProxyConvention>();
                                scan.WithDefaultConventions();

                            }));

                    //Example of conditionally using a different implementation
                    //(obviously you wouldn't normally conditionally cache something... not a real-world example)
                    //But the condition could be anything - check current user, whatever...
                    x.For<IAopExampleService>().ConditionallyUse(o =>
                        {
                            o.If(c => HttpContext.Current.Request.Params["cached"] == "true").ThenIt.Is.Type<AopExampleService>().EnrichWith(svc => new CachedAopService(svc));
                            o.TheDefault.Is.Type<AopExampleService>();
                        });

                });

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(ObjectFactory.Container);

        }
    }
}
