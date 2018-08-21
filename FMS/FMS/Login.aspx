<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FMS.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - Fleet Management System</title>
    <!-- Add Stylesheet --> 
       <link type="text/css" rel="stylesheet" href="Content/ass/css/login.css"/>
    <!-- Image for favicon -->
       <link href="~/fav.jpg" rel="shortcut icon" type="image/x-icon" />
    <!-- Javascript File -->
    <script type="text/javascript"src="Content/ass/js/js.js"></script> 
             
    <style type="text/css">
        .auto-style1 {
            position: absolute;
            height: 150px;
            width: 425px;
            padding: 10px;
            z-index: 2;
            top: 220px;
            left: 682px;
            margin-bottom: 4px;
        }
    </style>
             
</head>
<body>
<!-- Login Page -->
<div class="log_page"></div>
		<div class="grad">
		<div class="header">
			<div>Fleet Management System</div>
		</div>
		<div class="auto-style1">
            <form runat="server">
                <input id="username" type="text" placeholder="Username" name="user" runat="server"/><asp:RequiredFieldValidator ID="valdUser" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                <br/>
				<input id="password" type="password" placeholder="Password" name="password" runat="server" /><asp:RequiredFieldValidator ID="valdPass" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                <br/>
				<input id="btnLogin" type="button" value="Login" runat="server" OnServerClick="logon"/> <br/> 
               <p>  <a href="Login.aspx" style="color:white"> Forgot Password?</a> </p>
            </form>
				
		</div>
    </div>
		<br/>
		</body>
      
</html>

