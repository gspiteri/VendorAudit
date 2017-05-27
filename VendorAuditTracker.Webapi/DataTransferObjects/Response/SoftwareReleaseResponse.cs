
using System.Collections.Generic;

namespace VendorAuditTracker.Webapi.DataTransferObjects.Response
{
    public class SoftwareReleaseResponse : BaseResponse
    {
        public SoftwareReleaseResponse()
        {
            SoftwareReleases = new List<SoftwareReleaseDto>();
        }

        public List<SoftwareReleaseDto> SoftwareReleases { get; set; }
    }

}