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
            var row = Util.query(query);

            if (row.HasRows)
            {
                //ordernum.Text = "Hey"; 
                while (row.Read())
                {
                    //ordernum.Text = Convert.ToString(row.GetString(1));
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(Convert.ToString(row.GetInt32(0)));
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    Text1.Value = Convert.ToString(row.GetInt32(0));
                    ClientSelected.Value = Convert.ToString(row.GetInt32(1));
                    DeliveryDateSelected.Value = Convert.ToString(row.GetDateTime(2));
                    MaterialSelected.Value = row.GetString(3);
                    //TruckChosen.Value = row.GetString(2); 
                    //DriverChosen.Value = row.GetString(3); 
                    //StartRoute.Value = row.GetString(5);
                    //EndRoute.Value = row.GetString(6);
                    LoadSelected.Value = Convert.ToString(row.GetInt32(4));
                }
            }
            //ordernum.Text = "Bye"; 

            var query = "select [from], [to], truck, driver, client, accepted from Delivery WHERE COMPLETED is null AND (Month(DEPART_DAY) = Month(getdate()) AND YEAR(DEPART_DAY) = YEAR(getdate())) order by id desc";
            var rows = Util.query(query);
            //var client = Util.getClient() 
            var HTMLStr = "";
            if (rows.HasRows)
            {
                var assignedStr = "";
                while (rows.Read())
                {
                    Delivery deliv = Delivery.getInstance(10);
                    try
                    {
                        var date = rows.GetDateTime(5);
                        if (date != null) assignedStr = "Yes";
                        else assignedStr = "No";
                    }
                    catch (Exception ex)
                    {
                        Util.print(ex.ToString());
                        assignedStr = "No";
                    }
                    var Driver_MmeliThing = new Driver(rows.GetString(3));
                    HTMLStr += "<tr> <td> " + new Client(rows.GetInt32(4)).getCompany() + "</td> <td> " + Delivery.getAddress(rows.GetString(0)) + "</td> <td> " + Delivery.getAddress(rows.GetString(1)) + "</td> <td> " + Driver_MmeliThing.getName() + " " + Driver_MmeliThing.getSurname() + "</td> <td> " + "--" + "</td> <td> " + assignedStr + "</td> </tr>";
                }
                tables.InnerHtml = HTMLStr;
            }
             < h3>Current Delivery</h3>
                        <p>Order Number: </p>
                        <p>Truck Plate: </p>
                        <p>Driver Name: </p>
                        <p>Origin: </p>
                        <p>Destination: </p>
                        <p>Delivery Day: </p>
                        <p>Material: </p>
                        <p>Load: </p>
                        <p>Started: </p>
                        <p>ETA/Time Complete: </p>
        }
    }
}