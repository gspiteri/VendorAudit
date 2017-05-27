using System;
using VendorAuditTracker.Webapi.Interfaces;

namespace VendorAuditTracker.Webapi.Models
{
    public interface IDbContextFactory : IDisposable
    {
        IVendorAuditDbContext DbContext { get; }
    }
}