using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
    public class Driver : User
    {
        private string code;
        private DateTime firstIssue;
        private DateTime expiry;
        private int restriction;

        public Driver(string id) : base(id)
        {
            var query = "SELECT code, first_issue, expiry, restriction FROM DRIVERS WHERE ID LIKE '" + id + "'";
            var driver = Util.query(query);
            while (driver.Read())
            {
                code = Convert.ToString(driver.GetString(0));
                firstIssue = driver.GetDateTime(1);
                expiry = driver.GetDateTime(2);
                restriction = Convert.ToInt32(driver.GetInt32(3));
            }
        }

        public List<Delivery> deliveriesMade(DateTime from, DateTime to)
        {
            var query = "select id from delivery where started > '" + from + "' and started < '" + to + "' and driver like '" + id + "'";
            var reader = Util.query(query);
            if (reader.HasRows)
            {
                List<int> list = new List<int>();
                while (reader.Read())
                    list.Add(reader.GetInt32(0));
                List<Delivery> lstDelivs = new List<Delivery>();
                foreach (int id in list)
                    lstDelivs.Add(Delivery.getInstance(id));
                return lstDelivs;
            }
            return null;
        }

        public double KmsDriven(DateTime from, DateTime to) {
            var query = "select id from delivery where started > '" + from + "' and started < '" + to + "' and driver like '" + id + "'";
            var reader = Util.query(query);
            double km = 0.0;
            if(reader.HasRows) {
                List<int> lstDelivs = new List<int>();
                while (reader.Read())
                    lstDelivs.Add(reader.GetInt32(0));
                foreach(var id in lstDelivs) {
                    Delivery delivery = Delivery.getInstance(id);
                    var json = delivery.speedInfo();
                    if(json != null) {
                        km += Convert.ToDouble(json["distance"]);
                    }
                }
            }
            return km;
        }

        public static List<Driver> getDriverList()
        {
            List<Driver> lstDriver = new List<Driver>();
            List<string> lstIDS = new List<string>();
            var query = "select id from users where user_type like 'driver'";
            var reader = Util.query(query);
            if(reader.HasRows)
            {
                while(reader.Read())
                    lstIDS.Add(reader.GetString(0));
                foreach(var id in lstIDS)
                {
                    lstDriver.Add(new Driver(id));
                }
                return lstDriver;
            }
            return null;
        }

        public string lastLocation() {
            var query = "select location from location where driver like '" + id + "' order by time desc";
            var location = Util.query(query);
            location.Read();
            return location.GetString(0);
        }

        public string[] getMessage()
        {
            var query = "select message from drivers where id like '" + id + "'";
            var message = Util.query(query);
            message.Read();
            try
            {
                string text = message.GetString(0);
                var array = text.Split('#');
                return new string[] {name, array[0], array[1] };
            } catch(Exception ex)
            {
                Util.print(ex.ToString());
            }
            return null;
        }

        public void setCode(string value) { code = value; }
        public void setExpiry(DateTime value) { expiry = value; }
        public void setRestriction(int value) { restriction = value; }

        public string getCode() { return code; }
        public DateTime getFirstIssue() { return firstIssue; }
        public DateTime getExpiry() { return expiry; }
        public int getRetriction() { return restriction; }
    }
}