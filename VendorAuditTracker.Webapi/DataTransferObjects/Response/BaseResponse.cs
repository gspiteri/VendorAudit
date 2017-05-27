
using System.Collections.Generic;

namespace VendorAuditTracker.Webapi.DataTransferObjects.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; set; }
        public virtual bool IsSuccessful { get; set; }
    }
}