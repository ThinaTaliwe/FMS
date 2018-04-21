<%@ Page Title="Monitor Trucks" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonitorTrucks.aspx.cs" Inherits="FMS.MonitorTrucks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <head>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <script src="http://maps.google.com/maps/api/js?sensor=false"
            type="text/javascript"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBelHfLMXxL73XH_xMQ4p15uT-3GQztZYE&callback=myMap" type="text/javascript"></script>
</head>
<body>
    <div id="map" style="width: 100%; height: 500px;"></div>

    <script type="text/javascript">
        var locations = [
            ['Truck 2', -26.1890, 28.0040, 3, "http://maps.google.com/mapfiles/ms/micons/blue.png"],
            ['Truck 3', -26.195246, 28.034088, 2, "http://maps.google.com/mapfiles/ms/micons/green.png"],
            ['Truck 1', -26.107567, 28.056702, 1, "http://maps.google.com/mapfiles/ms/micons/yellow.png"],
        ];

        var map = new google.maps.Map(document.getElementById('map'), {
            zoom: 10,
            center: new google.maps.LatLng(-26.1890, 28.0040),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });

        var infowindow = new google.maps.InfoWindow();

        var marker, i;

        for (i = 0; i < locations.length; i++) {
            marker = new google.maps.Marker({
                position: new google.maps.LatLng(locations[i][1], locations[i][2]),
                icon: locations[i][4],
                title: locations[i][0],
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
