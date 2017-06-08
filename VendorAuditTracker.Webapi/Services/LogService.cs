using System.Collections.Generic;
using System.Net.Http;

namespace VendorAuditTracker.Webapi.Services
{
    public class LogService : HttpResponseMessage
    {
        public string Name { get; set; }
        public List<string> Error { get; set; }
    }
}