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
            date();
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
                    driverList.Items.Add(new ListItem("All Drivers"));
                    foreach(var driver in lstDrivers)
                    {
                        driverList.Items.Add(new ListItem(driver.getName() + " " + driver.getSurname()));
                    }
                }
                string dr = Request.QueryString["id"];
                if (!String.IsNullOrEmpty(dr))
                {
                    Driver Driv = new Driver(dr);
                    driverList.SelectedValue = Driv.getName() + " " + Driv.getSurname();
                }
                report();
            }
        }

        private void report()
        {
            setSelectedText();
            var choice = driverList.SelectedValue.ToString();
            if (choice == "All Drivers")
                viewHoursWorked();
            else
            {
                var parts = choice.Split(' ');
                foreach (var driver in lstDrivers)
                {
                    if (driver.getName() == parts[0] && driver.getSurname() == parts[1])
                    {
                        chartData.Value = "none";
                        var json = driver.summary(from, to);
                        var html = "<br/><br/>Driver: " + driver.getName() + "<br/>";
                        html += "Total Distance Driven (km): " + json["km"] + "<br/>";
                        html += "Total Time on Road (hours): " + json["time"] + "<br/>";
                        driverInfo.InnerHtml = html;
                        return;
                    }
                }
            }
        }

        protected void ViewHours(object sender, EventArgs e) {
            report();
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

        private void date()
        {
            if (String.IsNullOrWhiteSpace(toDate.Value) || String.IsNullOrWhiteSpace(fromDate.Value))
            {
                to = DateTime.Now;
                from = to.AddMonths(-1);
            }
            else
            {
                to = DateTime.Parse(toDate.Value);
                from = DateTime.Parse(fromDate.Value);
            }
        }

        private void viewHoursWorked() {
            List<string[]> data = new List<string[]>();
            foreach (Driver driver in lstDrivers)
            {
                var hours = driver.KmsDriven(from, to);
                data.Add(new string[] { driver.getName(), hours.ToString() });
            }
            setGraph("Driver Trips", "Kilometers (Km)", "Individual Trucks", data);
            setSelectedText();
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
    }
}