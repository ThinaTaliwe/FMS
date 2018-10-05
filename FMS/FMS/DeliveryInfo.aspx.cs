using FMS.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FMS
{
    public partial class DeliveryInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string check = Request.QueryString["id"]; ;
            Delivery delivery = Delivery.getInstance(Convert.ToInt32(check));

            //Dname.InnerHtml = delivery.ToString();
            orderNum.InnerHtml = delivery.getLocation();
            //if (delivery.HasRows)
            //{
            //    while (delivery.Read())
            //    {
            //        HTMLStr += "<tr>  <td> <a href='TruckInfo.aspx'>" + rows.GetString(0) + "</a></td> <td> " + rows.GetString(1) + "</td> <td> " + rows.GetInt32(2) + "</td><td> " + rows.GetInt32(3) + "</td><td> " + rows.GetString(4) + "</td></tr>";
            //    }
            //    tables.InnerHtml = HTMLStr;
            //}

        }
    }
}