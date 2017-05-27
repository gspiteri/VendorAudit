
using VendorAuditTracker.Webapi.Models;

namespace VendorAuditTracker.Webapi.Services
{
    public class ReleaseService : ServiceBase, IReleaseService
    {
        public ReleaseService(IDbContextFactory dbContext)
            : base(dbContext)
        {
        }
    }
}