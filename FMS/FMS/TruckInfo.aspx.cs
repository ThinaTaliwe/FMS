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
            string id = Request.QueryString["id"];
            truck = new Truck(id);
            Label1.Text = truck.ToString();
        }

        protected void report_Click(object sender, EventArgs e)
        {
            Response.Redirect("TruckReport?id=" + truck.getID());
        }
    }
}