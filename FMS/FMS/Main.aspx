<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="FMS.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!--main content start-->
      <section id="main-content">
          <section class="wrapper">
              <div class="row">
                  <h3 align="center"> This is where the map will go </h3>
					<!--map start start-->

					<div style="width:500px;height:300px;border:1px solid #000;">MAP</div>
                  </div>
                  
                  
      
          </section>
      </section>

             <!-- js placed at the end of the document so the pages load faster -->
    <script src="assets/js/jquery.js"></script>
   
    <script class="include" type="text/javascript" src="assets/js/jquery.dcjqaccordion.2.7.js"></script>
    <script src="assets/js/jquery.scrollTo.min.js"></script>
    <script src="assets/js/jquery.nicescroll.js" type="text/javascript"></script>
    <script src="assets/js/jquery.sparkline.js"></script>


    <!--common script for all pages-->
    <script src="assets/js/common-scripts.js"></script>
    
    <script type="text/javascript" src="assets/js/gritter/js/jquery.gritter.js"></script>
    <script type="text/javascript" src="assets/js/gritter-conf.js"></script>

</asp:Content>
