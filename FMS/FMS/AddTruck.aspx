<<<<<<< HEAD
﻿<%@ Page Title="Add Truck" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddTruck.aspx.cs" Inherits="FMS.AddTruck" %>
=======
﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddTruck.aspx.cs" Inherits="FMS.AddTruck" %>

>>>>>>> 8f41cc4533a943c04dac64fc68fa0d11be2d0175
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section id="main-content">
            <section class="wrapper">
                <div class="content-box-large">

                    	  			<div class="row">
	  				<div class="col-md-12 panel-info">
			  			<div class="content-box-header panel-heading">
		  					<div class="panel-title ">Add Truck</div>
						
			  			</div>

			  		</div>
	  			</div>

				<div class="row">
					<div class="col-md-12">
						<div class="content-box-large">
		  				    <div class="panel-body">
                              <div class="form-horizontal" role="form">
										  <div class="form-group">
                                               <label  class="col-sm-2 control-label"> Number Plate </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="TruckPlate" placeholder="" runat="server">
										    </div>
										  </div>
                                          <div class="form-group">
                                               <label  class="col-sm-2 control-label"> Brand </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="TruckBrand" placeholder="" runat="server">
										    </div>
										  </div>
                                          <div class="form-group">
                                              <label  class="col-sm-2 control-label"> Maximum Load </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="TruckMaxLoad" placeholder="" runat="server">
										    </div>
										  </div>
                                          <div class="form-group">
                                               <label  class="col-sm-2 control-label"> Maximum Speed </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="TruckMaxSpeed" placeholder="" runat="server">
										    </div>
                                        </div>
                                          <div class="form-group">
                                               <label  class="col-sm-2 control-label"> Code </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="TruckCode" placeholder="" runat="server">
										    </div>
                                        </div>
									      <div class="form-group">
                                              <label  class="col-sm-2 control-label"> Color </label>
										    <div class="col-sm-10">
                                                <select class="form-control" id="Color" runat="server">
													<option>Select Color</option>
												</select> 
										    </div>
										  </div>
									<asp:Button ID="btnClear" class="btn btn-default" runat="server" Text="Cancle" OnClick="CancelAddTruck"/>		
                                  <asp:Button ID="btn" class="btn btn-primary" runat="server" Text="Submit" OnClick="Add_Truck"/>
                              </div>
                            </div>
		  					
		  			    </div>
					</div>
				</div>


	  		<!--  Page content -->
		  </div>
           </section>
        </section>
</asp:Content>
