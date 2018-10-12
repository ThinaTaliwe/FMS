using FMS.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FMS
{
    public partial class AllDeliveries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = "select id from Delivery order by id desc";
            var rows = Util.query(query);
            //var client = Util.getClient() 
            var HTMLStr = "";
            if (rows.HasRows)
            {
                List<int> lstIds = new List<int>();
                while (rows.Read())
                    lstIds.Add(rows.GetInt32(0));
                Delivery delivery;
                foreach (var id in lstIds)
                {
                    delivery = Delivery.getInstance(id);
                    var accepted = "";
                    if (delivery.getAccepted() == null) accepted = "No";
                    else accepted = delivery.getAccepted().TimeOfDay.ToString();
                    HTMLStr += "<tr> <td> " + delivery.getClient().getCompany() + "</td> <td> " + delivery.getFromAddress() + "</td> <td> " + delivery.getToAddress() + "</td> <td> " + delivery.getDriver().getName() + " " + delivery.getDriver().getSurname() + "</td> <td> " + delivery.ETA() + "</td> <td>  < a href = 'ClientInfo?id=" + delivery.getID() + "></a> accepted </td> </tr>";
                }
                tables.InnerHtml = HTMLStr;
            }

        }
    }
}