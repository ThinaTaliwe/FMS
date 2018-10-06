using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;

namespace FMS
{
    public partial class Home : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            var query = "select id from Delivery WHERE COMPLETED is null AND (Month(DEPART_DAY) = Month(getdate()) AND YEAR(DEPART_DAY) = YEAR(getdate())) order by id desc";
            var rows = Util.query(query);
            //var client = Util.getClient() 
            var HTMLStr = "";
            if (rows.HasRows)
            {
                List<int> lstIds = new List<int>();
                while (rows.Read())
                    lstIds.Add(rows.GetInt32(0));
                Delivery delivery;
                foreach(var id in lstIds)
                {
                    delivery = Delivery.getInstance(id);
                    var accepted = delivery.getAccepted() == null ? "Yes" : "No";
                    HTMLStr += "<tr> <td> " + delivery.getClient().getCompany() + "</td> <td> " + delivery.getFromAddress() + "</td> <td> " + delivery.getToAddress() + "</td> <td> " + delivery.getDriver().getName() + " " + delivery.getDriver().getSurname() + "</td> <td> " + delivery.ETA() + "</td> <td> " + accepted + "</td> </tr>";
                }
                tables.InnerHtml = HTMLStr;
            }
            
        }
    }
}