using FMS.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FMS
{
    public partial class DeliveryInfo : System.Web.UI.Page
    {
        private Delivery d;

        protected void Page_Load(object sender, EventArgs e)
        {
            var HTMLStr = "";
            // ClientID
            string id = Request.QueryString["id"];
            d = new Delivery();
            //Label1.Text = truck.ToString();
            //Delist = client_var.getDeliveries();


            HTMLStr += "<tr> <td> Client : " + d.getClient() + "</td> " +
                        "</tr>";
            tables.InnerHtml = HTMLStr;
        }
        protected void report_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeliveryReport?id=" + d.getID());
        }
    }

}