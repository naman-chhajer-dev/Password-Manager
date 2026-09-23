<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Password_Manager.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <div>
    <h1>Mini Password Manager</h1>

    <p>Welcome to my Password Manager</p>

    <asp:Button 
        ID="btnStart"
        runat="server"
        Text="Get Started"
        OnClick="btnStart_Click" />
</div>
    </form>
</body>
</html>
