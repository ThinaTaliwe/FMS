using FMS.App_Code;
using System;
using System.IO;
using System.Net.Sockets;


namespace FMS
{
    internal class DriverHandle
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
        private static string ERROR_CODE = "400 ERR ";
        private static string OK_CODE = "200 OK ";

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
            string id = read();
            var query = "SELECT PASSWORD FROM USERS WHERE ID LIKE '" + id + "';";
            var dr = Util.query(query);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    send(OK_CODE);
                    string password = read();
                    verfied = password == dr.GetString(0);
                    if (verfied)
                    {
                        send(OK_CODE);
                        query = "UPDATE DRIVERS SET ADDRESS = '" + conn.Client.RemoteEndPoint + "' WHERE ID LIKE '" + id + "';";
                        Util.query(query);
                    }
                    else
                        send(ERROR_CODE);
                }
            } else
            {
                System.Diagnostics.Debug.WriteLine("oops id = " + id);
            }
            System.Diagnostics.Debug.WriteLine("Connection verified: " + verfied);
            if (verfied)
            {
                while (true)
                {
                    string text = read();
                    if(text != null) 
                        send(text.ToUpper());
                }
            }
            else
            {
                conn.Close();
                return;
            }
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
            System.Diagnostics.Debug.WriteLine("Sending: " + text);
            outStream.WriteLine(text);
            outStream.Flush();
        }

        private string read()
        {
            try
            {
                string text = inStream.ReadLine();
                System.Diagnostics.Debug.WriteLine("Read: " + text);
                return text;
            } catch (IOException ex) {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return null;
        }
    }
}