<%@ Page Title="Truck Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TruckReport.aspx.cs" Inherits="FMS.TruckReport" %>
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
					<div class="panel-title">Last Month</div>
					
					<div class="panel-options">
						<a href="#" data-rel="collapse"><i class="glyphicon glyphicon-refresh"></i></a>
						
					</div>
				</div>
  				<div class="panel-body">
  					<div class="row">
                        <div>
                            From: <input class="form-control" type="date" id="fromDate" runat="server"> <br />
                            To: <input class="form-control" type="date" id="toDate" runat="server"> <br />
                        </div> <asp:Button ID="view" runat="server" Text="View Kms Driven" OnClick="view_Click" />
  						<div id="chartContainer" style="height: 370px; width: 100%;"></div>
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
    <input type="hidden" id="truckData" runat="server" />
    <script>
        window.onload = function () {
            var input = document.getElementById('<%= truckData.ClientID %>').value.split("#");
            console.log(input);
            var data = [];
            for (var c in input) {
                var truck = input[c].split("*");
                console.log(truck);
                if(truck.length == 2) data.push({ y: parseFloat(truck[1]), label: truck[0] });
            }

            var chart = new CanvasJS.Chart("chartContainer", {
                animationEnabled: true,
                theme: "light2",
                title: {
                    text: "Truck Kilometers "
                },
                axisY: {
                    title: "Kilometers (KM)"
                },
                data: [{
                    type: "column",
                    showInLegend: true,
                    legendMarkerColor: "grey",
                    legendText: "Individual Trucks",
                    dataPoints: data
                }]
            });
            chart.render();

        }
    </script>
</asp:Content>
