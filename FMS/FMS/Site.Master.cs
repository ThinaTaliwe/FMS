using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;

namespace FMS
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login");
            }
            else
            {
                string name = Session["user"] as string;
                user.InnerText = new User(name).getName();
            }
            if (!timer.Enabled)
            {
                timer.Enabled = true;
                timer.Interval = 100000;
            }
        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            List<string> lstDrivers = new List<string>();
            var query = "select id from users where user_type like 'driver'";
            var drivers = Util.query(query);
            while(drivers.Read())
                lstDrivers.Add(drivers.GetString(0));
            string message = "";
            foreach(string id in lstDrivers)
            {
                Driver driver = new Driver(id);
                string part = String.Format("Driver: {0}, message {1} \n", driver.getName(), driver.getMessage());
                message += part;
            }
            notification.Text = message;
        }
    }
}