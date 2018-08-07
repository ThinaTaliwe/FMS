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
        private DateTime accepted, started, completed;

        public string getFromCoords() {
            var coords = Util.getCoords(from);
            return coords[0] + ":" + coords[1];
        }

        public string getToCoords()
        {
            var coords = Util.getCoords(to);
            return coords[0] + ":" + coords[1];
        }

        override
        public string ToString()
        {
            string output = "";
            output += "id=" + id + ";orderNum=" + orderNum + ";truck=" + truck + ";client=" + new Client(client).getName() + ";from=" + from.Replace(' ', '+') + ";to=" + to.Replace(' ', '+') + ";material=" + material + ";load=" + load + ";departday=" + departDay.ToString();
            return output;
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
                    delivery.setOrderNum(deliv.GetString(0));
                    delivery.setTruck(deliv.GetString(1));
                    delivery.setDriver(deliv.GetString(2));
                    delivery.setClient(deliv.GetInt32(3));
                    delivery.setFrom(getCoords(deliv.GetString(4)));
                    delivery.setTo(getCoords(deliv.GetString(5)));
                    delivery.setMaterial(deliv.GetString(6));
                    delivery.setLoad(deliv.GetInt32(7));
                    delivery.setDepartDay(deliv.GetDateTime(8));
                    delivery.setAuthority(new Admin(deliv.GetString(9)));
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
        public void setAuthority(Admin value) { authority = value; }

        public int getID() {return id;}
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
        public DateTime getAccepted() { return accepted; }
        public DateTime getStarted() { return started; }
        public DateTime getCompleted() { return completed; }
        public string getLocation() { return LastLocation(id)[0]; }

    }
}