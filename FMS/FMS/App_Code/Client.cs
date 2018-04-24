using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
    public class Client
    {
        private int id;
        private string name;
        private string company;

        public Client(string company)
        {
            var query = "SELECT NAME, COMPANY, ID FROM CLIENTS WHERE COMPANY LIKE '" + company + "';";
            var client = Util.query(query);
            if(client.Read())
            {
                name = Convert.ToString(client.GetString(0));
                company = Convert.ToString(client.GetString(1));
                id = Convert.ToInt32(client.GetInt32(2));
            }
        }

        public string getName() { return name; }
        public string getCompany() { return company; }

        public int getID()
        {
            return id;
        }
    }
}