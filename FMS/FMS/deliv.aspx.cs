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
    public partial class deliv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            Driver driver = new Driver("1234567770123");
            Truck truck = new Truck("DEF456MP");
            Response.Write("ekse");
            var jsonDri = driver.summary(now.AddMonths(-1), now);
            var jsonTru = truck.summary(now.AddMonths(-1), now);
            text.Text = jsonDri.ToString() + jsonTru.ToString();
        }
    }
}