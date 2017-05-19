using System;
using VendorAuditTracker.Webapi.Interfaces;
using VendorAuditTracker.Webapi.Models;

namespace VendorAuditTracker.Webapi.Services
{
    public abstract class ServiceBase : IDisposable
    {
        private readonly IDbContextFactory _factory;

        public IVendorAuditDbContext DbContext { get { return _factory.DbContext; } }

        public ServiceBase(IDbContextFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(@"dbContextFactory");

            _factory = factory;
        }

        public void Dispose()
        {
            _factory.Dispose();
        }
    }
}