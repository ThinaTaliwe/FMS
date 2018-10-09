using FMS.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FMS
{
    public partial class DriverInfo : System.Web.UI.Page
    {
          private Driver objDriver;

        protected void Page_Load(object sender, EventArgs e)
        {
            var HTMLStr = "";
            // ClientID
            string id = Request.QueryString["id"];
            objDriver = new Driver(id);
            //Label1.Text = truck.ToString();
            //Delist = client_var.getDeliveries();


            HTMLStr += "<tr> <td> Name : " + objDriver.getName() + "</td> " +
                        "</tr>" +
                        "<tr><td> Location : " + objDriver.getMessage() + "</td>" +
                        "</tr>";
            tables.InnerHtml = HTMLStr;
        }
        protected void report_Click(object sender, EventArgs e)
        {
            Response.Redirect("DriverReport?id=" + objDriver.getID());
        }
    }
}