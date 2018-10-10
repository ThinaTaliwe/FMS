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
        private Delivery d;// = new Delivery();

        protected void Page_Load(object sender, EventArgs e)
        {
            var HTMLStr = "";
            //DeliveryID
            string id = Request.QueryString["id"];
            // Response.Write("id: " + id);
            d = Delivery.getInstance(Convert.ToInt32(1));
            //Label1.Text = Delivery.ToString();
            HTMLStr += "<tr> <td> Delivery ID : " + d.getID() + "</td> " +
                        "</tr>" +
                        "<tr><td> Delivery Truck : <a href='TruckInfo?id=" + d.getTruck().getID() + "'>" + d.getTruck().getID() + "</a>" +
                        "<tr><td> Delivery Driver : " + d.getDriver().getName() + "</td>" +
                        "</tr>" +
                        "<tr><td> Delivery Client : " + d.getClient() .getCompany()+ "</td>" +
                        "</tr>" +
                         "<tr><td> Delivery Load : " + d.getLoad() + "</td>" +
                        "</tr>" +
                         "<tr><td> Delivery Distance : " + d.getDistance() + "</td>" +
                        "</tr>" +
                        "<tr><td> Delivery Material :  " + d.getMaterial() + "</td>" +
                        "</tr>";
            tables.InnerHtml = HTMLStr;
        }
        protected void report_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeliveryReport?id=" + d.getID());
        }
    }

}