using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
    public class Delivery
    {
        private int id;
        private string orderNum;
        private string truck;
        private string driver;
        private Client client;
        private string from;
        private string to;
        private string material;
        private int load;
        private DateTime departDay;
        private DateTime arrivalDay;
        private string authority;

        public void save(string var)
        {
            authority = var;
            var query = "INSERT INTO DELIVERY(ORDER_NUM, TRUCK, DRIVER, CLIENT, [FROM], [TO], MATERIAL, [LOAD], DEPART_DAY, AUTHORITY) VALUES('" + orderNum + "', '" + truck + "', '" + driver + "', '" + client.getID() + "', '" + from + "', '" + to + "', '" + material + "', '" + load + "', '" + departDay.ToShortDateString() + "', '" + "1234567890123" + "');";
            System.Diagnostics.Debug.WriteLine(query);
            Util.query(query);
        }

        public void setOrderNum(string value) { orderNum = value; }
        public void setTruck(string value) {
            truck = value;
        }
        public void setDriver(string value) { driver = value; }
        public void setClient(Client value) { client = value; }
        public void setFrom(string value) { from = value; }
        public void setTo(string value) { to = value; }
        public void setMaterial(string value) { material = value; }
        public void setLoad(int value) { load = value; }
        public void setDepartDay(DateTime value) { departDay = value; }
        public void setArrivalDay(DateTime value) { arrivalDay = value; }
        public void setAuthority(string value) { authority = value; }

        public string getOrderNumber() { return orderNum; }
        public string getTruck() { return truck; }
        public string getDriver() { return driver; }
        public Client getClient() { return client; }
        public string getTo() { return to; }
        public string getFrom() { return from; }
        public string geMaterial() { return material; }
        public int getLoad() { return load; }
        public DateTime getDepartDay() { return departDay; }
        public DateTime getArrivalDay() { return arrivalDay; }
        public string getAuthority() { return authority; }

    }
}