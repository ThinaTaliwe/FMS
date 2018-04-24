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
            
            var query = "select * from users where name like 'MMELI'";
            var rows = Util.query(query);
           
            if(rows.HasRows)
            {
                while(rows.Read())
                {
                    place.InnerText = Convert.ToString(rows.GetString(1));
                    
                }

            }
        }
    }
}