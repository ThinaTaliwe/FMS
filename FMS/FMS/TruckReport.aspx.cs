﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;
using Newtonsoft.Json.Linq;

namespace FMS
{
    public partial class TruckReport : System.Web.UI.Page
    {
        private List<Truck> trucks;
        private  DateTime from, to;

        protected void Page_Load(object sender, EventArgs e)
        {
            string query;
            if(!IsPostBack)
            {
                trucks = Truck.getTruckList();
                query = "select id from trucks";
                var reader = Util.query(query);
                if(reader.HasRows)
                {
                    while (reader.Read())
                        truckList.Items.Add(new ListItem(reader.GetString(0)));
                }
            }
            viewTruckRepot();
        }

        public string[] truckReport(Truck theTruck)
        {
            try {
                if (String.IsNullOrWhiteSpace(fromDate.Value)|| String.IsNullOrWhiteSpace(toDate.Value))
                {
                    to = DateTime.Now;
                    from = to.AddMonths(-1);
                } else {
                    Util.print(fromDate.Value + toDate.Value);
                    from = DateTime.Parse(fromDate.Value);
                    to = DateTime.Parse(toDate.Value);
                }
                return new string[] { theTruck.getID(), Convert.ToString(theTruck.totalDistance(from, to)) };
            } catch(Exception ex) {
                Util.print(ex.ToString());
            } return null;
        }

        protected void view_Click(object sender, EventArgs e)
        {
            viewTruckRepot();
        }

        private void setGraph(string title, string yAxisTitle, string legendText, List<string[]> values)
        {
            JObject json = new JObject();
            json["title"] = title;
            json["y_axis_title"] = yAxisTitle;
            json["legend_text"] = legendText;
            chart.Value = json.ToString();
            string data = "";
            foreach (var point in values)
                data += point[0] + "*" + point[1] + "#";
            chartData.Value = data;
        }

        private void setSelectedText()
        {
            var text = "";
            text += "Report Period" + "<br/>";
            text += "From: " + from.Date.ToString() + "<br/>";
            text += "To: " + to.Date.ToString() + "<br/>";
            reportText.InnerText = text;
        }

        private void viewTruckRepot()
        {
            List<string[]> values = new List<string[]>();
            foreach (var tr in trucks)
            {
                var report = truckReport(tr);
                if (report != null)
                    values.Add(report);
            }
            setGraph("Truck Report", "Trucks", "Individual trucks", values);
        }
    }
}