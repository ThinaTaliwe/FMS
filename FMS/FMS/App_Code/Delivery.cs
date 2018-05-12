using System;

namespace FMS.App_Code
{
    public class Delivery
    {
        private int id;
        private string orderNum;
        private Truck truck;
        private Driver driver;
        private Client client;
        private string from;
        private string to;
        private string material;
        private int load;
        private DateTime departDay;
        private DateTime arrivalDay;
        private Admin authority;

        public void save(Admin var)
        {
            authority = var;
            var query = "INSERT INTO DELIVERY(ORDER_NUM, TRUCK, DRIVER, CLIENT, [FROM], [TO], MATERIAL, [LOAD], DEPART_DAY, AUTHORITY) VALUES('" + orderNum + "', '" + truck.getID() + "', '" + driver.getID() + "', '" + client.getID() + "', '" + from + "', '" + to + "', '" + material + "', '" + load + "', '" + departDay.ToShortDateString() + "', '" + "1234567890123" + "');";
            System.Diagnostics.Debug.WriteLine(query);
            Util.query(query);
        }

        public static Delivery getInstance(string orderNum)
        {
            var query = "SELECT * FROM DELIVERY WHERE ORDER_NUM LIKE '" + orderNum + "';";
            var deliv = Util.query(query);
            if (deliv.HasRows)
            {
                Delivery delivery = new Delivery();
                delivery.setOrderNum(deliv.GetString(1));
                delivery.setTruck(new Truck(deliv.GetString(2)));
                delivery.setDriver(new Driver(deliv.GetString(3)));
                delivery.setClient(new Client(deliv.GetInt32(4)));
                delivery.setFrom(deliv.GetString(5));
                delivery.setTo(deliv.GetString(6));
                delivery.setMaterial(deliv.GetString(7));
                delivery.setLoad(deliv.GetInt32(8));
                delivery.setDepartDay(deliv.GetDateTime(9));
                delivery.setArrivalDay(deliv.GetDateTime(10));
                delivery.setAuthority(new Admin(deliv.GetString(11)));
                return delivery;
            }
            else
                return null;
        }

        public void setOrderNum(string value) { orderNum = value; }
        public void setTruck(Truck value) { truck = value;  }
        public void setDriver(Driver value) { driver = value; }
        public void setClient(Client value) { client = value; }
        public void setFrom(string value) { from = value; }
        public void setTo(string value) { to = value; }
        public void setMaterial(string value) { material = value; }
        public void setLoad(int value) { load = value; }
        public void setDepartDay(DateTime value) { departDay = value; }
        public void setArrivalDay(DateTime value) { arrivalDay = value; }
        public void setAuthority(Admin value) { authority = value; }

        public string getOrderNumber() { return orderNum; }
        public Truck getTruck() { return truck; }
        public Driver getDriver() { return driver; }
        public Client getClient() { return client; }
        public string getTo() { return to; }
        public string getFrom() { return from; }
        public string geMaterial() { return material; }
        public int getLoad() { return load; }
        public DateTime getDepartDay() { return departDay; }
        public DateTime getArrivalDay() { return arrivalDay; }
        public Admin getAuthority() { return authority; }

    }
}