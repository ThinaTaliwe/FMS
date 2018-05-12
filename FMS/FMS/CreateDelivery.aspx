<%@ Page Title="Create Delivery" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateDelivery.aspx.cs" Inherits="FMS.CreateDelivery" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <div class="page-content">
    	<div class="row">
		  <div class="col-md-10">

	  			<div class="row">
	  				<div class="col-md-12 panel-info">
			  			<div class="content-box-header panel-heading">
		  					<div class="panel-title ">Create Delivery</div>
                             <div class="panel-title" ><label id="Error" runat="server"></label></div>

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
										      <input class="form-control" id="OrderNum" placeholder="" runat="server">
                                               <asp:RequiredFieldValidator id="validOrderNum" runat="server" controlToValidate="OrderNum" errorMessage="Enter order number" display="dynamic">
                                               </asp:RequiredFieldValidator>
										    </div>
										  </div>
                                         <div class="form-group">
										    <label  class="col-sm-2 control-label">Truck</label>
										    <div class="col-sm-10">
										      <select class="form-control" id="TruckChosen" runat="server">
													<option>Select a truck</option>
												</select> 
                                               <asp:RequiredFieldValidator id="validTruckChosen" runat="server" controlToValidate="TruckChosen" errorMessage="choose truck" display="dynamic">
                                               </asp:RequiredFieldValidator>
			
										    </div>
										  </div>
                                         <div class="form-group">
										    <label  class="col-sm-2 control-label">Client</label>
										    <div class="col-sm-10">
										      <select class="form-control" id="Client" runat="server">
													<option>Select a Client</option>
												</select> 
                                               <asp:RequiredFieldValidator id="validClient" runat="server" controlToValidate="Client" errorMessage="Choose client" display="dynamic">
                                               </asp:RequiredFieldValidator>
			
										    </div>
										  </div>
                                         <div class="form-group">
										    <label  class="col-sm-2 control-label">Driver</label>
										    <div class="col-sm-10">
										      <select class="form-control" id="DriverChosen" runat="server">
													<option>Select a driver</option>
												</select> 
                                               <asp:RequiredFieldValidator id="validDriver" runat="server" controlToValidate="DriverChosen" errorMessage="Choose driver" display="dynamic">
                                               </asp:RequiredFieldValidator>
			
										    </div>
										  </div>
                                   <div class="form-group">
										    <label  class="col-sm-2 control-label">Delivery Date</label>
										    <div class="col-sm-10">
										     	
						                    <form action="/action_page.php">
                                              <input class="form-control" type="date" id="DeliveryDate" runat="server">
                                               <asp:RequiredFieldValidator id="validDeliveryDate" runat="server" controlToValidate="DeliveryDate" errorMessage="Enter date" display="dynamic">
                                               </asp:RequiredFieldValidator>
                                            </form>
										    </div>
										  </div>
                                   <div class="form-group">
										    <label class="col-sm-2 control-label">Material</label>
										    <div class="col-sm-10">
										      <input  class="form-control" id="Material" placeholder="" runat="server">
                                               <asp:RequiredFieldValidator id="validMaterial" runat="server" controlToValidate="Material" errorMessage="Enter material" display="dynamic">
                                               </asp:RequiredFieldValidator>
										    </div>
										  </div>
                                   <div class="form-group">
										    <label class="col-sm-2 control-label">Load</label>
										    <div class="col-sm-10">
										      <input  class="form-control" id="Load" placeholder="" runat="server">
                                               <asp:RequiredFieldValidator id="validLoad" runat="server" controlToValidate="Load" errorMessage="Enter Load" display="dynamic">
                                               </asp:RequiredFieldValidator>
										    </div>
										  </div>
                                <div class="form-group">
										    <label  class="col-sm-2 control-label">Origin</label>
										    <div class="col-sm-10">
										      <input  class="form-control" id="StartRoute" placeholder="" runat="server">
                                               <asp:RequiredFieldValidator id="validStartRoute" runat="server" controlToValidate="StartRoute" errorMessage="Enter origin" display="dynamic">
                                               </asp:RequiredFieldValidator>
										    </div>
										  </div>
                                   <div class="form-group">
										    <label class="col-sm-2 control-label">Destination</label>
										    <div class="col-sm-10">
										      <input  class="form-control" id="EndRoute" placeholder="" runat="server">
                                               <asp:RequiredFieldValidator id="validEndRoute" runat="server" controlToValidate="EndRoute" errorMessage="Enter destination" display="dynamic">
                                               </asp:RequiredFieldValidator>
										    </div>
										  </div>
				  							<button class="btn btn-default" type="submit" onclick="CancelCreateDelivery()">
													Cancel
												</button>		<asp:Button ID="btn" class="btn btn-primary" runat="server" Text="Submit" OnClick="btn_Click" />	 
										    	 
										  
										</form>
                            </div>
		  					
		  			</div>
					</div>
				</div>


	  		<!--  Page content -->
		  </div>
		</div>
    </div>
</asp:Content>
