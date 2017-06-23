using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Diagnostics;
using Nancy.Responses;
using Nancy.Serialization.JsonNet;
using Owin;
using VendorAuditTracker.Webapi;
using VendorAuditTracker.Webapi.Interfaces;
using VendorAuditTracker.Webapi.Models;
using VendorAuditTracker.Webapi.Services;

[assembly: OwinStartup(typeof(Startup))]
namespace VendorAuditTracker.Webapi
{
    public class Startup : AutofacNancyBootstrapper
    {
        protected async override void ApplicationStartup(ILifetimeScope container,
           IPipelines pipelines)
        {
            //Add correlationId for audit history for all incoming requests
            pipelines.BeforeRequest += (ctx) =>
            {
                Guid correlationId;
                if (!Guid.TryParse(ctx.Request.Headers["CorrelationId"].FirstOrDefault(), out correlationId))
                    correlationId = Guid.NewGuid();

                //Need to set global object 

                return null;
            };
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            pipelines.OnError.AddItemToEndOfPipeline((ctx, ex) =>
            {
                LogException(ex);
                return BuildErrorResponse(ex);
            });

            base.RequestStartup(container, pipelines, context);
        }

        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get { return new DiagnosticsConfiguration { Password = @"A2\6mVtH/XRT\p,B" }; }
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);

            existingContainer.Update(builder =>
            {
                builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

                builder.RegisterType<VendorAuditDbContext>().As<IVendorAuditDbContext>().InstancePerLifetimeScope();

                //Register services
                builder.RegisterType<VendorService>().As<IVendorService>().InstancePerLifetimeScope();
                builder.RegisterType<ReleaseService>().As<IReleaseService>().InstancePerLifetimeScope();

                builder.RegisterType<DbContextFactory>().As<IDbContextFactory>().InstancePerLifetimeScope();
            });
        }


        #region Helper Methods
        private static void LogException(Exception ex)
        {
            var logService = new LogService { Name = ex.Source };
            logService.Error.Add(string.Format("{0}-{1}", ex.Message, ex));
        }
        private static JsonResponse BuildErrorResponse(Exception ex)
        {
            var errorList = new List<string>();

            var list = ex.Data["errors"] as List<string>;
            if (list != null)
                errorList = list;
            else
                errorList.Add("Unexpected Error");

            var ErrorResponse = new
            {
                Errors = errorList
            };

            return new JsonResponse(ErrorResponse, new JsonNetSerializer())
            {
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
        #endregion
    }
}