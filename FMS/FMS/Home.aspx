<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FMS.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section id="main-content">
            <section class="wrapper">
                <div class="content-box-large">
                <h3>Deliveries Done</h3>
                <div class="row">

                    <div class="col-md-12">
                        <div class="content-panel">
                            <hr>
                            <table class="table">
                                <!-- Heading --> 
                                <thead>
                                    <tr>
                                        <th>Order Number</th>
                                        <th>Truck Plate</th>
                                        <th>Destination</th>
                                        <th>Time Delivered</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>1</td>
                                        <td>RMB 332 GP</td>
                                        <td>Sasolburg</td>
                                        <td>12h01</td>
                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td>RVF 212 GP</td>
                                        <td>Nelspruit</td>
                                        <td>11h20</td>
                                    </tr>
                                    <tr>
                                        <td>3</td>
                                        <td>BGF 212 GP</td>
                                        <td>Sasolburg</td>
                                        <td>10h00</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div><! --/content-panel -->
                    </div><!-- /col-md-12 -->

                    
                    </div><!-- /col-md-12 -->
                </div><!-- row -->

                

            </section><! --/wrapper -->
        </section><!-- /MAIN CONTENT -->


</asp:Content>
