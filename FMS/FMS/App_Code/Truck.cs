using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
    /// <summary>
    /// Truck class for all classes created
    /// </summary>
    public class Truck
    {
        // attributes
        private string ID;
        private int maxLoad;
        private int maxSpeed;

        // getter and setter
        public string getID()
        {
            return this.ID;
        }
        public int getMaxLoad()
        {
            return this.maxLoad;
        }
        public int getMaxSpeed()
        {
            return this.maxSpeed;
        }

        public void setID(string ID)
        {
            this.ID = ID;
        }
        public void setMaxLoad(int maxLoad)
        {
            this.maxLoad = maxLoad;
        }
        public void setMaxSpeed(int maxSpeed)
        {
            this.maxSpeed = maxSpeed;
        }

        // methods
    }
}