using Microsoft.Extensions.DependencyInjection;
using Reveal.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebMvcReveal
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

            // Register your services here
            IServiceCollection services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            // Set the resolver to use the service provider
            DependencyResolver.SetResolver(new MyDependencyResolver(serviceProvider));

            // Configure MVC and other components here
            //won't work
            
            //services.AddReveal();

        }
    }

    public class MyDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public MyDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            return _serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _serviceProvider.GetServices(serviceType);
        }
    }

}
