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
            viewTruckRepot();
        }

        public string[] truckReport(string truck)
        {
            try {
                Truck theTruck = new Truck(truck);
                DateTime from, to;
                if (String.IsNullOrWhiteSpace(fromDate.Value)|| String.IsNullOrWhiteSpace(toDate.Value))
                {
                    to = DateTime.Now;
                    from = to.AddMonths(-1);
                } else {
                    Util.print(fromDate.Value + toDate.Value);
                    from = DateTime.Parse(fromDate.Value);
                    to = DateTime.Parse(toDate.Value);
                }
                return new string[] { truck, Convert.ToString(theTruck.totalDistance(from, to)) };
            } catch(Exception ex) {
                Util.print(ex.ToString());
            } return null;
        }

        protected void view_Click(object sender, EventArgs e)
        {
            viewTruckRepot();
        }

        private void viewTruckRepot() {
            var query = "select id from trucks";
            var trucks = Util.query(query);
            if (trucks.HasRows)
            {
                List<string> strTrucks = new List<string>();
                while (trucks.Read())
                {
                    strTrucks.Add(trucks.GetString(0));
                }
                truckData.Value = "";
                foreach (var tr in strTrucks)
                {
                    var report = truckReport(tr);
                    if(report != null) {
                        var rep = report[0] + "*" + report[1] + "#";
                        Util.print(rep);
                        truckData.Value += rep;
                    }
                }
            }
        }
    }
}