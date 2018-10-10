using FMS.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FMS
{
    public partial class ClientInfo : System.Web.UI.Page
    {
        private Client client_var;

        protected void Page_Load(object sender, EventArgs e)
        {
            var HTMLStr = "";
            // ClientID
            int id = Convert.ToInt32(Request.QueryString["id"]);
            Response.Write("id: " + id);
            client_var = new Client(id);
            Label1.Text = client_var.ToString();
            //Delist = client_var.getDeliveries();


            HTMLStr += "<tr> <td> Name : " + client_var.getName() + "</td> " +
                        "</tr>" +
                        "<tr><td> Company : " + client_var.getCompany() + "</td> " +
                        "</tr>" +
                        "<tr><td> Telephone : " + client_var.getTelephone() + "</td>" +
                        "</tr>" +
                        "<tr><td> E-Mail : " + client_var.getEmail() + "</td>" +
                        "</tr>" +
                        "<tr><td> Location : " + client_var.getLocation() + "</td>" +
                        "</tr>";
            tables.InnerHtml = HTMLStr;
        }
        protected void report_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientReport?id=" + client_var.getID());
        }
    }
}