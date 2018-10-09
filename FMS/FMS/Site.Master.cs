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
            var lstMessages = Session["notifs"] as List<string[]>;
            foreach(var msg in lstMessages)
            {
                message += newNotif(msg[0], Convert.ToInt32(msg[1]), Convert.ToInt32(msg[2]));
            }
            notification.InnerText = message;
        }

        private string newNotif(string driver, int delivID, int msgID)
        {
            string notif = "<div id=\"" + delivID + "\">";
            notif += "Driver: " + driver + "<br/>";
            switch (msgID)
            {
                case 1:
                    notif += "Accepted assignment<br/>";
                    break;
                case 2:
                    notif += "Found delivery site empty<br/>";
                    break;
            }
            notif += "<input type=\"button\" onclick=\"view('"+ delivID +"')\" value=\"View\"/>";
            notif += "<input type=\"button\" onclick=\"remove('" + delivID + "')\" value=\"Remove\"/>";
            notif += "<hr/>";
            notif += "</div>";
            return notif;
        }

        private void checkMessages()
        {
            var removed = removedNotifs.Value.Split('#');
            List<int> lstRemoved = new List<int>();
            foreach(string id in removed)
            {
                int intID = Convert.ToInt32(id);
                lstRemoved.Add(intID);
            }
            List<string[]> lstMessages;
            object list = Session["notifs"];
            if (list == null)
                lstMessages = new List<string[]>();
            else
                lstMessages = list as List<string[]>;
            foreach(var msg in lstMessages)
            {
                foreach(var killed in lstRemoved)
                {
                    if (Convert.ToInt32(msg[1]) == killed)
                        lstMessages.Remove(msg);
                }
            }
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