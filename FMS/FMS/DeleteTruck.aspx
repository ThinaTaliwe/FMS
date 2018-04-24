<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteTruck.aspx.cs" Inherits="FMS.DeleteTruck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
                        <div class="page-content">
    	<div class="row">
		  <div class="col-md-10">

	  			<div class="row">
	  				<div class="col-md-12 panel-info">
			  			<div class="content-box-header panel-heading">
		  					<div class="panel-title ">Delete Truck</div>
						
			  			</div>

			  		</div>
	  			</div>

				<div class="row">
					<div class="col-md-12">
						<div class="content-box-large">
		  				<div class="panel-body">
                              <form class="form-horizontal" role="form">
                                         <div class="form-group">
										    <label  class="col-sm-2 control-label">Truck</label>
										    <div class="col-sm-10">
										      <select class="form-control" id="TruckChosen" runat="server">
													<option>Select a Truck</option>
												</select> 
			
										    </div>
										  </div>
                                   
                                   <div class="form-group">
										    <label class="col-sm-2 control-label">Admin Password</label>
										    <div class="col-sm-10">
										      <input  class="form-control" id="DeleteTruckPassword" placeholder="">
										    </div> <br/>
										  </div>
                                 
				  							<button class="btn btn-default" type="submit" onclick="CancelDeleteTruck()">
													Cancel
												</button>
												<button class="btn btn-primary" type="submit" onclick="DeleteTruck()">
													<i class="fa fa-save" ></i>
													Delete Truck
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
