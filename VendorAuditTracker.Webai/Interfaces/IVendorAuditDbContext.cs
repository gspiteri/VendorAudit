using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using VendorAuditTracker.Webapi.Models;

namespace VendorAuditTracker.Webapi.Interfaces
{
    public interface IVendorAuditDbContext
    {
        DbSet<Vendor> Vendors { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<SoftwareRelease> SoftwareReleases { get; set; }
        Database Database { get; }
        DbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        DbEntityEntry Entry(object entity);
    }
}