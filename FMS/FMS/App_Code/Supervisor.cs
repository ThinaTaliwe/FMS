using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
        public class Supervisor : User
        {
            // supervisor attributes
            private Delivery delivery;

            // supervisor methods
            public Driver[] viewDrivers(int numDrivers)
            {
                Driver[] driverArr = new Driver[numDrivers];
                for (int i = 0; i < driverArr.Length - 1; i++)
                {
                    Console.WriteLine("Driver " + i + " " + delivery.Driver);
                    driverArr[i] = new Driver(); 
                }
                return driverArr;
            }
            public void editDriver(Driver driver)
            {

            }
            public bool removeDriver(Driver driver)
            {
            return false;
            }

        }

}