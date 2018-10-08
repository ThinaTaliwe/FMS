using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code; 

namespace FMS
{
    public partial class ViewAllTrucks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = "select id from Trucks";
            var rows = Util.query(query);
            var HTMLStr = "";
            List<string> lstIDs = new List<string>();
            if (rows.HasRows)
            {
                while (rows.Read())
                    lstIDs.Add(rows.GetString(0));
                foreach(var id in lstIDs)
                {
                    Truck truck = new Truck(id);
                    HTMLStr += "<tr>  <td> <a href='TruckInfo?id=" + truck.getID() + "'>" + truck.getID() + "</a></td> <td> " + truck.getBrand() + "</td> <td> " + truck.getLoad() + "</td><td> " + truck.getSpeed() + "</td><td> " + truck.getClass_code() + "</td></tr>";
                }
                tables.InnerHtml = HTMLStr;
            }
        }
    }
}