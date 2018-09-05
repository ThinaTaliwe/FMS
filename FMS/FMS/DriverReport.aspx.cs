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

        protected void Page_Load(object sender, EventArgs e)
        {
            viewHourWorked();
        }

        protected void ViewHours(object sender, EventArgs e) {
            viewHourWorked();
        }

        private void viewHourWorked() {
            if(String.IsNullOrWhiteSpace(toDate.Value) || String.IsNullOrWhiteSpace(fromDate.Value)) {
                to = DateTime.Now;
                from = to.AddMonths(-1);
            } else {
                to = DateTime.Parse(toDate.Value);
                from = DateTime.Parse(fromDate.Value);
            }
            string query = "select id from users where user_type like 'driver'";
            var drivers = Util.query(query);
            if(drivers.HasRows) {
                List<string> lstDrivers = new List<string>();
                while (drivers.Read())
                    lstDrivers.Add(drivers.GetString(0));
                driverData.Value = "";
                foreach(string id in lstDrivers) {
                    Driver driver = new Driver(id);
                    var hours = driver.hoursWorked(from, to);
                    driverData.Value += driver.getName() + "*" + hours + "#";
                }
            }
        }
    }
}