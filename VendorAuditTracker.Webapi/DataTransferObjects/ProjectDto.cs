using System.Collections.Generic;

namespace VendorAuditTracker.Webapi.DataTransferObjects
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string InternalSystemId { get; set; }
        public string ExternalSystemId { get; set; }
        public virtual List<CodeDropDto> CodeDrops { get; set; }
        public virtual SoftwareReleaseDto SoftwareRelease { get; set; }
        public virtual List<VendorDto> Vendors { get; set; }
    }
}