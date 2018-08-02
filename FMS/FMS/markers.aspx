<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="markers.aspx.cs" Inherits="FMS.markers" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
  <head>
    <title>Simple Map</title>
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
    <div id="map"></div>
    <form runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"/>
    <script>
        function something() {
            var xhttp = new XMLHttpRequest();
            xhttp.onreadystatechange = function () {
                if (this.readyState = 4 && this.status = 200) {
                    document.getElementById("hid") = this.responseText;
                    console.log("it mus have worked");
                }
            }
            xhttp.open("GET", "markers.aspx/foo", true);
            xhttp.send();
        }
    </script>
    <script>
        var map;
        var marker;

        function initMap() {
            var myLatLng = { lat: -26.02, lng: 28.56 };
            console.log("initMap()");
            map = new google.maps.Map(document.getElementById('map'), {
                zoom: 4,
                center: myLatLng
            });

            marker = new google.maps.Marker({
                position: myLatLng,
                map: map,
                draggable: true,
                title: 'Hello World!' <% =foo() %>
            });

            google.maps.event.addListener(marker, 'dragend', function (event) {
                console.log(event.latLng.lat() + ":" + event.latLng.lng());
                var pos = event.latLng.lat() + ":" + event.latLng.lng();
                document.getElementById("position").innerHTML = pos;
                something();
            });

        } 
        

    </script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBelHfLMXxL73XH_xMQ4p15uT-3GQztZYE&callback=initMap"
    async defer></script>   
    <label id="position" runat="server" />
    <label id="hid" runat="server" />
    <button id="hidden" style="display: none" onclick="something()" />
      </form>
  </body>
</html>
