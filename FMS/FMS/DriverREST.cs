using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FMS
{
    internal class DriverREST
    {
        /*
         * Driver REST class which will provide a RESTful server to the driver class (android application)
         */
        private TcpListener server = null;

        public DriverREST(int port)
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
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
                new Thread(new ThreadStart(handle.handle));
            }
        }
    }
}