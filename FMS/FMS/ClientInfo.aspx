<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientInfo.aspx.cs" Inherits="FMS.ClientInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="wrapper">
                <div class="content-box-large">
                <div class="row">

                    <div class="col-md-10">
                        <div class="content-panel">
                             <table class="table">
                            <thead>
                                    <tr>
                                        <th>Client Details</th>
                                    </tr>
                                </thead>
                                <tbody runat="server" id="tables">
                                </tbody>

                            <!-- <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>-->
                 
                             </table>
                            <asp:Button ID="client" runat="server" Text="Report" OnClick="report_Click" />
                        </div><! --/content-panel -->
                    </div><!-- /col-md-12 -->
    
                    </div><!-- /col-md-12 -->
                </div><!-- row -->  

            </section><!-- /wrapper -->

</asp:Content>
