using System.Web.Http;
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

        /// <summary>
        /// Gives the ability to set the date format for all returned data
        /// </summary>
        /// <param name="config"></param>
        public static void SetDefaultDateFormat(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" });
        }

        /// <summary>
        /// Ability to reset the the json formater
        /// </summary>
        /// <param name="config"></param>
        public static void ResetDefaultDateFormat(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Clear();
        }
    }
}
