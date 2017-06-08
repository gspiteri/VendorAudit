using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VendorAuditTracker.Webapi.Services;

namespace VendorAuditTracker.Webapi.Controllers
{
    public class VendorController : BaseController
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [Route("api/Vendor")]
        [HttpGet]
        public virtual async Task<IHttpActionResult> Get()

        {
            try
            {
                dynamic response = await _vendorService.GetAll();
                var status = response.IsSuccessful && response.Errors.Count == 0
                    ? HttpStatusCode.OK
                    : HttpStatusCode.BadRequest;
                return ResponseMessage(Request.CreateResponse(status, response as object));
            }
            catch (Exception ex)
            {
                var exceptionResponse = new { Errors = new List<string> { ex.ToString() }, IsSuccessful = false };
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, exceptionResponse));
            }
        }
    }
}