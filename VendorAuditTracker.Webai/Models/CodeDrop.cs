using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendorAuditTracker.Webapi.Models
{
    public class CodeDrop
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string VendorName { get; set; }
        public DateTime TargetedDate { get; set; }
        public DateTime ActualDate { get; set; }
    }
}