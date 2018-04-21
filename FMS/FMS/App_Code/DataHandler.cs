using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
    /// <summary>
    /// Truck class for all classes created
    /// </summary>
    public class DataHandler : User
    {
            // dataHandler methods
            public Delivery[] viewPastDeliveries(int weeks)
            {
            Delivery[] delArr = new Delivery[weeks];

            return delArr;
            }
            public string generateReport(Delivery deliveries)
        {
            string report = "";

            return report;
        }
            public void validateDelivery(Delivery deliveries)
        {

        }
        
    }
}