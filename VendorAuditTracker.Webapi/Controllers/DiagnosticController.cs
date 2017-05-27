using System;
using System.Diagnostics;
using System.Reflection;
using System.Web.Http;

namespace VendorAuditTracker.Webapi.Controllers
{
    [RoutePrefix("api/diagnostics")]
    public class DiagnosticController : ApiController
    {
        [Route("isalive")]
        [HttpGet]
        public bool IsAlive()
        {
            return true;
        }

        [Route("version")]
        [HttpGet]
        public IHttpActionResult GetVersionNumber()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            var appInfo = new { version = fvi.FileVersion, servername = Environment.MachineName };

            return Ok(appInfo);
        }
    }
}