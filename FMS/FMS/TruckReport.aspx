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
                    dataPoints: [
                        { y: 300878, label: "AA00BBGP" },
                        { y: 266455, label: "AA00BBGP" },
                        { y: 169709, label: "AA00BBGP" },
                        { y: 158400, label: "AA00BBGP" },
                        { y: 142503, label: "AA00BBGP" },
                        { y: 101500, label: "AA00BBGP" },
                        { y: 97800, label: "AA00BBGP" },
                        { y: 80000, label: "AA00BBGP" }
                    ]
                }]
            });
            chart.render();

        }
    </script>
</asp:Content>
