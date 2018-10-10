<%@ Page Title="Add Client" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddClient.aspx.cs" Inherits="FMS.AddClient" %>
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
                                               <label  class="col-sm-2 control-label"> Name </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="ClientName" placeholder="" runat="server">
										    </div>
										  </div>
                                         <div class="form-group">
                                              <label  class="col-sm-2 control-label"> Company Name </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="ClientCompany" placeholder="" runat="server">
										    </div>
										  </div>
                                   <div class="form-group">
                                               <label  class="col-sm-2 control-label"> Tel Number </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="ClientTel" placeholder="" runat="server">
										    </div>
										  </div>
                                          <div class="form-group">
                                               <label  class="col-sm-2 control-label"> Email </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="ClientEmail" placeholder="" runat="server">
										    </div>
										  </div>
                                   <div class="form-group">
                                        <label  class="col-sm-2 control-label"> Company Address </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="ClientAddress" placeholder="" runat="server">
										    </div>
										  </div>                                  
                               
                                 
				  							<button class="btn btn-default" type="submit" onclick="CancelAddClient()">
													Cancel
												</button>
												  <asp:Button ID="btn" class="btn btn-primary" runat="server" Text="Submit"  OnClick="Add_Client"  />	 </form>
                        	 
										  
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
