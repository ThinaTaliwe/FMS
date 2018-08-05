using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;

namespace FMS
{
    public partial class MonitorTrucks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string query = "select id from delivery where accepted is not null";
            var accept_ids = Util.query(query);
            List<int> ids = new List<int>();
            if(accept_ids.HasRows) {
                while(accept_ids.Read()) {
                    ids.Add(accept_ids.GetInt32(0));
                }
            }
            string strTruck = "";
            foreach (int id in ids) { 
                Delivery deliv = Delivery.getInstance(Convert.ToInt32(id));
                Util.print(deliv.ToString());
                var loc = Delivery.LastLocation(deliv.getID());
                string tr = deliv.getDriver().getName() + "*" + deliv.getTruck().getID() + "*" + loc[0] + "*" + loc[1] + " ";
                strTruck += tr;
            }
            trucks.Value = strTruck;
            Util.print(strTruck);
        }
    }
}