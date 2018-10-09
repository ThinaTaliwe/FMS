<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DriverInfo.aspx.cs" Inherits="FMS.DriverInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="wrapper">
                <div class="content-box-large">
                <div class="row">

                    <div class="col-md-10">
                        <div class="content-panel">
                             <table class="table">
                            <thead>
                                    <tr>
                                        <th>Driver Details</th>
                                    </tr>
                                </thead>
                                <tbody runat="server" id="tables">
                                </tbody>
                 
                             </table>
                            <asp:Button ID="driver" runat="server" Text="Report" OnClick="report_Click" />
                        </div><! --/content-panel -->
                    </div><!-- /col-md-12 -->
    
                    </div><!-- /col-md-12 -->
                </div><!-- row -->  

            </section><!-- /wrapper -->

</asp:Content>
