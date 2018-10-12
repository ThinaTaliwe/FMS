using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;

namespace FMS
{
    public partial class ViewDrivers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = "select name, surname, email, ID from Users Where USER_TYPE like 'DRIVER'";
            var rows = Util.query(query);
            //var client = Util.getClient() 
            var HTMLStr = "";
            var drivers = Driver.getDriverList();
            foreach (var driver in drivers)
            {
                HTMLStr += "<tr> <td> <a href='DriverInfo?id=" + driver.getID() + "'>" + driver.getName() + "</a> </td> <td> " + driver.getSurname() + "</td> <td> " + "000 000 0000" + "</td> <td> " + driver.getEmail() + "</td> <td> " + driver.getCode() + "</td> <td> " + driver.getExpiry() + "</td> <td> " + driver.getRetriction() + "</td> </tr>";
            }
            tables.InnerHtml = HTMLStr;
        }
    }
}