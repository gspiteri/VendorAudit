using VendorAuditTracker.Webapi.Interfaces;

namespace VendorAuditTracker.Webapi.Models
{
    public class DbContextFactory : IDbContextFactory
    {
        private IVendorAuditDbContext vendorAuditDbContext;

        public IVendorAuditDbContext DbContext
        {
            get
            {
                if (vendorAuditDbContext == null)
                {
                    vendorAuditDbContext = new VendorAuditDbContext();
                }

                return vendorAuditDbContext;
            }
        }

        public void Dispose()
        {
            if (vendorAuditDbContext != null)
            {
                vendorAuditDbContext.Dispose();
            }
        }
    }
}