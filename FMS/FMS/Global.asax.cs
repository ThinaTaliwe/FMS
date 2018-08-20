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

namespace FMS{
    public class Global : HttpApplication {
        DriverREST rest = new DriverREST(1998);

        void Application_Start(object sender, EventArgs e)  {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Thread server = new Thread(rest.start);
            server.Start();
            //Todo timer to restart server

            SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
        }

        void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("delivery", "delivery/{id}", "~/EditDelivery.aspx");
        }
            
        // Handle Http errors.
        void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            if (exc is HttpUnhandledException)
            {
                // Pass the error on to the error page.
                Server.Transfer("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax", true);
            }
        }
    }

        
 }
 