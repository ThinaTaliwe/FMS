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
                        </div> <asp:Button ID="view" runat="server" Text="View Hours Driven" OnClick="view_Click" /><asp:Button ID="Button1" runat="server" Text="View Kms Driven" OnClick="view_Click" />
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

    <script>
        window.onload = function () {

            var chart = new CanvasJS.Chart("chartContainer", {
                animationEnabled: true,
                theme: "light2",
                title: {
                    text: "Driver Trips "
                },
                axisY: {
                    title: "Kilometers (KM)"
                },
                data: [{
                    type: "column",
                    showInLegend: true,
                    legendMarkerColor: "grey",
                    legendText: "Individual Trucks",
                    dataPoints: [
                        { y: 20, label: "Thina" },
                        { y: 50, label: "Khanyi" },
                        { y: 20, label: "Mmeli" },
                        { y: 30, label: "Carl" },
                    ]
                }]
            });
            chart.render();

        }
    </script>
</asp:Content>
