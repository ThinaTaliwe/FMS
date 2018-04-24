using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
    public class Truck
    {
        public string id;
        private int load;
        private int speed;

        public Truck(string id)
        {
            var query = "SELECT ID, [LOAD], SPEED FROM TRUCKS WHERE ID LIKE '" + id + "'";
            var truck = Util.query(query);
            if (truck.Read())
            {
                id = Convert.ToString(truck.GetString(0));
                load = Convert.ToInt32(truck.GetInt32(1));
                speed = Convert.ToInt32(truck.GetInt32(2));
            }
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string getID()
        {
            
            return id;
        }
        public int getLoad() { return load; }
        public int getSpeed() { return speed; }

    }
}