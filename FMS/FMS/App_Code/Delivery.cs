using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace FMS.App_Code
{
    public class Delivery
    {
        private int id { get; set; }
        private string orderNum { get; set; }
        private string truck { get; set; }
        private string driver { get; set; }
        private int client { get; set; }
        private string from { get; set; }
        private string to { get; set; }
        private string material { get; set; }
        private int load { get; set; }
        private DateTime departDay { get; set; }
        private DateTime arrivalDay { get; set; }
        private string authority { get; set; }
        private DateTime accepted { get; set; }
        private DateTime started { get; set; }
        private DateTime completed { get; set; }
        private DateTime confirmed { get; set; }
        private int distance { get; set; }

        public string getFromCoords() {
            return getCoords(from);
        }

        public string getToCoords()
        {
            return getCoords(to);
        }

        public JObject jsonDelivery()
        {
            JObject deliv = new JObject();
            deliv["id"] = id;
            deliv["orderNum"] = orderNum;
            deliv["truck"] = truck;
            deliv["client"] = new Client(client).getCompany();
            deliv["fromCoords"] = getCoords(from);
            deliv["fromAddress"] = getAddress(from);
            deliv["toCoords"] = getCoords(to);
            deliv["toAddress"] = getAddress(to);
            deliv["material"] = material;
            deliv["load"] = load;
            deliv["departDay"] = departDay.ToString();
            return deliv;
        }

        override
        public string ToString()
        {
            return jsonDelivery().ToString();
        }

        public static string[] LastLocation(int id) {
            try {
                string query = "select location, time from locations where delivery like " + id + " order by time asc";
                var location = Util.query(query);
                if (location.HasRows) {
                    if (location.Read()) {
                        return new string[] { location.GetString(0), location.GetDateTime(1).TimeOfDay.ToString() };
                    }
                }
            } catch (Exception ex) {
                Util.print(ex.ToString());
            } return null;
        }

        public JObject speedInfo() {
            var query = "select location, time from locations where delivery like " + id;
            var points = Util.query(query);
            if(points.HasRows) {
                List<string> coords = new List<string>();
                List<DateTime> times = new List<DateTime>();
                while(points.Read()) {
                    coords.Add(points.GetString(0));
                    times.Add(points.GetDateTime(1));
                }
                return Util.averageSpeed(coords, times);
            }
            return null;
        }

        public int save() { return Delivery.save(this); }

        public static int save(Delivery deliv)
        {
            var query = "select * from delivery where id like " + deliv.id;
            var isValid = Util.query(query);
            if (!isValid.HasRows)
            {
                query = "insert into delivery(order_num, truck, driver, client, [load], material, depart_day, authority, [from], [to], distance)";
                query += "values(";
                query += "'" + deliv.orderNum + "', ";
                query += "'" + deliv.truck + "', ";
                query += "'" + deliv.driver + "', ";
                query += "'" + deliv.client + "', ";
                query += "'" + deliv.load + "', ";
                query += "'" + deliv.material + "', ";
                query += "'" + deliv.departDay + "', ";
                query += "'" + deliv.authority + "', ";
                query += "'" + deliv.from + "', ";
                query += "'" + deliv.to + "', ";
                query += "'" + deliv.distance + "'";
                query += ");";
                Util.query(query);
                return deliv.id;
            }
            else
            {
                query = "update delivery where id like " + deliv.id;
                query += "set order_num = '" + deliv.orderNum + "',";
                query += "truck = '" + deliv.truck + "',";
                query += "driver = '" + deliv.driver + "',";
                query += "client = '" + deliv.client + "',";
                query += "[load] = '" + deliv.load + "',";
                query += "material = '" + deliv.material + "',";
                query += "depart_day = '" + deliv.departDay + "',";
                query += "authority = '" + deliv.authority + "',";
                query += "[from] = '" + deliv.from + "',";
                query += "[to] = '" + deliv.to + "'";
                query += "where id like " + deliv.id + ";";
                Util.query(query);
                query = "select id from delivery where order_num like '" + deliv.orderNum + "' order by id desc";
                var reader = Util.query(query);
                reader.Read();
                int id = reader.GetInt32(0);
                return id;
            }
        }

        public static Delivery getInstance(int id)
        {
            var query = "SELECT order_num, truck, driver, client, [from], [to], material, [load], depart_day, authority, accepted, started, completed, distance, confirmation FROM DELIVERY WHERE ID LIKE '" + id + "'";
            var deliv = Util.query(query);
            if (deliv.HasRows)
            {
                Delivery delivery = new Delivery();
                while (deliv.Read())
                {
                    delivery.id = id;
                    delivery.orderNum = deliv.GetString(0);
                    delivery.truck = deliv.GetString(1);
                    delivery.driver = deliv.GetString(2);
                    delivery.client = deliv.GetInt32(3);
                    delivery.from = deliv.GetString(4);
                    delivery.to = deliv.GetString(5);
                    delivery.material = deliv.GetString(6);
                    delivery.load = deliv.GetInt32(7);
                    delivery.departDay = deliv.GetDateTime(8);
                    delivery.authority = deliv.GetString(9);
                    delivery.distance = deliv.GetInt32(13);
                    if (!deliv.IsDBNull(10))
                    {
                        delivery.accepted = deliv.GetDateTime(10);
                        if (!deliv.IsDBNull(11)) {
                            delivery.started = deliv.GetDateTime(11);
                            if (!deliv.IsDBNull(12)) {
                                delivery.completed = deliv.GetDateTime(12);
                            }
                        }
                        if(!deliv.IsDBNull(14))
                        {
                            delivery.confirmed = deliv.GetDateTime(14);
                        }
                    }
                }
                return delivery;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("it never read");
                return null;
            }
        }

        public string ETA()
        {
            if (started == null)
                return "--";
            else
            {
                string eta = "";
                var query = "select location, time from locations where delivery like " + id;
                var points = Util.query(query);
                var travelled = 0.0;
                if (points.HasRows)
                {
                    List<string> coords = new List<string>();
                    while (points.Read())
                        coords.Add(points.GetString(0));
                    travelled = Util.totalDistance(coords);
                }
                var todo = distance - travelled;
                var time = todo / 40;
                eta = Convert.ToString(Math.Round(time, 2)) + " Hours";
                return eta;
            }
        }
        

        public static string getAddress(string text)
        {
            try
            {
                return text.Split('#')[1];
            }
            catch (Exception ex) 
            {
                Util.print(ex.ToString());
            }
            return DriverHandle.INTERNAL_ERROR;
        }
           
        public static string getCoords(string text)
        {
            try
            {
                return text.Split('#')[0].Replace(',', '.');
            }
            catch (Exception ex)
            {
                Util.print(ex.ToString());
            }
            return DriverHandle.INTERNAL_ERROR;
        }

        public void setOrderNum(string value) { orderNum = value; }
        public void setTruck(string value) { truck = value; }
        public void setDriver(string value) { driver = value; }
        public void setClient(int value) { client = value; }
        public void setFrom(string value) { from = value; }
        public void setTo(string value) { to = value; }
        public void setMaterial(string value) { material = value; }
        public void setLoad(int value) { load = value; }
        public void setDepartDay(DateTime value) { departDay = value; }
        public void setArrivalDay(DateTime value) { arrivalDay = value; }
        public void setAuthority(string value) { authority = value; }
        public void setDistance(int distance) { this.distance = distance; }
        public void setConfirmed(DateTime time) { this.confirmed = time; }

        public int getID() {return id;}
        public string getOrderNumber() { return orderNum; }
        public Truck getTruck() { return new Truck(truck); }
        public Driver getDriver() { return new Driver(driver); }
        public Client getClient() { return new Client(client); }
        public string getToAddress() { return getAddress(to); }
        public string getFromAddress() { return getAddress(from); ; }
        public string getMaterial() { return material; }
        public int getLoad() { return load; }
        public DateTime getDepartDay() { return departDay; }
        public DateTime getArrivalDay() { return arrivalDay; }
        public Admin getAuthority() { return new Admin(authority); }
        public DateTime getAccepted() { return accepted; }
        public DateTime getStarted() { return started; }
        public DateTime getCompleted() { return completed; }
        public string getLocation() { return LastLocation(id)[0]; }
        public int getDistance() { return distance; }
        public DateTime getConfirmed() { return confirmed; }
    }
}
