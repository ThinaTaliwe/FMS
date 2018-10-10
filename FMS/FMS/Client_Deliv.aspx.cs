using FMS.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FMS
{
    public partial class Client_Deliv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["order"];
            Response.Write(id);
            //ordernum.Text = id;
            //Display Info regarding the asked order 
            var query = "SELECT order_num, truck, driver, delivery_day, material, load, FROM DELIVERY WHERE ID LIKE '" + id + "'";
            System.Diagnostics.Debug.WriteLine(query);
            var rows = Util.query(query);

            //ordernum.Text = "Bye"; 
            var HTMLStr = "";
            if (rows.HasRows)
            {
            }
                tables.InnerHtml = HTMLStr;
            }
             
        }
}
