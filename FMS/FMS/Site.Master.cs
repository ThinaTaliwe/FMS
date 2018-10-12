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
                timer.Interval = 30000;
            }
            tick();
        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            tick();
        }

        private void tick()
        {
            Util.print("tick()");
            string message = "";
            var lstMessages = checkMessages();
            Util.print("about to print count");
            Util.print(lstMessages.Count.ToString());
            foreach (var msg in lstMessages)
            {
                Util.print("message: " + msg);
                message += newNotif(msg[0], Convert.ToInt32(msg[1]), Convert.ToInt32(msg[2]));
            }
            Util.print(message);
            notification.InnerHtml = message;
            Util.print("tick() end");
        }

        private string newNotif(string driver, int delivID, int msgID)
        {
            Util.print("newNotif()");
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
            notif += "<input class=\"btn btn-default\"  type=\"button\" onclick=\"view('" + delivID +"')\" value=\"View\"/>";
            notif += "<input class=\"btn btn-default\"  type=\"button\" onclick=\"remove('" + delivID + "')\" value=\"Remove\"/>";
            notif += "<hr/>";
            notif += "</div>";
            Util.print(notif);
            return notif;
        }

        private List<string[]> checkMessages()
        {
            Util.print("checkMessages()");
            List<string[]> lstMessages;
            object list = Session["notifs"];
            if (list == null)
                lstMessages = new List<string[]>();
            else
                lstMessages = (List<string[]>) Session["notifs"];
            removeNotifs(lstMessages);
            var drivers = Driver.getDriverList();
            Util.print("driver list size: " + drivers.Count);
            foreach(var driver in drivers)
            {
                var msg = driver.getMessage();
                Util.print(driver.getName());
                if (msg == null)
                    continue;
                else
                {
                    var found = false;
                    foreach (var message in lstMessages)
                    {
                        if (message[0] == msg[0] && message[1] == msg[1])
                            found = true;
                    }
                    if (!found)
                    {
                        lstMessages.Add(msg);
                        Util.print(msg[0] + " message added");
                    }
                }
            }
            Session["notifs"] = lstMessages;
            Util.print("checkMessage() end");
            return lstMessages;
        }

        private void removeNotifs(List<string[]> lstMessages)
        {
            Util.print("removeNotifs()");
            var toBeKilled = removedNotifs.Value;
            Util.print(toBeKilled);
            var removed = toBeKilled.Split('#');
            List<int> lstRemoved = new List<int>();
            foreach (string id in removed)
            {
                try
                {
                    int intID = Convert.ToInt32(id);
                    lstRemoved.Add(intID);
                } catch(Exception ex)
                {
                    Util.print(ex.ToString());
                }
            }
            if(lstRemoved.Count > 0)
            {
                foreach (var msg in lstMessages)
                {
                    foreach (var killed in lstRemoved)
                    {
                        if (Convert.ToInt32(msg[1]) == killed)
                        {
                            lstMessages.Remove(msg);
                            var query = "update drivers set message = 0 where id like '" + msg[3] + "'";
                            Util.query(query);
                        }
                    }
                }
            }
            Util.print("removeNotifs() end");
        }
    }
}