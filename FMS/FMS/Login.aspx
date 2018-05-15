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
             
</head>
<body>
<!-- Login Page -->
<div class="log_page"></div>
		<div class="grad">
		<div class="header">
			<div>Fleet Management System</div>
		</div>
		<div class="login">
				<input id="username" type="text" placeholder="username" name="user" /><br/>
				<input id="password" type="password" placeholder="password" name="password" /><br/>
				<input id="btnLogin" type="button" value="Login" onclick="validate()" /> <br/> 
		</div>
    </div>
		<br/>
		</body>
      <script>
          function validate() {
              var username = document.getElementById("username").value;
              var password = document.getElementById("password").value;
              if (username == "Mmeli" && password == "Fleet123") {
                  window.location = "Home"; // Redirecting to other page.
                  return false;
              }
              else {
                  alert("Login was unsuccessful, please check your username and password");
                  document.getElementById("Login_Error").innerHTML = "Login was unsuccessful, please check your username and password";
              }

          };
      </script>
      
</html>

