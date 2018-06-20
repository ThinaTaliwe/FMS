using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace FMS.App_Code
{
    public class ConnectionManager
    {
        private List<String> orders;

        public ConnectionManager()
        {
            orders = new List<string>();
            Thread t = new Thread(process);
            t.Start();
        }

        public void process()
        {
            while (true)
            {
                foreach (string order in orders)
                {
                    string[] parts = order.Split(' ');
                }
                Thread.Sleep(5000);
            }
        }

        public void addOrder(string order)
        {
            orders.Add(order);
        }
    }
}