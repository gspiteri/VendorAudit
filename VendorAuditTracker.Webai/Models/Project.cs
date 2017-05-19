using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendorAuditTracker.Webapi.Models
{
    public class Project
    {
        public Project()
        {
            CodeDrops = new List<CodeDrop>();
            Vendors = new List<Vendor>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string InternalSystemId { get; set; }
        public string ExternalSystemId { get; set; }
        public virtual List<CodeDrop> CodeDrops { get; set; }
        public virtual SoftwareRelease SoftwareRelease { get; set; }
        public virtual List<Vendor> Vendors { get; set; }
    }
}