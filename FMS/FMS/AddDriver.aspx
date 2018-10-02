<%@ Page Title="Add Driver" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddDriver.aspx.cs" Inherits="FMS.AddDriver" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
            <div class="page-content">
    	<div class="row">
		  <div class="col-md-10">

	  			<div class="row">
	  				<div class="col-md-12 panel-info">
			  			<div class="content-box-header panel-heading">
		  					<div class="panel-title ">Add Driver</div>
						
			  			</div>

			  		</div>
	  			</div>

				<div class="row">
					<div class="col-md-12">
						<div class="content-box-large">
		  				<div class="panel-body">
                              <form class="form-horizontal" role="form">
                                  <div class="form-group">
                                               <label  class="col-sm-2 control-label"> ID </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="DriverID" placeholder="" runat="server">
										    </div>
										  </div>

										  <div class="form-group">
                                               <label  class="col-sm-2 control-label"> Name </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="DriverName" placeholder="" runat="server">
										    </div>
										  </div>
                                         <div class="form-group">
                                              <label  class="col-sm-2 control-label"> Surname </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="DriverSurname" placeholder="" runat="server">
										    </div>
										  </div>
                                   <div class="form-group">
                                               <label  class="col-sm-2 control-label"> Cellphone Number </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="DriverCell" placeholder="" runat="server">
										    </div>
										  </div>
                                          <div class="form-group">
                                               <label  class="col-sm-2 control-label"> Email </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="DriverEmail" placeholder="" runat="server">
										    </div>
										  </div>
                                   <div class="form-group">
                                        <label  class="col-sm-2 control-label"> Licence Code </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="DriverCode" placeholder="" runat="server">
										    </div>
										  </div>
                                  <div class="form-group">
                                        <label  class="col-sm-2 control-label"> Restriction </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="Restriction" placeholder="" runat="server">
										    </div>
										  </div>

                                   <div class="form-group">
										    <label  class="col-sm-2 control-label">First Issue</label>
										    <div class="col-sm-10">
										     	
                                              <input class="form-control" type="date" id="LicenceIssueDate" runat="server">
										    </div>
										  </div>
                                 <div class="form-group">
										    <label  class="col-sm-2 control-label">Expiry Date</label>
										    <div class="col-sm-10">
										     
                                              <input class="form-control" type="date" id="LicenceExpiryDate" runat="server">
										    </div>
                                     	
										  </div>
                                   
                               
                                  
				  							<button class="btn btn-default" type="submit" onclick="CancelAddDriver()">
													Cancel
												</button>
												  <asp:Button ID="btn" class="btn btn-primary" runat="server" Text="Submit"  OnClick="Add_Driver"  />	 </form>
                        	 
										  
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
