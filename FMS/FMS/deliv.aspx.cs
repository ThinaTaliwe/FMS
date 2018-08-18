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
                        list.Items.Add(new ListItem(Convert.ToString(delivs.GetInt32(0))));
                    }
                }
            }
        }

        protected void getDeliv(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(list.SelectedValue);
            Delivery delivery = Delivery.getInstance(id);
            text.Text = delivery.ToString() + delivery.getTruck().getID();
        }
    }
}