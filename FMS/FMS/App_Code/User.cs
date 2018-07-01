using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
    public class User
    {
        private string id;
        private string name;
        private string surname;
        private string email;
        private string type;

        public User (string id)
        {
            var query = "SELECT * FROM USERS WHERE ID LIKE '" + id + "';";
            var user = Util.query(query);
            if(user != null && user.HasRows)
            {
                user.Read();
                id = user.GetString(0);
                name = user.GetString(1);
                surname = user.GetString(2);
                email = user.GetString(3);
                type = user.GetString(4);
            }
        }

        public void setEmail(string value) { email = value; }
        public void setType(string value) { type = value; }

        public string getID() { return id; }
        public string getName()  { return name;  }
        public string getSurname() { return surname; }
        public string getEmail() { return email; }
        public string getType() { return type; }
    }
    
}