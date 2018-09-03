﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
    public class Truck
    {
        private string id{ get; set; }
        private int load { get; set; }
        private int speed { get; set; }

        public Truck(string id)
        {
            var query = "select id, [load], speed from trucks where id like '" + id + "';";
            var truck = Util.query(query);
            if(truck.HasRows) {
                if (truck.Read())
                {
                    this.id = truck.GetString(0);
                    load = truck.GetInt32(1);
                    speed = truck.GetInt32(2);
                }
                else
                    Util.print("no truck read");
            }
        }

        public double totalDistance() {
            double distance = 0;
            try {
                var query = "select id from delivery where truck like '" + id  + "'";
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

        public string getID() { return id; }
        public int getLoad() { return load; }
        public int getSpeed() { return speed; }

    }
}