
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
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

        public async Task<object> FindAsync(Vendor vendorToSave)
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

        public async Task<object> GetAll()
        {
            var vendorList = new List<object>();
            var vendors = await DbContext.Vendors.ToListAsync();
            vendors.ForEach(i => vendorList.Add(Mapper.BusinessObjectToDto(i)));
            return new { Vendors = vendorList, IsSuccessful = true, Errors = new List<string>() };
        }
    }
}