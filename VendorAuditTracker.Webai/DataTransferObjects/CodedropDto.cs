using System;

namespace VendorAuditTracker.Webapi.DataTransferObjects
{
    public class CodeDropDto
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public DateTime TargetedDate { get; set; }
        public DateTime ActualDate { get; set; }
    }
}