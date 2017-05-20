
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
    public class ReleaseController : BaseController
    {
        private readonly IReleaseService _releaseService;
        public ReleaseController(IReleaseService releaseService)
        {
            _releaseService = releaseService;
        }

        [Route("api/Release")]
        [HttpGet]
        [ResponseType(typeof(SoftwareReleaseResponse))]
        public virtual async Task<IHttpActionResult> Get()
        {
            HttpStatusCode status = HttpStatusCode.Accepted;
            SoftwareReleaseResponse response = new SoftwareReleaseResponse();
            try
            {
                //Call service
                status = response.IsSuccessful && response.Errors.Count == 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, response));
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.ToString());
                response.IsSuccessful = false;
            }
            return new NegotiatedContentResult<SoftwareReleaseResponse>(status, response, this);
        }

        [Route("api/Release/{Version}")]
        [HttpGet]
        [ResponseType(typeof(SoftwareReleaseResponse))]
        public virtual async Task<IHttpActionResult> GetSoftwareReleaseByVersion(long version)
        {
            HttpStatusCode status = HttpStatusCode.Accepted;
            SoftwareReleaseResponse response = new SoftwareReleaseResponse();
            try
            {
                //Call service
                status = response.IsSuccessful && response.Errors.Count == 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, response));
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.ToString());
                response.IsSuccessful = false;
            }
            return new NegotiatedContentResult<SoftwareReleaseResponse>(status, response, this);
        }
    }
}
