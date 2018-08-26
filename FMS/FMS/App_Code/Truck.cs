using System;
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

        public string getID() { return id; }
        public int getLoad() { return load; }
        public int getSpeed() { return speed; }

    }
}