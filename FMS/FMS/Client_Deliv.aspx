<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Client_Deliv.aspx.cs" Inherits="FMS.Client_Deliv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Era Commerce: Delivery </title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="Content/client/assets/css/main.css" />
</head>
<body>
<!-- Main -->
    <div id="main">

        <!-- Section -->
        <section class="wrapper style1">
            <div class="inner">
                <!-- 2 Columns -->
                <div class="flex flex-2">
                    <div class="col col2">
                        <div class="image round fit"><input type="hidden" id="locations" runat="server" />
                            <div id="map"></div>
                        </div>
                    </div>
                    <div class="col col1 first">
                        <div runat="server" id="tables">
                                    
                                </div>
                       
                        <form runat="server">

                            <asp:Button ID="btn" class="btn btn-primary" runat="server" Text="Confirm Delivery Delivered" OnClick="confirm_delivery"/>
                        </form>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBelHfLMXxL73XH_xMQ4p15uT-3GQztZYE&callback=initMap"
    async defer></script>
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
                    </div>
                </div>
            </div>
        </section>

        <!-- Section -->
        <section class="wrapper style2">
            <div class="inner">
                <div class="flex flex-2">
                    <div class="col col2">
                        <h3>Have any problems?</h3>
                        <p>Should you encouter any problems such as long ETAs or wrong information, please do not hesitate to contact us: </p>
                        <p>Email: <a style="color:black" href="mailto:info@newera.co.za"> info@newera.co.za</a></p>
                        <p>Telephone: 011 123 2343"</p>
                    </div>
                    <div class="col col1 first">
                        <div class="image round fit">
                            <a href="generic.html" class="link"><img src="content/client/images/pic02.jpg" alt="" /></a>
                        </div>
                    </div>
                </div>
            </div>
        </section>


    </div>


</body>>
</html>
