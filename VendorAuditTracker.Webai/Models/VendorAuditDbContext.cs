using System.Data.Entity;
using VendorAuditTracker.Webapi.Interfaces;

namespace VendorAuditTracker.Webapi.Models
{


    public class VendorAuditDbContext : DbContext, IVendorAuditDbContext
    {
        // Your context has been configured to use a 'VendorAuditDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'VendorAuditTracker.Webapi.Models.VendorAuditDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'VendorAuditDbContext' 
        // connection string in the application configuration file.
        public VendorAuditDbContext()
            : base("name=VendorAuditDbContext")
        {
        }

        public virtual DbSet<Vendor> Vendors { get; set; }


        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<SoftwareRelease> SoftwareReleases { get; set; }
    }
}