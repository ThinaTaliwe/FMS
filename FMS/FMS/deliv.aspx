<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deliv.aspx.cs" Inherits="FMS.deliv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="initial-scale=1.0">
    <meta charset="utf-8">
    <style>
      /* Always set the map height explicitly to define the size of the div
       * element that contains the map. */
      #map {
        height: 100%;
      }
      /* Optional: Makes the sample page fill the window. */
      html, body {
        height: 80%;
        margin: 0;
        padding: 0;
      }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="delivery" runat="server"></asp:DropDownList>
            <asp:DropDownList ID="driver" runat="server"></asp:DropDownList>
            <asp:DropDownList ID="truck" runat="server"></asp:DropDownList>
            <asp:Button ID="button" runat="server" OnClick="getDeliv" Text="View" />
        </div>
    </form>
    <input type="hidden" id="locations" runat="server" />
    <div id="map"></div>
    <script>
        var map;
        var redIcon = "http://maps.google.com/mapfiles/ms/micons/red.png";
        var greenIcon = "http://maps.google.com/mapfiles/ms/micons/green.png";
        var places = document.getElementById("locations").value;
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

                //var place = arrPlaces[loc];
                //var mark = place.split(":");
                //console.log(mark);
                //var marker = new google.maps.Marker({
                //    position: new google.maps.LatLng(parseFloat(mark[0]), parseFloat(mark[1])),
                //    icon: icon,
                //    title: arrPlaces[loc],
                //    map: map
                //});
            }
        }
        

    </script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBelHfLMXxL73XH_xMQ4p15uT-3GQztZYE&callback=initMap"
    async defer></script> <form>
        <asp:Label ID="text" runat="server" Text="" ></asp:Label></form>
</body>
</html>
