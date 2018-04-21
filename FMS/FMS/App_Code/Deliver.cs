using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.App_Code
{
    interface Deliver
    {
         Delivery createDilevery(Truck truck, Driver driver);
         bool assignDriver(Driver driver);
         void assignRoute();
         bool assignTruck(Truck truck);
         void startDelivery();
         void setTrip(String to, String from);
    }
}
