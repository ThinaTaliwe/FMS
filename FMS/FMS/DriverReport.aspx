<%@ Page Title="Driver Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DriverReport.aspx.cs" Inherits="FMS.DriverReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
    	<div class="row">
		  <div class="col-md-10">

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
  					<div class="row">
                        <div>
                            From: <input class="form-control" type="date" id="fromDate" runat="server"> 
                            To: <input class="form-control" type="date" id="toDate" runat="server"> 
                            Drivers <asp:DropDownList ID="driverList" runat="server"></asp:DropDownList>
                        </div><asp:Button ID="View" runat="server" Text="View Hours Driven" OnClick="ViewHours" />
                          <asp:UpdatePanel ID="report" runat="server" UpdateMode="Always">
                              <Triggers><asp:AsyncPostBackTrigger ControlID="View" /></Triggers>
                              <ContentTemplate>
        <asp:Label ID="reportText" runat="server" Text="" ></asp:Label>
                              </ContentTemplate>
                          </asp:UpdatePanel>
                        <input type="hidden" runat="server" id="chartData" />
                        <input type="hidden" runat="server" id="chart" />
  						<div id="chartContainer" style="height: 370px; width: 100%;"></div>
        <asp:Label ID="text" runat="server" Text="" ></asp:Label>
                     <script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>
  					</div>
  				</div>
  			</div>
                            </div>
		  					
		  			</div>
					</div>
				</div>


	  		<!--  Page content -->
		  </div>
		</div>
    </div>

    <script>

        function load_graph() {

            var lstDrivers = document.getElementById('<%= chartData.ClientID %>').value.split("#");
            console.log(lstDrivers);
            var bars = [];
            for (var c in lstDrivers) {
                var bar = lstDrivers[c].split("*");
                console.log(bar)
                if (bar.length == 2 && parseFloat(bar[1]) > 0) bars.push({ y: parseFloat(bar[1]), label: bar[0] });
            }
            var strChart = document.getElementById('<%= chart.ClientID %>').value;
            console.log(strChart);
            var json = JSON.parse(strChart);
            var chart = new CanvasJS.Chart("chartContainer", {
                animationEnabled: true,
                theme: "light2",
                title: {
                    text: json["title"]
                },
                axisY: {
                    title: json["y_axis_title"]
                },
                data: [{
                    type: "column",
                    showInLegend: true,
                    legendMarkerColor: "grey",
                    legendText: json["legend_text"],
                    dataPoints: bars
                }]
            });
            chart.render();
        }

        window.onload = load_graph()
    </script>
</asp:Content>
