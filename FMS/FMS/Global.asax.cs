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

namespace FMS
{
    public class Global : HttpApplication
    {
        DriverREST rest = new DriverREST(1998);
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Thread t = new Thread(rest.start);
            t.Start();
        } 

        public StreamWriter getServer(string address)
        {
            return rest.getWriter(address);
        }
    }

}