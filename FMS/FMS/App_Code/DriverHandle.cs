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
                                query = "UPDATE DRIVERS SET ADDRESS = '" + conn.Client.RemoteEndPoint + "' WHERE ID LIKE '" + parts[0] + "';";
                                Util.query(query);
                                driver = parts[0];
                                time = DateTime.Now;
                                checkAssignment();
                            }
                            else
                                send(ERROR_CODE);
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("oops id = " + text);
                        send(ERROR_CODE);
                    }
                }
                System.Diagnostics.Debug.WriteLine("Connection verified: " + verfied);
                while (verfied)
                {
                    if(DateTime.Now.Subtract(time).Minutes > 5) {
                        checkAssignment();
                        time = DateTime.Now;
                    }
                    if(conn.Available > 0) {
                        string foo = read();
                        if (foo == "kill")
                        {
                            send("BYE");
                            verfied = false;
                        }else
                        {
                            send(foo.ToUpper());
                        }
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
                    send(delivery.ToString());
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
                outStream.WriteLine(text + "\n");
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