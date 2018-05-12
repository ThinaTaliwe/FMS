using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace FMS
{
    public class Global : HttpApplication
    {
        DriverREST rest = null;
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            if(rest == null)
            {
                DriverREST rest = new DriverREST(1998);
                Thread t = new Thread(rest.start);
                t.Start();
            }
        }
    }
}