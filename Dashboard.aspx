<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs"
    Inherits="Password_Manager.Dashboard" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <title>Password Manager - Dashboard</title>

    <link href="Style.css"
          rel="stylesheet"
          type="text/css" />

</head>

<body>

<form id="form1" runat="server">

    <div class="container">

        <div class="card">

            <h1>Password Manager</h1>

            <h2>
                <asp:Label
                    ID="lblWelcome"
                    runat="server">
                </asp:Label>
            </h2>

            <p>
                Manage your saved website credentials securely.
            </p>

            <div class="dashboard-actions">

                <asp:Button
                    ID="btnAddCredential"
                    runat="server"
                    Text="Add Credential"
                    CssClass="btn btn-success"
                    OnClick="btnAddCredential_Click" />

                <asp:Button
                    ID="btnViewCredentials"
                    runat="server"
                    Text="View Credentials"
                    CssClass="btn btn-primary"
                    OnClick="btnViewCredentials_Click" />

                <asp:Button
                    ID="btnLogout"
                    runat="server"
                    Text="Logout"
                    CssClass="btn btn-danger"
                    OnClick="btnLogout_Click" />

            </div>

        </div>

    </div>

</form>

</body>
</html>