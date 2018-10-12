<%@ Page Title="Truck Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TruckReport.aspx.cs" Inherits="FMS.TruckReport" %>
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
                      
                            <table class="table" style="width: 95%; margin-right: 0px">
                                <tbody>
                                    <tr> 
                                        <td style="width: 155px">From: </td> <td style="width: 177px"> <input class="form-control" type="date" id="fromDate" runat="server"></td>
                                        <td style="width: 37px">To: </td> <td><input class="form-control" type="date" id="toDate" runat="server" style="width: 77%"> </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 155px; height: 46px">Driver: </td> <td style="width: 177px; height: 46px"> <asp:DropDownList ID="truckList" runat="server" CssClass="col-md-offset-0" Height="38px" Width="178px"></asp:DropDownList></td>
                                    </tr>
                              <tr>
                                  <td>
                                   <label id="reportText" runat="server" />
                                  </td>
                              </tr>
                                    <tr>
                                        <td style="width: 155px"><asp:Button ID="View" runat="server" Text="View Report" OnClick="view_Click" class="btn btn-default" /></td>
                                    </tr>
                                </tbody>
                            </table>
  						<div id="chartContainer" style="height: 370px; width: 100%;"></div>
                        <div id="truckInfo" runat="server" ></div>
                     <script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>
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
    <input type="hidden" id="chartData" runat="server" />
    <input type="hidden" id="chart" runat="server" />
    <script>

        function load_graph() {
            var info = document.getElementById('<%= chartData.ClientID %>').value;
            if (info == "none") {
                document.getElementById("chartContainer").innerHTML = "";
                document.getElementById("chartContainer").style.height = 0;
                return;
            }
            var input = info.split("#");
            console.log(input);
            var data = [];
            for (var c in input) {
                var truck = input[c].split("*");
                console.log(truck);
                if (truck.length == 2 && parseFloat(truck[1]) > 0) data.push({ y: parseFloat(truck[1]), label: truck[0] });
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
                    dataPoints: data
                }]
            });
            chart.render();
        }

        window.onload = load_graph();
    </script>
</asp:Content>
