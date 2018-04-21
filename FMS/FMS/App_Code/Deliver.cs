using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.App_Code
{
    interface Deliver
    {
        public Delivery createDilevery(Truck truck, Driver driver);
        public Bool assignDriver(Driver driver);
        public void assignRoute();
        public Bool assignTruck(Truck truck);
        public void startDelivery();
        public void setTrip(String to, String from);
    }
}
