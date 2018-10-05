<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteTruck.aspx.cs" Inherits="FMS.DeleteTruck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section id="main-content">
            <section class="wrapper">
                <div class="content-box-large">

				<div class="row">
					<div class="col-md-12">
						<div class="content-box-large">
		  				    <div class="panel-body">



















	  
                                          
									      <div class="form-group" role="form">
                                              <label  class="col-sm-2 control-label"> Truck(s) </label>
										      <div class="col-sm-10">
                                                <select class="form-control" id="delTruck" runat="server">
													<option>Select Truck</option>
												</select> 
										    </div>
                                            <asp:Button ID="btnRemove" class="btn btn-default" runat="server" Text="Remove" OnClick="RemoveTruck"/>
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
