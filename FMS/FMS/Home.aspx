<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FMS.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section id="main-content">


            <section class="wrapper">
                <div class="content-box-large">
                <h3>Deliveries Today</h3>
                <div class="row">

                    <div class="col-md-10">
                        <div class="content-panel">
                            <div class="panel-heading">
							<div class="panel-title"> </div>
							
							<div class="panel-options">
								<a href="CreateDelivery.aspx" data-rel="collapse">Create New Deliveries</a>
							</div>
						</div>
                            <hr> 
                            <table class="table">
                                <!-- Heading --> 
                                <thead>
                                    <tr>
                                        <th>Company</th>
                                        <th>Address: From</th>
                                        <th>Address: To</th>
                                        <th>Driver</th>
                                        <th>ETA</th>
                                        <th>Status</th>
                                    </tr>
                                </thead>
                                <tbody runat="server" id="tables">
                                    
                                </tbody>
                            </table>
                        </div><! --/content-panel -->
                    </div><!-- /col-md-12 -->

                    
                    </div><!-- /col-md-12 -->
                </div><!-- row -->

                

            </section><! --/wrapper -->
        </section><!-- /MAIN CONTENT -->


</asp:Content>
