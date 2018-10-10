<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Example Page</title>
    <script>
        function foo(val) {
            document.getElementById(val).innerText = "Removed this";
            document.getElementById('theDiv').innerText = "It worked";
        }
    </script>
</head>
<body>
    <div id="test"''>
         kosovodfjnv
        <input type="button" value="click me" onclick="foo('test')" />
    </div>
    <div id="theDiv"></div>
</body>
</html>