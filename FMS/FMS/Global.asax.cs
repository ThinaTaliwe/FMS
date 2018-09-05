using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using FMS.App_Code;
using System.Timers;

namespace FMS{
    public class Global : HttpApplication {
        private DriverREST rest;
        private System.Timers.Timer timer = new System.Timers.Timer();

        void Application_Start(object sender, EventArgs e)  {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            startServer(1998);
            timer.Elapsed += new ElapsedEventHandler(serverCheck);
            timer.Interval = 300000;
            timer.Enabled = true;

        }

        private void serverCheck(object source, ElapsedEventArgs a) {
            Util.print("Checking if server is online");
            if (!rest.isRunning())
            {
                Util.print("Not running");
                startServer(1998);
            }
            else
                Util.print("server running");
        }

        private void startServer(int port) {
            Util.print("Starting server");
            if (rest != null)
            {
                rest = null;
                Thread.Sleep(3000);
            }
            rest = new DriverREST(1998);
            Thread server = new Thread(rest.start);
            server.Start();
        }

        void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("delivery", "delivery/{id}", "~/EditDelivery.aspx");
            routes.MapPageRoute("driver", "driver/{id}", "~/drivers.aspx");
            routes.MapPageRoute("client", "client/{id}", "~/clients.aspx");
            routes.MapPageRoute("truck", "truck/{id}", "~/trucks.aspx");
            routes.MapPageRoute("report", "report/{id}", "~/deliveryreport.aspx");
        }
    }

        
 }
 