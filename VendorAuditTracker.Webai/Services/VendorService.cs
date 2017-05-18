﻿
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using VendorAuditTracker.Webapi.DataTransferObjects.Response;
using VendorAuditTracker.Webapi.Interfaces;
using VendorAuditTracker.Webapi.Models;

namespace VendorAuditTracker.Webapi.Services
{
    public class VendorService : IVendorService
    {
        private readonly IVendorAuditDbContext _auditDbContext;
        public VendorService(IVendorAuditDbContext dbContext)
        {
            _auditDbContext = dbContext;
        }

        public async Task<int> Add(Vendor vendorToSave)
        {
            if (!VendorExists(vendorToSave).Result)
                return 0;

            _auditDbContext.Vendors.Add(vendorToSave);
            return await _auditDbContext.SaveChangesAsync();
        }

        private async Task<bool> VendorExists(Vendor vendorToSave)
        {
            return await FindAsync(vendorToSave) != null;
        }

        public async Task<Vendor> FindAsync(Vendor vendorToSave)
        {
            return await _auditDbContext.Vendors.FindAsync(vendorToSave);
        }

        public async Task<int> Update(Vendor vendorToSave)
        {
            var vendor = FindAsync(vendorToSave).Result;
            if (vendor == null)
                return 0;

            _auditDbContext.Entry(vendor).State = EntityState.Modified;
            return await _auditDbContext.SaveChangesAsync();
        }

        public async Task<VendorResponse> GetAll()
        {
            var response = new VendorResponse();
            try
            {
                var vendors = await _auditDbContext.Vendors.ToListAsync();
                response.IsSuccessful = true;
                response.Vendors = vendors;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.ToString());
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}