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
            checkMessages();
            string message = "";
            
            notification.Text = message;
        }

        private string newNotif(int msgID, int delivID)
        {
            return null;
        }

        private void checkMessages()
        {
            List<string> lstMessages;
            object list = Session["notifs"];
            if (list == null)
                lstMessages = new List<string>();
            else
                lstMessages = list as List<string>;
            var drivers = Driver.getDriverList();
            foreach(var driver in drivers)
            {
                var msg = driver.getMessage();
                if (lstMessages.Contains(msg))
                    continue;
                else
                    lstMessages.Add(msg);
            }
        }
    }
}