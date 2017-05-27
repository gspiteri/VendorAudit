using System;
using Autofac;
using Autofac.Integration.WebApi;

namespace VendorAuditTracker.Webapi.Tests
{
    public class AutofacResolver
    {
        private readonly IContainer _container;

        private AutofacResolver()
        {
            _container = AutofacConfig.Configure();
        }

        /// <summary>
        ///     Please use inside a using block
        /// </summary>
        public static ILifetimeScope GetLifeTimeScope(Action<ContainerBuilder> _override = null)
        {
            var autofacConfig = new AutofacResolver();

            if (_override != null)
            {
                var containerUpdater = new ContainerBuilder();
                _override(containerUpdater);
                containerUpdater.Update(autofacConfig._container);
            }

            var resolver = new AutofacWebApiDependencyResolver(autofacConfig._container);
            return resolver.GetRootLifetimeScope();
        }
    }
}
