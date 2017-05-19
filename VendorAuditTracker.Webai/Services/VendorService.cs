
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using VendorAuditTracker.Webapi.DataTransferObjects;
using VendorAuditTracker.Webapi.DataTransferObjects.Response;
using VendorAuditTracker.Webapi.Models;

namespace VendorAuditTracker.Webapi.Services
{
    public class VendorService : ServiceBase, IVendorService
    {
        public VendorService(IDbContextFactory dbContext)
            : base(dbContext)
        {
        }

        public async Task<int> Add(Vendor vendorToSave)
        {
            if (!VendorExists(vendorToSave).Result)
                return 0;

            DbContext.Vendors.Add(vendorToSave);
            return await DbContext.SaveChangesAsync();
        }

        private async Task<bool> VendorExists(Vendor vendorToSave)
        {
            return await FindAsync(vendorToSave) != null;
        }

        public async Task<Vendor> FindAsync(Vendor vendorToSave)
        {
            return await DbContext.Vendors.FindAsync(vendorToSave);
        }

        public async Task<int> Update(Vendor vendorToSave)
        {
            var vendor = FindAsync(vendorToSave).Result;
            if (vendor == null)
                return 0;

            DbContext.Entry(vendor).State = EntityState.Modified;
            return await DbContext.SaveChangesAsync();
        }

        public async Task<VendorResponse> GetAll()
        {
            var response = new VendorResponse();
            var _vendors = new List<VendorDto>();

            var vendors = await DbContext.Vendors.ToListAsync();
            response.IsSuccessful = true;
            vendors.ForEach(i => _vendors.Add(Mapper.BusinessObjectToDto(i)));
            response.Vendors = _vendors;

            return response;
        }
    }
}