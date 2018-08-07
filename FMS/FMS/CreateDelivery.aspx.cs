using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;
using GoogleMaps.LocationServices;

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
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            string tempIDquery = "SELECT ID FROM CLIENTS WHERE COMPANY LIKE '" + Client.Value + "'";
            var tempID = Util.query(tempIDquery);
            int IDnow = 0; 
            if (tempID.HasRows)
            {
                tempID.Read();
                IDnow = tempID.GetInt32(0);
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
            DateTime timeDate = DateTime.Parse(DeliveryDate.Value + " " + DeliveryTime.Value);

            //setting lat and long
            var Originaddress = here.Value; 
            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(Originaddress);
            var latitude = point.Latitude;
            var longitude = point.Longitude;
            String latlngOrigin = latitude + ":" + longitude + "#" + Originaddress;

            var Destaddress = there.Value;
            var Destpoint = locationService.GetLatLongFromAddress(Destaddress);
            var Destlatitude = Destpoint.Latitude;
            var Destlongitude = Destpoint.Longitude;
            String latlngDest = Destlatitude + ":" + Destlongitude + "#" + Destaddress;
            
            //string driver = DriverChosen.Value.Split(' ')[1];
            var query = "INSERT INTO DELIVERY(ORDER_NUM, TRUCK, DRIVER, CLIENT, [FROM], [TO], MATERIAL, [LOAD], DEPART_DAY, AUTHORITY) VALUES('" + OrderNum.Value + "', '" + TruckChosen.Value + "', '" + DriverID + "', '" + IDnow + "', '" + latlngOrigin + "', '" + latlngDest + "', '" + Material.Value + "', '" + Load.Value + "', '" + timeDate + "', '" + "1234567890123" + "');";
            Util.query(query);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}