﻿using FMS.App_Code;
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

        public DriverHandle(TcpClient conn)
        {
            this.conn = conn;
            stream = conn.GetStream();
            outStream = new StreamWriter(stream);
            inStream = new StreamReader(outStream.BaseStream);
            System.Diagnostics.Debug.WriteLine("Connection made");
            string id = read();
            System.Diagnostics.Debug.WriteLine(id);
            var query = "SELECT * FROM DRIVERS WHERE ID LIKE '" + id + "';";
            var dr = Util.query(query);
            if(dr.HasRows)
            {
                query = "UPDATE DRIVERS SET ADDRESS = '" + conn.Client.RemoteEndPoint + "' WHERE ID LIKE '" + id + "';";
                Util.query(query);
            }
        }

        public void handle()
        {
            string text = read();
            string[] request = text.Split(' ');

            switch (request[0])
            {
                case "GET":

                    break;
                case "POST":

                    break;
                case "PUT":

                    break;
                default:
                    send("200 ERR Invalid_Request");
                    break;
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

        private void send(string text)
        {
            outStream.WriteLine(text);
            outStream.Flush();
        }

        private string read()
        {
            return inStream.ReadLine();
        }
    }
}