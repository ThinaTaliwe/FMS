<%@ Page Title="All Trucks" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAllTrucks.aspx.cs" Inherits="FMS.ViewAllTrucks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section id="main-content">
            <section class="wrapper">
                <div class="content-box-large">
                <div class="row">

                    <div class="col-md-10">
                        <div class="content-panel">
                            <table class="table">
                                <!-- Heading --> 
                                <thead>
                                    <tr>
                                        <th>Plate Number</th>
                                        <th>Brand </th>
                                        <th>Load Capacity</th>
                                        <th>Max Speed</th>
                                        <th>Class<th>
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
