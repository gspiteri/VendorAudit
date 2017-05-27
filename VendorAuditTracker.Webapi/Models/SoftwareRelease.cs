using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendorAuditTracker.Webapi.Models
{
    public class SoftwareRelease
    {
        public SoftwareRelease()
        {
            Projects = new List<Project>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public long Version { get; set; }
        public string ReleaseName { get; set; }
        public List<Project> Projects { get; set; }
        public DateTime TargetedDate { get; set; }
        public DateTime ActualDate { get; set; }
    }
}