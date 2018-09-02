using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;

namespace FMS
{
    public partial class TruckReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = "select id from trucks";
            var trucks = Util.query(query);
            if (trucks.HasRows)
            {
                List<string> strTrucks = new List<string>();
                while (trucks.Read())
                {
                    strTrucks.Add(trucks.GetString(0));
                }
                foreach(var tr in strTrucks) {
                    var report = truckReport(tr);
                    var rep = report[0] + "*" + report[1] + "#";
                    Util.print(rep);
                    truckData.Value += rep;
                }
            }
        }

        public string[] truckReport(string truck)
        {
            Truck theTruck = new Truck(truck);
            return new string[] { truck, Convert.ToString(theTruck.totalDistance()) };
        }
    }
}