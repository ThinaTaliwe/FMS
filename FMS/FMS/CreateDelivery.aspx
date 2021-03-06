<%@ Page Title="Create Delivery" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateDelivery.aspx.cs" Inherits="FMS.CreateDelivery" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section id="main-content">
            <section class="wrapper">
                <div class="content-box-large">
	  			<div class="row">
	  				<div class="col-md-12 panel-info">
			  			<div class="content-box-header panel-heading">
		  					<div class="panel-title ">Create Delivery</div>
			  			</div>
			  		</div>
	  			</div>

				<div class="row">
					<div class="col-md-12">
						<div class="content-box-large">
		  				<div class="panel-body">
                             
                              <form class="form-horizontal" role="form">
										  <div class="form-group">
										    <label  class="col-sm-2 control-label">Order Number</label>
										    <div class="col-sm-10">
										      <input class="form-control" id="OrderNum" placeholder="Enter Order Number" runat="server">
                                               <asp:RequiredFieldValidator id="validOrderNum" runat="server" style="color:Red;" controlToValidate="OrderNum" errorMessage="Enter order number" display="dynamic">
                                               </asp:RequiredFieldValidator>
										    </div>
										  </div>
                                  <div class="form-group">
										    <label  class="col-sm-2 control-label">Delivery Date</label>
										    <div class="col-sm-10">
										     	
                                              <input class="form-control" type="date" id="DeliveryDate" runat="server">
                                               <asp:RequiredFieldValidator id="validDeliveryDate" style="color:Red;" runat="server" controlToValidate="DeliveryDate" errorMessage="Enter date" display="dynamic">
                                               </asp:RequiredFieldValidator>
										    </div>
										  </div>
                                         <div class="form-group">
										    <label  class="col-sm-2 control-label">Truck</label>
										    <div class="col-sm-10">
										      <select class="form-control" id="TruckChosen" runat="server">
													<option>Select A Truck</option>
												</select> 
                                               <asp:RequiredFieldValidator id="validTruckChosen" style="color:Red;" runat="server" controlToValidate="TruckChosen" errorMessage="choose truck" display="dynamic">
                                               </asp:RequiredFieldValidator>
			
										    </div>
										  </div>
                                         <div class="form-group">
										    <label  class="col-sm-2 control-label">Client</label> 
										    <div class="col-sm-10">
										      <select class="form-control" id="Client" runat="server">
													<option>Select A Client</option>
												</select> 
                                               <asp:RequiredFieldValidator id="validClient" style="color:Red;" runat="server" controlToValidate="Client" errorMessage="Choose client" display="dynamic">
                                               </asp:RequiredFieldValidator>
			
										    </div>
										  </div>
                                         <div class="form-group">
										    <label  class="col-sm-2 control-label">Driver</label>
										    <div class="col-sm-10">
										      <select class="form-control" id="DriverChosen" runat="server">
													<option>Select A Driver</option>
												</select> 
                                               <asp:RequiredFieldValidator id="validDriver" style="color:Red;" runat="server" controlToValidate="DriverChosen" errorMessage="Choose driver" display="dynamic">
                                               </asp:RequiredFieldValidator>
			
										    </div>
										  </div>
                                   
                                   <div class="form-group">
										    <label class="col-sm-2 control-label">Material</label>
										    <div class="col-sm-10">
                                                 <select class="form-control" id="Material" runat="server">
													<option>Select Material</option>
												</select> 
										       <asp:RequiredFieldValidator id="validMaterial" style="color:Red;" runat="server" controlToValidate="Material" errorMessage="Enter material" display="dynamic">
                                               </asp:RequiredFieldValidator>
										    </div>
										  </div>
                                   <div class="form-group">
										    <label class="col-sm-2 control-label">Load (Ton)</label>
										    <div class="col-sm-10">
										      <input  class="form-control" id="Load" placeholder="Enter Load" runat="server">
                                                 <input  class="form-control" type="hidden" id="here" value="" runat="server">
                                                 <input  class="form-control" type="hidden" id="there" value="" runat="server">
                                               <asp:RequiredFieldValidator id="validLoad" style="color:Red;" runat="server" controlToValidate="Load" errorMessage="Enter Load" display="dynamic">
                                               </asp:RequiredFieldValidator>
										    </div>
										  </div>
                                  
<input id="origin-input" class="controls" type="text" placeholder="Origin">

<input id="destination-input" class="controls" type="text"
       placeholder="Destination">

<div id="mode-selector" class="controls" hidden="hidden">
    <input type="radio" name="type" id="changemode-walking" checked="checked">
    <label for="changemode-walking">Walking</label>

    <input type="radio" name="type" id="changemode-transit">
    <label for="changemode-transit">Transit</label>

    <input type="radio" name="type" id="changemode-driving">
    <label for="changemode-driving">Driving</label>
</div>

<div id="map"></div>
 <input type="hidden" runat="server" id="routeInfo" />

<script>
    var originInput;
    var destinationInput;
    var originIDs;
    var destIDs;
    var tester;
    // This example requires the Places library. Include the libraries=places
    // parameter when you first load the API. For example:
    // <script src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&libraries=places">
    function initMap() {
        var map = new google.maps.Map(document.getElementById('map'), {
            mapTypeControl: false,
            center: { lat: - 26.270760, lng: 28.112268 },
            zoom: 13
        });
        new AutocompleteDirectionsHandler(map);
    }
    /**
     * @constructor
    */
    function AutocompleteDirectionsHandler(map) {
        this.map = map;
        this.originPlaceId = null;
        this.destinationPlaceId = null;
        this.travelMode = 'WALKING';
        originInput = document.getElementById('origin-input');
        //document.getElementById('itemRun') = originInput;
        destinationInput = document.getElementById('destination-input');
        var modeSelector = document.getElementById('mode-selector');
        this.directionsService = new google.maps.DirectionsService;
        this.directionsDisplay = new google.maps.DirectionsRenderer;
        this.directionsDisplay.setMap(map);
        var originAutocomplete = new google.maps.places.Autocomplete(
            originInput, { placeIdOnly: true });
        var destinationAutocomplete = new google.maps.places.Autocomplete(
            destinationInput, { placeIdOnly: true });
        this.setupClickListener('changemode-walking', 'WALKING');
        this.setupClickListener('changemode-transit', 'TRANSIT');
        this.setupClickListener('changemode-driving', 'DRIVING');
        this.setupPlaceChangedListener(originAutocomplete, 'ORIG');
        this.setupPlaceChangedListener(destinationAutocomplete, 'DEST');
        this.map.controls[google.maps.ControlPosition.TOP_LEFT].push(originInput);
        this.map.controls[google.maps.ControlPosition.TOP_LEFT].push(destinationInput);
        this.map.controls[google.maps.ControlPosition.TOP_LEFT].push(modeSelector);
    }
    // Sets a listener on a radio button to change the filter type on Places
    // Autocomplete.
    AutocompleteDirectionsHandler.prototype.setupClickListener = function (id, mode) {
        var radioButton = document.getElementById(id);
        var me = this;
        radioButton.addEventListener('click', function () {
            me.travelMode = mode;
            me.route();
        });
    };
    AutocompleteDirectionsHandler.prototype.setupPlaceChangedListener = function (autocomplete, mode) {
        var me = this;
        //window.alert(place.placeId + place.placeId.value);
        autocomplete.bindTo('bounds', this.map);
        autocomplete.addListener('place_changed', function () {
            var place = autocomplete.getPlace();
            if (!place.place_id) {
                window.alert("Please select an option from the dropdown list.");
                return;
            }
            if (mode === 'ORIG') {
                me.originPlaceId = place.place_id;
                originIDs = place.name;
                //txtHidden.value = originIDs;
                //window.alert(place.coords);
            } else {
                me.destinationPlaceId = place.place_id;
                destIDs = place.name
                //window.alert(destIDs);
            }
            me.route();
        });
        // document.getElementById("run").innerHTML
    };
    AutocompleteDirectionsHandler.prototype.route = function () {
        if (!this.originPlaceId || !this.destinationPlaceId) {
            return;
        }
        var me = this;

        this.directionsService.route({
            origin: { 'placeId': this.originPlaceId },
            destination: { 'placeId': this.destinationPlaceId },
            travelMode: this.travelMode
        }, function (response, status) {
            if (status === 'OK') {
                me.directionsDisplay.setDirections(response);
                //document.getElementById("itemRun").innerText = originInput;
                //originIDs = originPlaceId;
                //window.alert(origin.lat());
                var route = response.routes[0].legs[0];
                var distance = route.distance.value;
                var from_coords = route.start_location.toString();
                var to_coords = route.end_location.toString();
                var from_address = route.start_address.toString();
                var to_address = route.end_address.toString();
                var json = {
                    "distance": parseFloat(distance) / 1000.0,
                    "from_coords" : from_coords,
                   "to_coords" : to_coords,
                    "from_address" : from_address,
                    "to_address" : to_address
                }
                var strRoute = JSON.stringify(json);
                document.getElementById('<%= routeInfo.ClientID %>').value = strRoute;
                console.log(strRoute, typeof(strRoute));
            } else {
                window.alert('Directions request failed due to ' + status);
            }
        });
    };
    function getOrigin() {
        document.getElementById('<%= here.ClientID %>').value = originIDs;
        document.getElementById('<%= there.ClientID %>').value = destIDs;
        //window.alert(document.getElementById('<%= here.ClientID %>').value); 
    }
</script>
<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBelHfLMXxL73XH_xMQ4p15uT-3GQztZYE&libraries=places&callback=initMap"
        async defer></script>


				  		<button class="btn btn-default" type="submit" onclick="CancelCreateDelivery()"> Cancel </button>
                                
                                  <asp:Button ID="btn" class="btn btn-primary" runat="server" Text="Submit" OnClientClick ="getOrigin();" OnClick="btn_Click"  />	 </form>
                            </div>
		  					

		  			</div>
					</div>
				</div>


	  		<!--  Page content -->
		  </div>


            </section>
        </section>
</asp:Content>