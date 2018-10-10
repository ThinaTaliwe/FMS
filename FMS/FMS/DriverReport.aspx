<%@ Page Title="Driver Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DriverReport.aspx.cs" Inherits="FMS.DriverReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <section id="main-content">
            <section class="wrapper">
                <div class="content-box-large">

	  			<div class="row">
	  				<div class="col-md-12 panel-info">
			  			<div class="content-box-header panel-heading">
		  					<div class="panel-title ">Truck Report </div>
						
			  			</div>

			  		</div>
	  			</div>

				<div class="row">
					<div class="col-md-12">
						<div class="content-box-large">
		  				<div class="panel-body">
                                			<div class="content-box-large">
  				<div class="panel-heading">
					
					<div class="panel-options">
						<a href="#" data-rel="collapse"><i class="glyphicon glyphicon-refresh"></i></a>
						
					</div>
				</div>
  				<div class="panel-body">
                        <div>
                            <table class="table" style="width: 95%; margin-right: 0px">
                                <tbody>
                                    <tr> 
                                        <td style="width: 155px">From: </td> <td style="width: 177px"> <input class="form-control" type="date" id="fromDate" runat="server"></td>
                                        <td style="width: 37px">To: </td> <td><input class="form-control" type="date" id="toDate" runat="server" style="width: 77%"> </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 155px; height: 46px">Driver: </td> <td style="width: 177px; height: 46px"> <asp:DropDownList ID="driverList" runat="server" CssClass="col-md-offset-0" Height="38px" Width="178px"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 155px"><asp:Button ID="View" runat="server" Text="View Hours Driven" OnClick="ViewHours" /></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div
        <asp:Label ID="reportText" runat="server" Text="" ></asp:Label>
                        <input type="hidden" runat="server" id="chartData" />
                        <input type="hidden" runat="server" id="chart" />
  						<div id="graph" style="height: 370px; width: 100%;"></div>
        <asp:Label ID="text" runat="server" Text="" ></asp:Label>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
  				</div>
  			</div>
                            </div>
		  			</div>
					</div>
				</div>


	  		<!--  Page content -->
		  </div>
           </section>
        </section>

    <script>

        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(load_graph);

        function load_graph() {

            var lstDrivers = document.getElementById('<%= chartData.ClientID %>').value.split("#");
            console.log(lstDrivers);
            var bars = [];
            for (var c in lstDrivers) {
                var bar = lstDrivers[c].split("*");
                console.log(bar)
                if (bar.length == 2 && parseFloat(bar[1]) > 0) bars.push([bar[0], parseFloat(bar[1])]);
            }
            var strChart = document.getElementById('<%= chart.ClientID %>').value;
            console.log(strChart);
            var json = JSON.parse(strChart);
            var data = new google.visualization.DataTable();
            data.addColumn('string', json["legend_text"]);
            data.addColumn('number', json["y_axis_title"]);
            data.addRows(bars);
            var options = {
                'title': json["title"],
                'width': 600,
                'height': 500
            }
            var chart = new google.visualization.BarChart(document.getElementById('graph'));
            chart.draw(data, options);
        }
    </script>
</asp:Content>
