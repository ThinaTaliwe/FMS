<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteUser.aspx.cs" Inherits="FMS.DeleteUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
    	<div class="row">
		  <div class="col-md-10">

	  			<div class="row">
	  				<div class="col-md-12 panel-info">
			  			<div class="content-box-header panel-heading">
		  					<div class="panel-title ">Delete User</div>
						
			  			</div>

			  		</div>
	  			</div>

				<div class="row">
					<div class="col-md-12">
						<div class="content-box-large">
		  				<div class="panel-body">
                              <form class="form-horizontal" role="form">
                                        <div class="form-group">
												<label class="control-label col-md-2">Select User</label>
											<div class="col-md-10">
												<select class="form-control">
													<option>Khonzokuhle Nkosi</option>
													<option>Marley Mavuso</option>
													<option>Thina Taliwe</option>
													
												</select>
											</div>
										</div>
                                   
                                   <div class="form-group">
										    <label class="col-sm-2 control-label">Admin Password</label>
										    <div class="col-sm-10">
										      <input  class="form-control" id="DeleteUserPassword" placeholder="">
										    </div> <br/>
										  </div>
                                 
				  							<button class="btn btn-default" type="submit" onclick="CancelDeleteDriver()">
													Cancel
												</button>
												<button class="btn btn-primary" type="submit" onclick="DeleteUser()">
													<i class="fa fa-save" ></i>
													Delete User
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
