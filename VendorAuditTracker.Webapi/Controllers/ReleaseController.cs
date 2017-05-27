
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VendorAuditTracker.Webapi.Services;

namespace VendorAuditTracker.Webapi.Controllers
{
    public class ReleaseController : BaseController
    {
        private readonly IReleaseService _releaseService;
        public ReleaseController(IReleaseService releaseService)
        {
            _releaseService = releaseService;
        }

        [Route("api/Release")]
        [HttpGet]
        public virtual async Task<IHttpActionResult> Get()
        {
            try
            {
                var response = new { IsSuccessful = true, Errors = new List<string>() };
                var status = response.IsSuccessful && response.Errors.Count == 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
                return ResponseMessage(Request.CreateResponse(status, (object)response));
            }
            catch (Exception ex)
            {
                var exceptionResponse = new { IsSuccessful = false, Errors = new List<string> { ex.ToString() } };
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, exceptionResponse));
            }
        }

        [Route("api/Release/{Version}")]
        [HttpGet]
        public virtual async Task<IHttpActionResult> GetSoftwareReleaseByVersion(long version)
        {
            try
            {
                //Retrieve from service will match Controller ie: ReleaseService
                var response = new { IsSuccessful = true, Errors = new List<string>() };
                var status = response.IsSuccessful && response.Errors.Count == 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
                return ResponseMessage(Request.CreateResponse(status, (object)response));
            }
            catch (Exception ex)
            {
                var exceptionResponse = new { IsSuccessful = false, Errors = new List<string> { ex.ToString() } };
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, exceptionResponse));
            }
        }
    }
}
