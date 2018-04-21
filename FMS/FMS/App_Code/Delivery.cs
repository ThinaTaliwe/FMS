using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
    public class Delivery
    {
        private int id;
        private int orderNum;
        private Truck truck;
        private Driver driver;
        private string from;
        private string to;
        private string material;
        private int load;
        private DateTime departDay;
        private DateTime arrivalDay;
        private User authority;

        public Driver Driver { get => driver; set => driver = value; }
    }
}