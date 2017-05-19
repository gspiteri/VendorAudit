
using System.Collections.Generic;
using VendorAuditTracker.Webapi.Models;

namespace VendorAuditTracker.Webapi.DataTransferObjects.Response
{
    public class VendorResponse : BaseResponse
    {
        public List<VendorDto> Vendors { get; set; }
    }
}