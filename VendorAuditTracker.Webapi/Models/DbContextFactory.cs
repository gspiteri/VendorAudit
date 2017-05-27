using VendorAuditTracker.Webapi.Interfaces;

namespace VendorAuditTracker.Webapi.Models
{
    public class DbContextFactory : IDbContextFactory
    {
        private IVendorAuditDbContext _vendorAuditDbContext;

        public IVendorAuditDbContext DbContext
        {
            get { return _vendorAuditDbContext ?? (_vendorAuditDbContext = new VendorAuditDbContext()); }
        }

        public void Dispose()
        {
            if (_vendorAuditDbContext != null)
                _vendorAuditDbContext.Dispose();
        }
    }
}