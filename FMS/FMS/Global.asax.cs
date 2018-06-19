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

namespace FMS{
    public class Global : HttpApplication {
        ConnectionManager connMan = new ConnectionManager();
        DriverREST rest = new DriverREST(1998);

        void Application_Start(object sender, EventArgs e)  {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Thread t = new Thread(rest.start);
            t.Start();
        } 

        private class ConnectionManager  {
            private List<String> orders;

            public ConnectionManager()  {
                orders = new List<string>();
                Thread t = new Thread(process);
                t.Start();
            }

            public void process()  {
                while(true) {
                    foreach(string order in orders) {
                        string[] parts = order.Split(' ');
                    }
                    Thread.Sleep(5000);
                }
            }

            public void addOrder(string order) {
                orders.Add(order);
            }
        }

        public void addOrder(string order)  {  connMan.addOrder(order);   }

        public StreamWriter getServer(string address)   {  return rest.getWriter(address);   }
    }

}