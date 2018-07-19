using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        private string driver;
        private DateTime time;
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
            try {
                send("HELLO");
                string text = read();
                if (text != null)
                {
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
                                driver = parts[0];
                                checkAssignment();
                                time = DateTime.Now;
                            }
                            else
                                send(ERROR_CODE);
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("invalid id = " + text);
                        send(ERROR_CODE);
                    }
                }
                System.Diagnostics.Debug.WriteLine("Driver Verified: " + verfied);
                while (verfied)
                {
                    try {
                        if (DateTime.Now.Subtract(time).Minutes > 30)
                        {
                            checkAssignment();
                            time = DateTime.Now;
                        }
                        if (conn.Available > 0)
                        {
                            string response = read();
                            if (response == "kill")
                            {
                                send("BYE"); 
                                verfied = false;
                            }
                            else
                            {
                                string[] parts = response.Split(' ');
                                var query = "";
                                switch (parts[0]) {
                                    case "accept":
                                        //accept [delivery_id]
                                         query = "update delivery set accepted = '1' where id like '" + parts[1] + "'";
                                        Util.query(query);
                                        send(OK_CODE);
                                        break;
                                    case "location":
                                        //location [delivery_id] [longitude:latitude]
                                        query = "insert into locations values('" + parts[1] + "', '" + driver + "', '" + parts[2] + "', '" + DateTime.Now + "')";
                                        Util.query(query);
                                        send(OK_CODE);
                                        break;
                                    case "message":
                                        //message [message_code]
                                        query = "update drivers set message = '" + parts[1] + "' where id like '" + driver + "'";
                                        Util.query(query);
                                        send("message " + OK_CODE);
                                        break;
                                    case "current":
                                        query = "select id from delivery where driver like '" + driver + "' and accepted like '1' and completed like '0'";
                                        var current = Util.query(query);
                                        if (current.HasRows)
                                        {
                                            while (current.Read())
                                            {
                                                Delivery deliv = Delivery.getInstance(current.GetInt32(0));
                                                send(deliv.ToString());
                                            }
                                        }
                                        else
                                            send(ERROR_CODE);
                                        break;
                                    case "distance":
                                        //distance [from] [to]
                                        try {
                                            var from = Util.getCoords(parts[1]);
                                            var to = Util.getCoords(parts[2]);
                                            var distance = Util.distance(from, to);
                                            send(distance.ToString());
                                        } catch(Exception ex) {
                                            System.Diagnostics.Debug.WriteLine(ex);
                                            send(ERROR_CODE);
                                        }
                                        break;
                                    case "start":
                                        //start [delivery_id]
                                        query = "update delivery set started = '1' where id like " + parts[1];
                                        Util.query(query);
                                        send(OK_CODE);
                                        break;
                                    case "complete":
                                        //complete [delivery_id]
                                        query = "update delivery set completed = '1' where id like " + parts[1];
                                        Util.query(query);
                                        send(OK_CODE);
                                        break;
                                    default:        
                                        send(ERROR_CODE);
                                        break;
                                }
                            }
                        }
                    } catch (Exception ex) {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                }
                conn.Close();
            } catch(NullReferenceException ex) {
                System.Diagnostics.Debug.WriteLine(ex);
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);

            } 
        }

        private void checkAssignment() {
            string query = "select id from delivery where driver like '" + driver + "' and accepted like '0'";
            var assignment = Util.query(query);
            if (assignment.HasRows)
            {
                while (assignment.Read())
                {
                    Delivery delivery = Delivery.getInstance(assignment.GetInt32(0));
                    send("assignment " + delivery.ToString());
                }
            }
        }        

        public String getAddress() { return conn.Client.RemoteEndPoint.ToString(); }

        public bool isVerified() { return verfied; }

        public string getDriver() { return driver; }

        public void send(string text)
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