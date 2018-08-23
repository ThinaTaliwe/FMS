<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deliv.aspx.cs" Inherits="FMS.deliv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="delivery" runat="server"></asp:DropDownList>
            <asp:DropDownList ID="driver" runat="server"></asp:DropDownList>
            <asp:DropDownList ID="truck" runat="server"></asp:DropDownList>
            <asp:Button ID="button" runat="server" OnClick="getDeliv" Text="View" />
        </div>
        <asp:Label ID="text" runat="server" Text="" ></asp:Label>
    </form>
</body>
</html>
