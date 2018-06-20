using System;
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
        private static Dictionary<string, DriverHandle> handles = null;
        private StreamWriter need = null;

        public DriverREST(int port)
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            handles = new Dictionary<string, DriverHandle>();
        }

        /*
         * start function will start the handles in new threads
         * **/
        public void start()
        {
            server.Start();
            while (true)
            {
                DriverHandle handle = new DriverHandle(server.AcceptTcpClient());
                lock (handle)
                {
                    if (!handles.ContainsKey(handle.getAddress()))
                    {
                        handles.Add(handle.getAddress(), handle);
                        Thread t = new Thread(handle.handle);
                        t.Start();
                        System.Diagnostics.Debug.WriteLine(handles[handle.getAddress()]);
                    }
                }
            }
        }

        public StreamWriter getWriter(string address)
        {
            StreamWriter x;
            lock (handles)
            {
                x = handles[address].GetWriter();
            }
            return x;
        }
    }
}