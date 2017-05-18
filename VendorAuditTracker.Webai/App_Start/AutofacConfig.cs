using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using VendorAuditTracker.Webapi.Interfaces;
using VendorAuditTracker.Webapi.Models;
using VendorAuditTracker.Webapi.Services;

namespace VendorAuditTracker.Webapi
{
    public class AutofacConfig
    {
        public static IContainer Register(HttpConfiguration config)
        {
            var container = Configure();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<VendorAuditDbContext>().As<IVendorAuditDbContext>().InstancePerLifetimeScope();

            //Register services
            builder.RegisterType<VendorService>().As<IVendorService>().InstancePerLifetimeScope();
            builder.RegisterType<ReleaseService>().As<IReleaseService>().InstancePerLifetimeScope();


            var container = builder.Build();
            return container;
        }
    }
}