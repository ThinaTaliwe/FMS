﻿using System;
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
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Thread server = new Thread(rest.start);
            server.Start();
            //Thread assignments = new Thread(assignmentCheck);
            //assignments.Start();
        } 

        public DriverHandle getHandle(string address) { return rest.getHandle(address); }
    }

        
 }
 