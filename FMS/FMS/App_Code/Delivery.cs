using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
    public class Delivery
    {
        private int id {get;}
        private int orderNum;
        private Truck truck { get; set; }
        private Driver driver { get; set; }
        private string from { get; set;  }
        private string to { get; set; }
        private string material { get; set; }
        private int load { get; set; } 
        private DateTime departDay { get; set; }
        private DateTime arrivalDay { get; set; }
        private User authority { get; set; }



    }
}