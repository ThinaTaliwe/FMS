using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;

namespace FMS
{
    public partial class deliv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack) {
                string query = "select id from delivery";
                var delivs = Util.query(query);
                if (delivs.HasRows)
                {
                    while (delivs.Read())
                    {
                        delivery.Items.Add(new ListItem(Convert.ToString(delivs.GetInt32(0))));
                    }
                }
                query = "select id from users where user_type like 'driver'";
                var dr = Util.query(query);
                if(dr.HasRows) {
                    while (dr.Read())
                        driver.Items.Add(new ListItem(dr.GetString(0)));
                }
                query = "select id from trucks";
                var tr = Util.query(query);
                if(tr.HasRows) {
                    while (tr.Read())
                        truck.Items.Add(new ListItem(tr.GetString(0)));
                }
            }
        }

        protected void getDeliv(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(delivery.SelectedValue);
            string tr, dr;
            tr = truck.SelectedValue;
            dr = driver.SelectedValue;
            Delivery d = Delivery.getInstance(id);
            Truck t = new Truck(tr);
            Driver dri = new Driver(dr);
            string query = "select location from locations where delivery like '" + delivery.SelectedValue + "' order by time asc";
            var reader = Util.query(query);
            List<string> list = new List<string>();
            if(reader.HasRows) {
                while (reader.Read())
                    list.Add(reader.GetString(0));
            }

            text.Text = t.getID() + "  " + dri.getName() + Util.totalDistance(list);// + d.ToString();
        }
    }
}