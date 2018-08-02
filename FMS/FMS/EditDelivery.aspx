<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditDelivery.aspx.cs" Inherits="FMS.EditDelivery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="ordernum" runat="server"> </asp:Label>
      <div class="page-content">
    	<div class="row">
		  <div class="col-md-10">

	  			<div class="row">
	  				<div class="col-md-12 panel-info">
			  			<div class="content-box-header panel-heading">
		  					<div class="panel-title ">Update Delivery</div>
                             <div class="panel-title" ><label id="Error" runat="server"></label></div>
                             <asp:Label ID="label" runat="server"> </asp:Label>
                             
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
                                               
										      <input class="form-control" id="Text1" value="" readonly placeholder="" runat="server">
                                               
										    </div>
										  </div>
                                         <div class="form-group">
										    <label  class="col-sm-2 control-label">Truck</label>
										    <div class="col-sm-10">
										      <select class="form-control" id="TruckChosen" runat="server">
													<option>Select A Truck</option>
												</select> 
                                               <asp:RequiredFieldValidator id="validTruckChosen" runat="server" controlToValidate="TruckChosen" errorMessage="choose truck" display="dynamic">
                                               </asp:RequiredFieldValidator>
			
										    </div>
										  </div>
                                         <div class="form-group">
										    <label  class="col-sm-2 control-label">Client</label>
										    <div class="col-sm-10">
                                                <input class="form-control" id="ClientSelected" value="" readonly placeholder="" runat="server">
										    </div>
										  </div>
                                         <div class="form-group">
										    <label  class="col-sm-2 control-label">Driver</label>
										    <div class="col-sm-10">
										      <select class="form-control" id="DriverChosen" runat="server">
													<option>Select A Driver</option>
												</select> 
                                               <asp:RequiredFieldValidator id="validDriver" runat="server" controlToValidate="DriverChosen" errorMessage="Choose driver" display="dynamic">
                                               </asp:RequiredFieldValidator>
			
										    </div>
										  </div>
                                   <div class="form-group">
										    <label  class="col-sm-2 control-label">Delivery Date</label>
										    <div class="col-sm-10">
										     	<input class="form-control" id="DeliveryDateSelected" value="" readonly placeholder="" runat="server">
										    </div>
										  </div>
                                   <div class="form-group">
										    <label class="col-sm-2 control-label">Material</label>
										    <div class="col-sm-10">

										      <input  class="form-control" id="MaterialSelected" value ="" placeholder="" readonly runat="server">
										    </div>
										  </div>
                                   <div class="form-group">
										    <label class="col-sm-2 control-label">Load</label>
										    <div class="col-sm-10">
										      <input  class="form-control" id="LoadSelected" value ="" placeholder="" readonly runat="server">
                                               
										    </div>
										  </div>
                                <div class="form-group">
										    <label  class="col-sm-2 control-label">Origin</label>
										    <div class="col-sm-10">
										      <input  class="form-control" value="" id="StartRoute" placeholder="" readonly runat="server">
                                               
										    </div>
										  </div>
                                   <div class="form-group">
										    <label class="col-sm-2 control-label">Destination</label>
										    <div class="col-sm-10">
										      <input  class="form-control" value="" id="EndRoute" placeholder="" readonly runat="server">
                                               
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
