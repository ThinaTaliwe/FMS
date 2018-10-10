<%@ Page Title="Add Truck" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddTruck.aspx.cs" Inherits="FMS.AddTruck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div class="page-content">
    	<div class="row">
		  <div class="col-md-10">

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
                              <form class="form-horizontal" role="form">
										  <div class="form-group">
                                               <label  class="col-sm-2 control-label"> Number Plate </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="TruckPlate" placeholder="" runat="server">
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
									 <div class="form-group">
                                              <label  class="col-sm-2 control-label"> Color </label>
										    <div class="col-sm-10">
                                                <select class="form-control" id="Color" runat="server">
													<option>Select Color</option>
												</select> 
										    </div>
										  </div>
                                   
                               
                                  
				  							<button class="btn btn-default" type="submit" onclick="CancelAddTruck()">
													Cancel
												</button>
											<asp:Button ID="btn" class="btn btn-primary" runat="server" Text="Submit" OnClick="Add_Truck"  />	 </form>
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
