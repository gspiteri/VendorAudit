using System.Collections.Generic;

namespace VendorAuditTracker.Webapi.DataTransferObjects
{
    public class VendorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PrimaryContact { get; set; }
        public string SecondaryContact { get; set; }
        public virtual List<ProjectDto> Projects { get; set; }
    }
}