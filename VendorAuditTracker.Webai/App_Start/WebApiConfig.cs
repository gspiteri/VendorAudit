using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Newtonsoft.Json.Converters;

namespace VendorAuditTracker.Webapi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            SetDefaultDateFormat(config);
        }


        public static void SetDefaultDateFormat(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" });
        }

        public static void ResetDefaultDateFormat(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Clear();
        }
    }
}
