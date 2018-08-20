<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddClient.aspx.cs" Inherits="FMS.AddClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
    	<div class="row">
		  <div class="col-md-10">

	  			<div class="row">
	  				<div class="col-md-12 panel-info">
			  			<div class="content-box-header panel-heading">
		  					<div class="panel-title ">Add Client</div>
						
			  			</div>

			  		</div>
	  			</div>

				<div class="row">
					<div class="col-md-12">
						<div class="content-box-large">
		  				<div class="panel-body">
                              <form class="form-horizontal" role="form">
										   <div class="form-group">
										    <label  class="col-sm-2 control-label">Contact Name </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="UserName" placeholder="Enter Contact Name" runat="server">
										    </div>
										  </div>
                                  	<div class="form-group">
										    <label  class="col-sm-2 control-label">Company Name </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="Surname" placeholder="Enter Company Name" runat="server">
										    </div>
										  </div>
                                        <div class="form-group">
										    <label  class="col-sm-2 control-label">Telephone </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="Email" placeholder="Enter User Email Address" runat="server">
										    </div>
										  </div>
                                   

                                  <div class="form-group">
												<label class="control-label col-md-2">Select User Type</label>
											<div class="col-md-10">
												<select class="form-control">
													<option>Admin</option>
													<option>Data Handler</option>
													<option>Supervisor</option>
												</select>
											</div>
										</div>
                                   
                               
                                  
				  							<button class="btn btn-default" type="submit" onclick="CancelAdduser()">
													Cancel
												</button>
												<button class="btn btn-primary" type="submit" onclick="Add_User()">
													<i class="fa fa-save" ></i>
													Add User
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
