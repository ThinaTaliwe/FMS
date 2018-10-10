using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;
using GoogleMaps.LocationServices;
using FMS.App_Code;
using Newtonsoft.Json.Linq;
using System.Net.Mail;

namespace FMS
{
    public partial class CreateDelivery : System.Web.UI.Page
    {
        private Delivery delivery = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string driversQuery = "SELECT NAME, SURNAME, ID FROM USERS WHERE USER_TYPE LIKE 'DRIVER'";
                string truckQuery = "SELECT ID FROM TRUCKS";
                string clientQuery = "SELECT COMPANY, ID FROM CLIENTS ";
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
                var clients = Util.query(clientQuery);
                if (clients.HasRows)
                {
                    while (clients.Read()) 
                    {
                        Client.Items.Add(new ListItem(clients.GetString(0)));
                    }
                }
                Client.Items.Add(new ListItem("Other"));
                Material.Items.Add(new ListItem("Coal"));
                Material.Items.Add(new ListItem("Pozzsand"));
                Material.Items.Add(new ListItem("Clinkers"));
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            string tempIDquery = "SELECT ID, email FROM CLIENTS WHERE COMPANY LIKE '" + Client.Value + "'";
            var tempID = Util.query(tempIDquery);
            var clientEmail = "";
            int IDnow = 0; 
            if (tempID.HasRows)
            {
                tempID.Read();
                IDnow = tempID.GetInt32(0);
                clientEmail = tempID.GetString(1);
            }
            string DriverTempQuery = "SELECT ID FROM USERS WHERE NAME LIKE '" + DriverChosen.Value.Split(' ')[0]  + "' AND SURNAME LIKE '" + DriverChosen.Value.Split(' ')[1] + "'";
            var DriverTemp = Util.query(DriverTempQuery);
            String DriverID = "";
            if (DriverTemp.HasRows)
            {
                DriverTemp.Read();
                DriverID = DriverTemp.GetString(0);
                //reading the drivers table
            }
            //Time & Date Format 
            DateTime timeDate = DateTime.Parse(DeliveryDate.Value);

            //setting lat and long
            var strJson = routeInfo.Value;
            Util.print(strJson);
            var json = JObject.Parse(strJson);
            String latlngOrigin = parseCoords(json["from_coords"].ToString()) + "#" + json["from_address"];
            String latlngDest = parseCoords(json["to_coords"].ToString()) + "#" + json["to_address"];
            var authority = Session["user"] as string;
            Delivery delivery = new Delivery();
            delivery.setOrderNum(OrderNum.Value);
            delivery.setTruck(TruckChosen.Value);
            delivery.setDriver(DriverID);
            delivery.setClient(IDnow);
            delivery.setFrom(latlngOrigin);
            delivery.setTo(latlngDest);
            delivery.setMaterial(Material.Value);
            delivery.setLoad(Convert.ToInt32(Load.Value));
            delivery.setDepartDay(timeDate);
            delivery.setAuthority(authority);
            delivery.setDistance(Convert.ToInt32(json["distance"]));
            //var query = "INSERT INTO DELIVERY(ORDER_NUM, TRUCK, DRIVER, CLIENT, [FROM], [TO], MATERIAL, [LOAD], DEPART_DAY, AUTHORITY, distance) VALUES('" + OrderNum.Value + "', '" + TruckChosen.Value + "', '" + DriverID + "', '" + IDnow + "', '" + latlngOrigin + "', '" + latlngDest + "', '" + Material.Value + "', '" + Load.Value + "', '" + timeDate + "', '" + authority + "', " + json["distance"] + ");";
            //Util.query(query);
            delivery.save();

            //send mail to client
            sendEmail(clientEmail); 
        }

        private string parseCoords(string coords)
        {
            return coords.Replace('(', ' ').Replace(')', ' ').Trim().Replace(',', ':');
        }

        private void sendEmail(string email)
        {
          
        }
    }
}