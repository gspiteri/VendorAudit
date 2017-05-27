using System;
using System.Collections.Generic;

namespace VendorAuditTracker.Webapi.DataTransferObjects
{
    public class SoftwareReleaseDto
    {
        public int Id { get; set; }
        public long Version { get; set; }
        public string ReleaseName { get; set; }
        public List<ProjectDto> Projects { get; set; }
        public DateTime TargetedDate { get; set; }
        public DateTime ActualDate { get; set; }
    }
}