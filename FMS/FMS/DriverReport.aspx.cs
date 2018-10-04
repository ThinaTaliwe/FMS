using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;
using Newtonsoft.Json.Linq;

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
                    driverList.Items.Add(new ListItem("Select a Driver"));
                    foreach(var driver in lstDrivers)
                    {
                        driverList.Items.Add(new ListItem(driver.getName() + " " + driver.getSurname()));
                    }
                }
                viewHoursWorked();
            }
        }

        protected void ViewHours(object sender, EventArgs e) {
            setSelectedText();
            var choice = driverList.SelectedValue.ToString();
            if (choice == "Select a Driver")
                viewHoursWorked();
            else
            {
                var parts = choice.Split(' ');
                foreach(var driver in lstDrivers)
                {
                    if(driver.getName() == parts[0] && driver.getSurname() == parts[1])
                    {

                    }
                }
            }
        }

        private void setGraph(string title, string yAxisTitle, string legendText, List<string[]> values)
        {
            JObject json = new JObject();
            json["title"] = title;
            json["y_axis_title"] = yAxisTitle;
            json["legend_text"] = legendText;
            chart.Value = json.ToString();
            string data = "";
            foreach(var point in values)
                data += point[0] + "*" + point[1] + "#";
            chartData.Value = data;
        }

        private void viewHoursWorked() {
            if(String.IsNullOrWhiteSpace(toDate.Value) || String.IsNullOrWhiteSpace(fromDate.Value)) {
                to = DateTime.Now;
                from = to.AddMonths(-1);
            } else {
                to = DateTime.Parse(toDate.Value);
                from = DateTime.Parse(fromDate.Value);
            }
            List<string[]> data = new List<string[]>();
            foreach (Driver driver in lstDrivers)
            {
                var hours = driver.KmsDriven(from, to);
                data.Add(new string[] { driver.getName(), hours.ToString() });
            }
            setGraph("Driver Trips", "Kilometers (Km)", "Individual Trucks", data);
            setSelectedText();
            viewInfo();
        }

        private void setSelectedText()
        {
            string text = "";
            text += "Report Period" + "<br/>";
            text += "From: " + from.Date.ToString() + "<br/>";
            text += "To: " + to.Date.ToString() + "<br/>";
            text += "Selected Driver: " + driverList.SelectedValue.ToString() + "<br/>";
            reportText.Text = text;
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