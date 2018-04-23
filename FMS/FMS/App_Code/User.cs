using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
    public class User
    {
        private string id { get; }
        private string name { get; }
        private string surname { get; }
        private string email { get; set; }
        private string type { get; set; }

        public User(string id)
        {
            var query = "SELECT * FROM USERS WHERE ID LIKE '" + id + "';";
            var user = Util.query(query);
            if(user.Read())
            {
                id = user.GetString(0);
                name = user.GetString(1);
                surname = user.GetString(2);
                email = user.GetString(3);
                type = user.GetString(4);
            }
        }
    }
    
}