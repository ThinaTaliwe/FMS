using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.IO;
using System.Net.Sockets;

namespace FMS.App_Code
{
    public class DriverHandle
    {
        /*
         * DriverHandle class handles driver request using RESTful services
         * 
         * **/
        private TcpClient conn = null;
        private Stream stream = null;
        private StreamWriter outStream = null;
        private StreamReader inStream = null;
        private bool verfied = false;
        private static string ERROR_CODE = "400 ERR";
        private static string OK_CODE = "200 OK";

        public DriverHandle(TcpClient conn)
        {
            this.conn = conn;
            stream = conn.GetStream();
            outStream = new StreamWriter(stream);
            inStream = new StreamReader(outStream.BaseStream);
            System.Diagnostics.Debug.WriteLine("Connection made");
        }

        public void handle()
        {
            send("HELLO");
            string text = read();
            string[] parts = text.Split(' ');
            var query = "SELECT PASSWORD FROM USERS WHERE ID LIKE '" + parts[0] + "';";
            var dr = Util.query(query);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string password = parts[1];
                    verfied = password == dr.GetString(0);
                    if (verfied)
                    {
                        send(OK_CODE);
                        query = "UPDATE DRIVERS SET ADDRESS = '" + conn.Client.RemoteEndPoint + "' WHERE ID LIKE '" + parts[0] + "';";
                        Util.query(query);
                    }
                    else
                        send(ERROR_CODE);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("oops id = " + text);
            }
            System.Diagnostics.Debug.WriteLine("Connection verified: " + verfied);
            while (verfied)
            {
                string foo = read();
                if (foo != null && foo != "kill")
                    send(foo.ToUpper());
                else
                {
                    send("BYE");
                    verfied = false;
                }
            }
            conn.Close();
        }

        private void GET(string request)
        {
            string[] parts = request.Split(' ');
            switch (parts[0])
            {
                case "":
                    break;
                default:
                    break;
            }
        }

        private void POST(string request)
        {
            string[] parts = request.Split(' ');
            switch (parts[0])
            {
                case "":
                    break;
                default:
                    break;
            }
        }

        private void PUT(string request)
        {
            string[] parts = request.Split(' ');
            switch (parts[0])
            {
                case "":
                    break;
                default:
                    break;
            }
        }

        public String getAddress() { return conn.Client.RemoteEndPoint.ToString(); }

        public StreamWriter GetWriter() { return outStream; }

        public bool isVerified() { return verfied; }

        private void send(string text)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Sending: " + text);
                outStream.WriteLine(text);
                outStream.Flush();
            }
            catch (ObjectDisposedException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            catch (IOException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }

        private string read()
        {
            try
            {
                string text = inStream.ReadLine();
                System.Diagnostics.Debug.WriteLine("Read: " + text);
                return text;
            }
            catch (ObjectDisposedException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            catch (IOException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return null;
        }
    }
}