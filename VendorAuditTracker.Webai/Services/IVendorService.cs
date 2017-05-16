using System.Threading.Tasks;
using VendorAuditTracker.Webapi.Models;

namespace VendorAuditTracker.Webapi.Services
{
    public interface IVendorService
    {
        Task<int> Add(Vendor vendorToSave);
        Task<Vendor> FindAsync(Vendor vendorToSave);
        Task<int> Update(Vendor vendorToSave);
    }
}