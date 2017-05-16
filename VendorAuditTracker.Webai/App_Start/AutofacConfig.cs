using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using VendorAuditTracker.Webapi.Services;

namespace VendorAuditTracker.Webapi
{
    public class AutofacConfig
    {
        public static void RegisterComponents()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            builder.RegisterType<VendorService>().As<IVendorService>().InstancePerRequest();
            builder.RegisterType<ReleaseService>().As<IReleaseService>().InstancePerRequest();
        }
    }
}