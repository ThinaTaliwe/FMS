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
        private string telephone;
        private string email;
        private string location;

        public Client(int id)
        {
            var query = "SELECT NAME, COMPANY, ID, telephone, email, location FROM CLIENTS WHERE ID LIKE '" + id + "';";
            var client = Util.query(query);
            if (client.Read())
            {
                name = Convert.ToString(client.GetString(0));
                company = Convert.ToString(client.GetString(1));
                id = Convert.ToInt32(client.GetInt32(2));
                telephone = client.GetString(3);
                email = client.GetString(4);
                location = client.GetString(5);
            }
        }

        public Client(string company)
        {
            var query = "SELECT NAME, COMPANY, ID, telephone, email, location FROM CLIENTS WHERE COMPANY LIKE '" + company + "';";
            var client = Util.query(query);
            if (client.Read())
            {
                name = Convert.ToString(client.GetString(0));
                company = Convert.ToString(client.GetString(1));
                id = Convert.ToInt32(client.GetInt32(2));
                telephone = client.GetString(3);
                email = client.GetString(4);
                location = client.GetString(5);
            }
        }

		public List<Delivery> getDeliveries(DateTime from, DateTime to) {
            var query = "select id from delivery where client like " + id + " and started > '" + from + "' and started < '" + to + "'";
			var reader = Util.query(query);
			if(reader.HasRows) {
                List<int> list = new List<int>();
				while(reader.Read())
					list.Add(reader.GetInt32(0));
				var lstDelivs = new List<Delivery>();
				foreach(int i in list) 
					lstDelivs.Add(Delivery.getInstance(i));
				return lstDelivs;
			}
			return null;
		}

        public string getName() { return name; }
        public string getCompany() { return company; }
        public string getTelephone() { return telephone; }
        public string getEmail() { return email; }
        public string getLocation() { return location; }

        public int getID()
        {
            return id;
        }
    }
}
