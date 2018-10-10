<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeliveryReport.aspx.cs" Inherits="FMS.DeliveryReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section id="main-content">
            <section class="wrapper">
                <div class="content-box-large">

	  			<div class="row">
	  				<div class="col-md-12 panel-info">
			  			<div class="content-box-header panel-heading">
		  					<div class="panel-title ">Delivery Report</div>
						    
			  			</div>

			  		</div>
	  			</div>

				<div class="row">

					<div class="col-md-12">
						<div class="content-box-large">
		  				<div class="panel-body"><input type="hidden" id="locations" runat="server" />
                        <div id="map" style="width: 100%; height: 500px;"></div>
                        <script>
                            var map;
                            var redIcon = "http://maps.google.com/mapfiles/ms/micons/red.png";
                            var greenIcon = "http://maps.google.com/mapfiles/ms/micons/green.png";
                            var places = document.getElementById('<%= locations.ClientID %>').value;
                            var arrPlaces = places.split("#");
                            function initMap() {
                                var myLatLng = { lat: -26.02, lng: 28.56 };
                                map = new google.maps.Map(document.getElementById('map'), {
                                    zoom: 4,
                                    center: myLatLng
                                });
                                for (var loc in arrPlaces) {
                                    console.log(arrPlaces[loc]);
                                    var info = arrPlaces[loc].split("*");
                                    var speed = parseFloat(info[1])
                                    var icon = speed > 80 ? redIcon : greenIcon;
                                    var coords = info[3].split(":");
                                    var text = "Distance: " + info[0] + "\n";
                                    text += "Speed: " + info[1] + "\n";
                                    text += "Coordinates: " + info[3] + "\n";
                                    text += "Time: " + info[2];
                                    var marker = new google.maps.Marker({
                                        position: new google.maps.LatLng(parseFloat(coords[0]), parseFloat(coords[1])),
                                        icon: icon,
                                        title: text,
                                        map: map
                                    })
                                }
                            }
                        </script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBelHfLMXxL73XH_xMQ4p15uT-3GQztZYE&callback=initMap"
    async defer></script>
        <asp:Label ID="text" runat="server" Text="" ></asp:Label>
                            </div>
		  			</div>
					</div>
				</div>


	  		<!--  Page content -->
		  </div>
           </section>
        </section>
</asp:Content>
