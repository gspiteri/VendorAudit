
using System.Collections.Generic;

namespace VendorAuditTracker.Webapi.DataTransferObjects.Response
{
    public class VendorResponse : BaseResponse
    {
        public List<VendorDto> Vendors { get; set; }
    }
}