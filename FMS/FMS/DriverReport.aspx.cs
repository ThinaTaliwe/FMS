using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;

namespace FMS
{
    public partial class DriverReport : System.Web.UI.Page
    {
        private DateTime from, to;
        private List<Driver> lstDrivers;

        protected void Page_Load(object sender, EventArgs e)
        {
            string query = "select id from users where user_type like 'driver'";
            var drivers = Util.query(query);
            if (drivers.HasRows)
            {
                List<string> strDrivers = new List<string>();
                while (drivers.Read())
                    strDrivers.Add(drivers.GetString(0));
                lstDrivers = new List<Driver>();
                foreach (string id in strDrivers)
                {
                    Driver driver = new Driver(id);
                    lstDrivers.Add(driver);
                }
                if(!IsPostBack)
                {
                    
                    foreach(var driver in lstDrivers)
                    {
                        driverList.Items.Add(new ListItem(driver.getName() + " " + driver.getSurname()));
                    }
                }
                viewHoursWorked();
            }
        }

        protected void ViewHours(object sender, EventArgs e) {
            viewHoursWorked();
        }

        private void viewHoursWorked() {
            if(String.IsNullOrWhiteSpace(toDate.Value) || String.IsNullOrWhiteSpace(fromDate.Value)) {
                to = DateTime.Now;
                from = to.AddMonths(-1);
            } else {
                to = DateTime.Parse(toDate.Value);
                from = DateTime.Parse(fromDate.Value);
            }
            chartData.Value = "";
            foreach (Driver driver in lstDrivers)
            {
                var hours = driver.KmsDriven(from, to);
                chartData.Value += driver.getName() + "*" + hours + "#";
            }
            viewInfo();
        }

        private void viewInfo()
        {
            text.Text = "";
            foreach (var driver in lstDrivers)
            {
                var info = driverInfo(driver);
                if (info != null)
                    text.Text += info;
            }
        }

        public string driverInfo(Driver driver)
        {
            var delivs = driver.deliveriesMade(from, to);
            if(delivs != null)
            {
                string response = "Driver Info for: " + driver.getName() + "<br/>";
                response += "From: " + from + "<br/>";
                response += "To: " + to + "<br/>";
                foreach (var delivery in delivs)
                {
                    var json = delivery.speedInfo();
                    if (json != null)
                    {
                        response += "Delivery Order Num: " + delivery.getOrderNumber() + "<br/>";
                        response += "Client: " + delivery.getClient().getCompany() + "<br/>";
                        response += "Distance: " + json["distance"] + "<br/>";
                        response += "Average Speed: " + json["speed"] + "<br/>";
                        response += "Time (in hours): " + json["time"] + "<br/>";
                        response += "Overspeed Ratio (%): " + json["overspeed_ratio"] + "<br/>";
                    }
                }
                return response + "<br/>";
            }
            return null;
        }
    }
}