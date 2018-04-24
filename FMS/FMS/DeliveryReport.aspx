<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeliveryReport.aspx.cs" Inherits="FMS.DeliveryReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="page-content">
    	<div class="row">
		  <div class="col-md-10">

	  			<div class="row">
	  				<div class="col-md-12 panel-info">
			  			<div class="content-box-header panel-heading">
		  					<div class="panel-title ">Delivery Report</div>
						    
			  			</div>

			  		</div>
	  			</div>

				<div class="row">

					<div class="col-md-12">
						<div class="content-box-large">
		  				<div class="panel-body">
                              <p> Leave Blank if Filter Does Not Apply</p>
                              <form class="form-horizontal" role="form">
                                   <div class="form-group">
                                               <label  class="col-sm-2 control-label"> Destination </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="Destination" placeholder="">
										    </div>
										  </div>
                                   <div class="form-group">
                                               <label  class="col-sm-2 control-label"> Origin </label>
										    <div class="col-sm-10">
										      <input class="form-control" id="Origin" placeholder="">
										    </div>
										  </div>
                                   <div class="form-group">
												<label class="control-label col-md-2">Material</label>
											<div class="col-md-10">
												<select class="form-control">
                                                    <option>None</option>
													<option>Coal</option>
													<option>Clinkers</option>
													<option>Pozzsand</option>
												</select>
											</div>
										</div>

                                   <div class="form-group">
										    <label  class="col-sm-2 control-label">From*</label>
										    <div class="col-sm-10">
										     	
						                    <form action="/action_page.php">
                                              <input class="form-control" type="date" id="DeliveryReportFrom">
                                            </form>
										    </div>
										  </div>
                                 <div class="form-group">
										    <label  class="col-sm-2 control-label">To*</label>
										    <div class="col-sm-10">
										     	
						                    <form action="/action_page.php">
                                              <input class="form-control" type="date" id="DeliveryReportTo">
                                            </form>
										    </div>
                                     	
										  </div>
                                   
                                   <div class="form-group">
												<label class="control-label col-md-2">Select Graph Type*</label>
											<div class="col-md-10">
												<select class="form-control">
													<option>Table</option>
													<option>Graph</option>
													
												</select>
											</div>
										</div>
                               
                                  
				  						
												<button class="btn btn-primary" type="submit" onclick="DeliveryReport()">
													<i class="fa fa-save" ></i>
													Search
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
