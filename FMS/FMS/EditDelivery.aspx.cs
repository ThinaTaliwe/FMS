using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;

namespace FMS
{
    public partial class EditDelivery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string driversQuery = "SELECT NAME, ID FROM USERS WHERE USER_TYPE LIKE 'DRIVER'";
                string truckQuery = "SELECT ID FROM TRUCKS";
                var drivers = Util.query(driversQuery);
                if (drivers.HasRows)
                {
                    while (drivers.Read())
                    {
                        DriverChosen.Items.Add(new ListItem(drivers.GetString(0) + " " + drivers.GetString(1)));
                    }
                }
                var trucks = Util.query(truckQuery);
                if (trucks.HasRows)
                {
                    while (trucks.Read())
                    {
                        TruckChosen.Items.Add(new ListItem(trucks.GetString(0)));
                    }
                }
            }

            try {
                string text ="id=" + Page.RouteData.Values["id"].ToString();
                Response.Write(text);
            } catch(Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            string id = Request.QueryString["order"];
            //ordernum.Text = id;
            //Display Info regarding the asked order 
            var query = "SELECT id, client, depart_day, material, load FROM DELIVERY WHERE ID LIKE '" + id + "'";
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

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["order"]; 
            var query = "UPDATE DELIVERY SET TRUCK = '" + TruckChosen.Value + "', DRIVER = '" + DriverChosen.Value.Split(' ')[1] + "' WHERE ID LIKE " + id;
            System.Diagnostics.Debug.WriteLine(query);
            Util.query(query);
            //Response.Redirect("EditDelivery"); 
        }
    }
} 