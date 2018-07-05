using System;

namespace FMS.App_Code
{
    public class Delivery
    {
        private int id;
        private string orderNum;
        private string truck;
        private string driver;
        private int client;
        private string from;
        private string to;
        private string material;
        private int load;
        private DateTime departDay;
        private DateTime arrivalDay;
        private Admin authority;

        override
        public string ToString()
        {
            string output = "";
            output += "id=" + id + ";orderNum=" + orderNum + ";truck=" + truck + ";client=" + new Client(client).getName() + ";from=" + from + ";to=" + to + ";material=" + material + ";load=" + load + ";departday=" + departDay.ToString();
            return output;
        }

        public void save(Admin var)
        {
            authority = var;
            var query = "INSERT INTO DELIVERY(ORDER_NUM, TRUCK, DRIVER, CLIENT, [FROM], [TO], MATERIAL, [LOAD], DEPART_DAY, AUTHORITY) VALUES('" + orderNum + "', '" + truck + "', '" + driver + "', '" + client + "', '" + from + "', '" + to + "', '" + material + "', '" + load + "', '" + departDay.ToShortDateString() + "', '" + "1234567890123" + "');";
            Util.query(query);
        }

        public void save()
        {
            var query = "UPDATE DELIVERY WHERE ID LIKE '" + id +
                "SET TRUCK = '" + truck + "'" +
                "SET DRIVER = " + driver + "';";
            Util.query(query);
        }

        public static Delivery getInstance(int id)
        {
            var query = "SELECT order_num, truck, driver, client, [from], [to], material, [load], depart_day, authority FROM DELIVERY WHERE ID LIKE '" + id + "';";
            var deliv = Util.query(query);
            if (deliv.HasRows)
            {
                Delivery delivery = new Delivery();
                while (deliv.Read())
                {
                    delivery.id = id;
                    delivery.setOrderNum(deliv.GetString(0));
                    delivery.setTruck(deliv.GetString(1));
                    delivery.setDriver(deliv.GetString(2));
                    delivery.setClient(deliv.GetInt32(3));
                    delivery.setFrom(deliv.GetString(4));
                    delivery.setTo(deliv.GetString(5));
                    delivery.setMaterial(deliv.GetString(6));
                    delivery.setLoad(deliv.GetInt32(7));
                    delivery.setDepartDay(deliv.GetDateTime(8));
                    delivery.setAuthority(new Admin(deliv.GetString(9)));
                }
                return delivery;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("it never read");
                return null;
            }
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
        public void setAuthority(Admin value) { authority = value; }

        public string getOrderNumber() { return orderNum; }
        public Truck getTruck() { return new Truck(truck); }
        public Driver getDriver() { return new Driver(driver); }
        public Client getClient() { return new Client(client); }
        public string getTo() { return to; }
        public string getFrom() { return from; }
        public string geMaterial() { return material; }
        public int getLoad() { return load; }
        public DateTime getDepartDay() { return departDay; }
        public DateTime getArrivalDay() { return arrivalDay; }
        public Admin getAuthority() { return authority; }

    }
}