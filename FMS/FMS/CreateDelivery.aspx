<%@ Page Title="Create Delivery" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateDelivery.aspx.cs" Inherits="FMS.CreateDelivery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <div class="page-content">
    	<div class="row">
		  <div class="col-md-10">

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
										      <input class="form-control" id="OrderNum" placeholder="">
										    </div>
										  </div>
                                         <div class="form-group">
										    <label  class="col-sm-2 control-label">Truck</label>
										    <div class="col-sm-10">
										      <select class="form-control" id="TruckChosen">
													<option>RMB1454GP</option>
													<option>FD23FDGP</option>
													<option>POD453GP</option>
													<option>FED243GP</option>
													<option>GOD454GP</option>
												</select> 
			
										    </div>
										  </div>
                                         <div class="form-group">
										    <label  class="col-sm-2 control-label">Driver</label>
										    <div class="col-sm-10">
										      <select class="form-control" id="DriverChosen">
													<option>Marley Taliwe</option>
													<option>Thina Mavuso</option>
													<option>Maitso Mkwanazi</option>
													<option>Bill Bobslay</option>
												</select> 
			
										    </div>
										  </div>
                                   <div class="form-group">
										    <label  class="col-sm-2 control-label">Delivery Date</label>
										    <div class="col-sm-10">
										      <input  class="form-control" id="DeliveryDate" placeholder="YYYY/MM/DD">
										    </div>
										  </div>
                                   <div class="form-group">
										    <label class="col-sm-2 control-label">Delivery Time</label>
										    <div class="col-sm-10">
										      <input  class="form-control" id="DeliveryTime" placeholder="00H00">
										    </div>
										  </div>
                                <div class="form-group">
										    <label  class="col-sm-2 control-label">Origin</label>
										    <div class="col-sm-10">
										      <input  class="form-control" id="StartRoute" placeholder="">
										    </div>
										  </div>
                                   <div class="form-group">
										    <label class="col-sm-2 control-label">Destination</label>
										    <div class="col-sm-10">
										      <input  class="form-control" id="EndRoute" placeholder="">
										    </div>
										  </div>
				  							<button class="btn btn-default" type="submit" onclick="CancelCreateDelivery()">
													Cancel
												</button>
												<button class="btn btn-primary" type="submit" onclick="CreateDelivery()">
													<i class="fa fa-save" ></i>
													Submit
												</button>			 
										  
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
