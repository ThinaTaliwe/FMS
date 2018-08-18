using FMS.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FMS
{
	public partial class AddDriver : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void Add_Driver(object sender, EventArgs e)
        {
            String User = "Driver";
            String Salt = "0";
            int Message = 0; 
            var query = "INSERT INTO USERS(ID, NAME, SURNAME, PASSWORD, EMAIL, USER_TYPE, SALT) VALUES('" + DriverID.Value + "', '" + DriverName.Value + "', '" + DriverSurname.Value + "', '" + DriverName.Value + DriverSurname.Value + "', '" + DriverEmail.Value + "', '" + User + "', '" + Salt + "');";
            Util.query(query);
            var query2 = "INSERT INTO DRIVERS(ID, CODE, FIRST_ISSUE, EXPIRY, RESTRICTION, MESSAGE) VALUES('" + DriverID.Value + "', '" + DriverCode.Value + "', '" + LicenceIssueDate.Value + "', '" + LicenceExpiryDate.Value + "', '" + Restriction.Value + "', '" + Message + "');";
            Util.query(query2);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}