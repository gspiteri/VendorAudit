
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using VendorAuditTracker.Webapi.DataTransferObjects.Response;
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
        [ResponseType(typeof(VendorResponse))]
        public virtual async Task<IHttpActionResult> Get()
        {
            HttpStatusCode status = HttpStatusCode.Accepted;
            VendorResponse response = new VendorResponse();
            try
            {
                response = await _vendorService.GetAll();
                status = response.IsSuccessful && response.Errors.Count == 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, response));
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.ToString());
                response.IsSuccessful = false;
            }
            return new NegotiatedContentResult<VendorResponse>(status, response, this);
        }
    }
}
