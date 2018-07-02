﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FMS.App_Code
{
    public class DriverREST
    {
        /*
         * Driver REST class which will provide a RESTful server to the driver class (android application)
         */
        private TcpListener server = null;
        private static List<DriverHandle> handles = null;
        private StreamWriter need = null;

        public DriverREST(int port)
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            handles = new List<DriverHandle>();
        }

        /*
         * start function will start the handles in new threads
         * **/
        public void start()
        {
            try {
                server.Start();
                lock(server) {
                    while (true)
                    {
                        try
                        {
                            DriverHandle handle = new DriverHandle(server.AcceptTcpClient());
                            lock (handle)
                            {
                                if (!handles.Contains(handle))
                                {
                                    handles.Add(handle);
                                    Thread t = new Thread(handle.handle);
                                    t.Start();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex);
                        }
                    }
                }
            } catch (SocketException ex) {
                System.Diagnostics.Debug.WriteLine(ex);
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}