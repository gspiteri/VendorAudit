
using VendorAuditTracker.Webapi.Services;

namespace VendorAuditTracker.Webapi.Controllers
{
    public class ReleaseController : BaseController
    {
        public readonly IReleaseService _ReleaseService;
        public ReleaseController(IReleaseService releaseService)
        {
            _ReleaseService = releaseService;
        }

    }
}
