<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewClients.aspx.cs" Inherits="FMS.ViewClients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section id="main-content">


            <section class="wrapper">
                <div class="content-box-large">
                <h3>All Clients</h3>
                <div class="row">

                    <div class="col-md-10">
                        <div class="content-panel">
                            <hr>
                            <table class="table">
                                <!-- Heading --> 
                                <thead>
                                    <tr>
                                        <th>Company Name</th>
                                        <th>Contact Person </th>
                                        <th>Contact Number</th>
                                        <th>Email Address</th>
                                        <th>Address</th>
                                       
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
