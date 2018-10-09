using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;

namespace FMS
{
    public partial class TruckInfo : System.Web.UI.Page
    {
        private Truck truck;

        protected void Page_Load(object sender, EventArgs e)
        {
            var HTMLStr = "";
            //TruckID
            string id = Request.QueryString["id"];
            truck = new Truck(id);
            //Label1.Text = truck.ToString();
            HTMLStr += "<tr> <td> Number Plate:     " + truck.getID() + "</td> " +
                        "</tr>" +
                        "<tr><td> Truck Brand:      " + truck.getBrand() + "</td> " +
                        "</tr>" +
                        "<tr><td> Load Capacity:    " + truck.getLoad() + "</td>" +
                        "</tr>" +
                        "<tr><td> Maximum Speed:    " + truck.getSpeed() + "</td>" +
                        "</tr>" +
                        "<tr><td> Licence Code:     " + truck.getClass_code() + "</td>" +
                        "</tr>";
                        tables.InnerHtml = HTMLStr;
        }
        protected void report_Click(object sender, EventArgs e)
        {
            Response.Redirect("TruckReport?id=" + truck.getID());
        }
    }
}