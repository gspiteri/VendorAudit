
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendorAuditTracker.Webapi.Models
{
    public class Vendor
    {
        public Vendor()
        {
            Projects = new List<Project>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PrimaryContact { get; set; }
        public string SecondaryContact { get; set; }
        public virtual List<Project> Projects { get; set; }
    }
}