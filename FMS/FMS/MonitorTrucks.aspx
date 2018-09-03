<%@ Page Title="Monitor Trucks" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonitorTrucks.aspx.cs" Inherits="FMS.MonitorTrucks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <head>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <script src="http://maps.google.com/maps/api/js?sensor=false"
            type="text/javascript"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBelHfLMXxL73XH_xMQ4p15uT-3GQztZYE" type="text/javascript"></script>
</head>
<body>
    <div id="map" style="width: 100%; height: 500px;"></div>
    <input type="hidden" id="trucks" runat="server" />
    <script type="text/javascript">
       /* var locations = [
            ['Truck 2', -26.1890, 28.0040, 3, "http://maps.google.com/mapfiles/ms/micons/blue.png"],
            ['Truck 3', -26.195246, 28.034088, 2, "http://maps.google.com/mapfiles/ms/micons/green.png"],
            ['Truck 1', -26.107567, 28.056702, 1, "http://maps.google.com/mapfiles/ms/micons/yellow.png"],
        ]; */

        var map = new google.maps.Map(document.getElementById('map'), {
            zoom: 10,
            center: new google.maps.LatLng(-26.1890, 28.0040),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });
        
        var infowindow = new google.maps.InfoWindow();

        var marker, i;

        var trucks = document.getElementById('<%= trucks.ClientID %>').value;
        var locations = trucks.split(" ");
        var icon = "http://maps.google.com/mapfiles/ms/micons/red.png";
        console.log(trucks);

        for (place in locations) {
            var mark = locations[place].split("*");
            var text = "Driver: " + mark[0] + "\n" + "Truck: " + mark[1] + "\n" + "Time: " + mark[3];
            console.log(mark)
            var coords = mark[2].split(":");
            console.log(coords)
            marker = new google.maps.Marker({
                position: new google.maps.LatLng(parseFloat(coords[0]), parseFloat(coords[1])),
                icon: icon,
                title: text,
                map: map
            });

            google.maps.event.addListener(marker, 'click', (function (marker, i) {
                return function () {
                    infowindow.setContent(locations[i][0]);
                    infowindow.open(map, marker);
                }
            })(marker, i));
        }
    </script>
</body>
</asp:Content>
