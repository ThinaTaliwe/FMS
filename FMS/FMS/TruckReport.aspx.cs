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
            
        }

        public string[] truckReport(string truck) {
            var query = "select id from trucks where id like '" + truck + "'";
            var tr = Util.query(query);
            if(tr.HasRows) {
                tr.Read();
                Truck theTruck = new Truck(tr.GetString(0));
                return new string [] {truck, Convert.ToString(theTruck.totalDistance())}
            }
            return null;
        }
    }
}