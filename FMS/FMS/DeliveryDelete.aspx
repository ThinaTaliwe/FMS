<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeliveryDelete.aspx.cs" Inherits="FMS.DeliveryDelete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div class="page-content">
    	<div class="row">
		  <div class="col-md-10">

	  			<div class="row">
	  				<div class="col-md-12 panel-info">
			  			<div class="content-box-header panel-heading">
		  					<div class="panel-title ">Delete Delivery</div>
						
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
                                        
				  							
												<button class="btn btn-primary" type="submit" onclick="DeleteDelivery()">
													<i class="fa fa-save" ></i>
													Delete
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
