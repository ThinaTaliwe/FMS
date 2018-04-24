<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DriverReport.aspx.cs" Inherits="FMS.DriverReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
    	<div class="row">
		  <div class="col-md-10">

	  			<div class="row">
	  				<div class="col-md-12 panel-info">
			  			<div class="content-box-header panel-heading">
		  					<div class="panel-title ">Driver Report</div>
						
			  			</div>

			  		</div>
	  			</div>

				<div class="row">
					<div class="col-md-12">
						<div class="content-box-large">
		  				<div class="panel-body">
                              <form class="form-horizontal" role="form">

                                   <div class="form-group">
												<label class="control-label col-md-2">Select a Driver</label>
											<div class="col-md-10">
												<select class="form-control">
													<option>Khanyisile Morudu</option>
													<option>Thina Taliwe</option>
													
												</select>
											</div>
										</div>

                                   <div class="form-group">
										    <label  class="col-sm-2 control-label">From</label>
										    <div class="col-sm-10">
										     	
						                    <form action="/action_page.php">
                                              <input class="form-control" type="date" id="DriverReportFrom">
                                            </form>
										    </div>
										  </div>
                                 <div class="form-group">
										    <label  class="col-sm-2 control-label">To</label>
										    <div class="col-sm-10">
										     	
						                    <form action="/action_page.php">
                                              <input class="form-control" type="date" id="DriverReportTo">
                                            </form>
										    </div>
                                     	
										  </div>
                                   
                                   <div class="form-group">
												<label class="control-label col-md-2">Select Graph Type</label>
											<div class="col-md-10">
												<select class="form-control">
													<option>Table</option>
													<option>Graph</option>
													
												</select>
											</div>
										</div>
                               
                                  
				  						
												<button class="btn btn-primary" type="submit" onclick="DriverReport()">
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
