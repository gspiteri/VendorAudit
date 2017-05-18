using System.Web.Http;
using Microsoft.Owin;
using Owin;
using VendorAuditTracker.Webapi;

[assembly: OwinStartup(typeof(Startup))]
namespace VendorAuditTracker.Webapi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            //configure web api related settings
            WebApiConfig.Register(config);

            //configure autofac
            app.UseAutofacMiddleware(AutofacConfig.Register(config));
            app.UseAutofacWebApi(config);

            //configure owin to run web api
            app.UseWebApi(config);

            // Configure swagger
            //SwaggerConfig.Register(config);
        }
    }
}