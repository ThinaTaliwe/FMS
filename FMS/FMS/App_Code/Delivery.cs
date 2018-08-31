﻿using Newtonsoft.Json.Linq;
using System;

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

        public static Delivery homeRun(string driver, string truck, string authority) {
            var query = "select id from clients where name like 'mmeli'";
            var id = Util.query(query);
            id.Read();
            Client home = new Client(id.GetInt32(0));
            Delivery deliv = new Delivery();
            deliv.orderNum = "home";
            deliv.truck = truck;
            deliv.driver = driver;
            id.Read();
            deliv.client = home.getID();
            deliv.load = 0;
            deliv.material = "empty";
            deliv.departDay = DateTime.Now;
            deliv.authority = authority;
            deliv.to = getCoords(home.getLocation());
            deliv.from = new Driver(driver).lastLocation();
            Delivery.save(deliv);
            return deliv;
        }

        public void save() { Delivery.save(this); }

        public static void save(Delivery deliv)
        {
            var query = "select * from delivery where id like " + deliv.id;
            var isValid = Util.query(query);
            if (!isValid.HasRows)
            {
                query = "insert into delivery(order_num, truck, driver, client, [load], material, depary_day, authority, [from], [to])";
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
                query += "'" + deliv.to + "'";
                query += ");";
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
            }
            Util.query(query);
        }

        public static Delivery getInstance(int id)
        {
            var query = "SELECT order_num, truck, driver, client, [from], [to], material, [load], depart_day, authority, accepted, started, completed FROM DELIVERY WHERE ID LIKE '" + id + "'";
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
                    if (!deliv.IsDBNull(10))
                    {
                        delivery.accepted = deliv.GetDateTime(10);
                        if (!deliv.IsDBNull(11)) {
                            delivery.started = deliv.GetDateTime(11);
                            if (!deliv.IsDBNull(12)) {
                                delivery.completed = deliv.GetDateTime(12);
                            }
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

        public static string[] getDest(int id)
        {
            string query = "select [to] from delivery where id like '" + id + "'";
            var dest = Util.query(query);
            if (dest.HasRows)
            {
                dest.Read();
                string place = dest.GetString(0);
                return place.Split('#');
            }
            return null;
        }

        public static string[] getOrigin(int id)
        {
            string query = "select [from] from delivery where id like '" + id + "'";
            var origin = Util.query(query);
            if (origin.HasRows)
            {
                origin.Read();
                string place = origin.GetString(0);
                return place.Split('#');
            }
            return null;
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

        public int getID() {return id;}
        public string getOrderNumber() { return orderNum; }
        public Truck getTruck() { return new Truck(truck); }
        public Driver getDriver() { return new Driver(driver); }
        public Client getClient() { return new Client(client); }
        public string getToAddress() { return getAddress(to); }
        public string getFromAddress() { return getAddress(from); ; }
        public string geMaterial() { return material; }
        public int getLoad() { return load; }
        public DateTime getDepartDay() { return departDay; }
        public DateTime getArrivalDay() { return arrivalDay; }
        public Admin getAuthority() { return new Admin(authority); }
        public DateTime getAccepted() { return accepted; }
        public DateTime getStarted() { return started; }
        public DateTime getCompleted() { return completed; }
        public string getLocation() { return LastLocation(id)[0]; }

    }
}