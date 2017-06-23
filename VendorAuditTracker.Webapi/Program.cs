using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.Owin.Hosting;
using Topshelf;

namespace VendorAuditTracker.Webapi
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<NancyOwinSelfHost>(s =>
                {
                    s.ConstructUsing(name => new NancyOwinSelfHost());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription(ConfigurationManager.AppSettings["Description"]);
                x.SetDisplayName(ConfigurationManager.AppSettings["DisplayName"]);
                x.SetServiceName(ConfigurationManager.AppSettings["ServiceName"]);
            });
        }
    }

    public class NancyOwinSelfHost
    {
        private IDisposable _webApp;

        public void Start()
        {
            var uri =
               new Uri(ConfigurationManager.AppSettings["Hostname"]);

            var url = uri.AbsoluteUri;
            _webApp = WebApp.Start<Startup>(url);
        }

        public void Stop()
        {
            if (_webApp != null)
                _webApp.Dispose();

            Console.WriteLine("Stopped. Good bye!");
        }
    }
}