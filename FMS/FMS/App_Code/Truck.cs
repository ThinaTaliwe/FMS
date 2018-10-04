using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
    public class Truck
    {
        private string id{ get; set; }
        private string brand { get; set; }
        private int load { get; set; }
        private int speed { get; set; }
        private int class_code { get; set; }

        public Truck(string id)
        {
            var query = "select id, brand, [load], speed, class_code from trucks where id like '" + id + "';";
            var truck = Util.query(query);
            if(truck.HasRows) {
                if (truck.Read())
                {
                    this.id = truck.GetString(0);
                    brand = truck.GetString(1);
                    load = truck.GetInt32(2);
                    speed = truck.GetInt32(3);
                    class_code = truck.GetInt32(4);
                }
                else
                    Util.print("no truck read");
            }
        }

        public double totalDistance(DateTime from, DateTime to) {
            double distance = 0;
            try {
                var query = "select id from delivery where truck like '" + id  + "' and started > '" + from + "' and started < '" + to + "'";
                var ids = Util.query(query);
                if(ids.HasRows) {
                    List<int> strIDS = new List<int>();
                    while (ids.Read())
                        strIDS.Add(ids.GetInt32(0));
                    foreach(int deliv in strIDS) {
                        query = "select location from locations where delivery like '" + deliv + "' order by time asc";
                        var locations = Util.query(query);
                        if(locations.HasRows)
                        {
                            List<string> coords = new List<string>();
                            while (locations.Read())
                                coords.Add(locations.GetString(0));
                            var diff = Util.totalDistance(coords);
                            distance += diff;
                            Util.print("distance: " + diff + " delivery: " + deliv);
                        }
                    }
                }
            } catch (Exception ex) {
                Util.print(ex.ToString());
            } return distance;
        }

        public static List<Truck> getTruckList()
        {
            List<string> lstIDS = new List<string>();
            List<Truck> lstTrucks = new List<Truck>();
            var query = "select id from trucks";
            var reader = Util.query(query);
            if(reader.HasRows)
            {
                while (reader.Read())
                    lstIDS.Add(reader.GetString(0));
                foreach (var id in lstIDS)
                    lstTrucks.Add(new Truck(id));
                return lstTrucks;
            }
            return null;
        }

        public string getID() { return id; }
        public string getBrand() { return brand; }
        public int getLoad() { return load; }
        public int getSpeed() { return speed; }
        public int getClass_code() { return class_code; }

    }
}